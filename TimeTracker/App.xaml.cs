using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Xml;
using TimeTracker.Properties;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        WinEventDelegate dele = null;

        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;

        private static bool paused = false;
        private static bool disturb = true;

        private KeyboardHook hook; // Global keyboard hook 

        protected override void OnStartup(StartupEventArgs e)
        {
            // Set connection string
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TimeTracker";
            Variables.activityPath = path + "\\Activities.csv";
            Variables.windowPath = path + "\\Windows.csv";

            if (!File.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            if (!File.Exists(Variables.windowPath))
            {
                using (TextWriter tw = new StreamWriter(Variables.windowPath))
                {
                    var csv = new CsvWriter(tw);
                    csv.WriteHeader<Helper.Window>();
                    csv.NextRecord();
                }
            }

            if (!File.Exists(Variables.activityPath))
            {
                using (TextWriter tw = new StreamWriter(Variables.activityPath))
                {
                    var csv = new CsvWriter(tw);
                    csv.WriteHeader<Helper.Activity>();
                    csv.NextRecord();
                }
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            // Set up app to run in the background
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => changeActivity();

            _notifyIcon.Icon = TimeTracker.Properties.Resources.MyIcon;
            _notifyIcon.Visible = true;
            CreateContextMenu();

            // Set up global hotkey
            hook = new KeyboardHook();
            hook.KeyCombinationPressed += KeyCombinationPressed;

            // Set up callback if active window changes
            dele = new WinEventDelegate(WinEventProc);
            IntPtr m_hhook = SetWinEventHook(
                EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND,
                IntPtr.Zero,
                dele,
                0,
                0,
                WINEVENT_OUTOFCONTEXT);

            // Set up callback if computers powerstate changes
            SystemEvents.PowerModeChanged += OnPowerChange;

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(OnSessionSwitch);

            // Check if the tutorial should be shown
            if(!Settings.Default.TutorialViewed)
            {
                new Tutorial().Show();
                Settings.Default.TutorialViewed = true;
                Settings.Default.Save();
            }

            // Check for update
            try
            {
                var m_strFilePath = "https://github.com/RobinWeitzel/WindowsTimeTracker/releases.atom";
                string xmlStr;
                using (var wc = new WebClient())
                {
                    xmlStr = wc.DownloadString(m_strFilePath);
                }
                var xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(xmlStr);

                XmlNode root = xmlDoc.DocumentElement;

                // Add the namespace.  
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("f", "http://www.w3.org/2005/Atom");

                XmlNode node = root.SelectSingleNode(
         "descendant::f:entry", nsmgr);

                if (!node.FirstChild.InnerXml.Equals("tag:github.com,2008:Repository/145717546/" + Variables.version))
                {
                    new NewVersion().Show();
                }
            } catch(WebException ignore)
            {

            }
        }

        /************* Methods for handeling app running in background ***************/
        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Change Activity").Click += (s, e) => changeActivity();
            _notifyIcon.ContextMenuStrip.Items.Add("Pause").Click += (s, e) => pause(null);
            _notifyIcon.ContextMenuStrip.Items.Add("Do not Disturb").Click += (s, e) => doNotDisturb();
            _notifyIcon.ContextMenuStrip.Items.Add("View Data").Click += (s, e) => new HTMLDataWindow().Show();
            _notifyIcon.ContextMenuStrip.Items.Add("Edit Activities").Click += (s, e) => new ManualEdit().Show();
            _notifyIcon.ContextMenuStrip.Items.Add("Settings").Click += (s, e) => new SettingsWindow().Show();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
            hook.Dispose();
            hook = null;
            this.Shutdown(1);
        }

        private void CloseAllToasts()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                if(App.Current.Windows[intCounter].GetType().Name.Equals("CustomToast"))
                    App.Current.Windows[intCounter].Close();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            closeCurrentWindow();
            closeCurrentActivity();
        }

        private void ShowMainWindow()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }
        
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not             
            }
        }

        /*** Methods for handling power state change ***/
        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    if(Settings.Default.OfflineTracking)
                        new ManualTracking().Show();
                    pause(false);
                    break;
                case PowerModes.Suspend:
                    pause(true);
                    break;
            }
        }

        /*** Method for handling session change ***/
        private void OnSessionSwitch(object s, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    pause(true);
                    break;
                case SessionSwitchReason.SessionLogoff:
                    pause(true);
                    break;
                case SessionSwitchReason.SessionLogon:
                    if (Settings.Default.OfflineTracking)
                        new ManualTracking().Show();
                    pause(false);
                    break;
                case SessionSwitchReason.SessionUnlock:
                    if (Settings.Default.OfflineTracking)
                        new ManualTracking().Show();
                    pause(false);
                    break;
            }
        }

        private void doNotDisturb()
        {
            if(disturb)
            {
                _notifyIcon.ContextMenuStrip.Items[2].Text = "Disable \"Do not disturb\"";
                disturb = false;
            } else
            {
                _notifyIcon.ContextMenuStrip.Items[2].Text = "Do not disturb";
                disturb = true;
            }
        }

        private void pause(bool? setPause)
        {
            if(setPause != null)
            {
                if (setPause == true)
                {
                    closeCurrentWindow();
                    closeCurrentActivity();
                    _notifyIcon.ContextMenuStrip.Items[1].Text = "Unpause";
                    paused = true;
                }
                else
                {
                    _notifyIcon.ContextMenuStrip.Items[1].Text = "Pause";
                    paused = false;
                }
            }
            else // Toggle pause
            {
                if (paused)
                {
                    _notifyIcon.ContextMenuStrip.Items[1].Text = "Pause";
                    paused = false;
                }
                else
                {
                    closeCurrentWindow();
                    closeCurrentActivity();
                    _notifyIcon.ContextMenuStrip.Items[1].Text = "Unpause";
                    paused = true;
                }
            }
        }

        /*** Method for handling hotkey press ***/
        void KeyCombinationPressed(object sender, EventArgs e)
        {
            changeActivity(true);
        }

        /************* Methods for tracking active window ***************/

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetWindowText(IntPtr hwnd, StringBuilder lpString, long cch);

        [DllImport("User32.dll")]
        static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("kernel32.dll")]
        static extern int GetProcessId(IntPtr handle);

        private string GetActiveWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            if (handle.ToInt64() == 0)
                handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (paused)
                return;

            IntPtr handle;
            long handleLong;

            // Check if parent exists, if it does use it (meaning the active window is only a sub window)
            IntPtr newParent = GetParent(hwnd);
            IntPtr oldParent = hwnd;
            while (newParent.ToInt64() > 0)
            {
                oldParent = newParent;
                newParent = GetParent(oldParent);
            }

            handle = oldParent;

            handleLong = hwnd.ToInt64();

            if (handleLong <= 0)
                return;
            /*try Code to access process name and path (currently not used)
            {
                Process myProcess = Process.GetProcesses().Single(
        p => p.Id != 0 && p.MainWindowHandle == handle);

                Console.WriteLine(Path.GetFileName(myProcess.MainModule.FileName));
                Console.WriteLine(myProcess.MainWindowTitle);
            } catch (Exception e)
            {
                return;
            }*/

            string windowTitle = GetActiveWindowTitle(handle);
            if (windowTitle == null)
            {
                return;
            }

            string[] arr = windowTitle.Split(new string[] { "- " }, StringSplitOptions.None);
            if (arr.Length >= 1)
            {
                string name = arr.Last();
                // Stop if the current activity is blacklisted or a file path
                if (Settings.Default.Blacklist.Contains(arr.Last()) || arr.Last().Contains("\\"))
                {
                    if(!windowTitle.Equals("NotificationsWindow - TimeTracker"))
                        closeCurrentWindow();
                    return;
                }

                    // Determine if this window has been used in the last 5 minutes
                    bool hasBeenSeen = false;

                // Load timeNotUsed from settings
                long timeNotUsed = Settings.Default.TimeSinceAppLastUsed * 60 * 1000;  // Convert to ms 
                long timeout2 = Settings.Default.Timeout2;

                DateTime lastSeen;
                if (Variables.windowsLastSeen.TryGetValue(name, out lastSeen))
                    hasBeenSeen = timeNotUsed > DateTime.Now.Subtract(lastSeen).TotalMilliseconds;

                // If window has not changed do nothing
                if (Variables.currentWindow != null && Variables.currentWindow.Name.Equals(name))
                    return;

                if(Variables.currentWindow != null)
                {
                    closeCurrentWindow();
                }

                Variables.currentWindow = new Helper.Window();
                Variables.currentWindow.Name = arr.Last();
                Variables.currentWindow.From = DateTime.Now;

                if (arr.Length >= 2)
                {
                    Variables.currentWindow.Details = string.Join("- ", arr.Take(arr.Length - 1));
                }

                // Show notification if app has not been seen in last few minutes
                if ((Variables.lastConfirmed == null || DateTime.Now.Subtract((DateTime)Variables.lastConfirmed).TotalSeconds > timeout2) && !hasBeenSeen && disturb)
                {
                    changeActivity();
                }
                // Handle case that window has been seen shortly before but still no activity is running. In this case just start up another activity of the same kind
                else if (Variables.currentActivity == null)
                {
                    using (TextReader tr = new StreamReader(Variables.activityPath))
                    {
                        var csv = new CsvReader(tr);
                        var records = csv.GetRecords<Helper.Activity>();

                        Helper.Activity last_activity = records.LastOrDefault();

                        Variables.currentActivity = new Helper.Activity();
                        Variables.currentActivity.Name = last_activity != null ? last_activity.Name : "";
                        Variables.currentActivity.From = DateTime.Now;
                    } 
                }
            }
        }


        private void closeCurrentActivity()
        {
            if (Variables.currentActivity != null)
                Variables.currentActivity.save();
            Variables.currentActivity = null;
        }

        private void closeCurrentWindow()
        {
            if (Variables.currentWindow != null)
                Variables.currentWindow.save();
            Variables.currentWindow = null;
        }

        private void changeActivity(bool focusToast = false)
        {
            try
            {
                CloseAllToasts();

                CustomToast newToast = new CustomToast(focusToast);
                newToast.Show();
                if (focusToast)
                    newToast.Activate();
            }
            catch (ObjectDisposedException ignore) {
            }
        }
    }
}
