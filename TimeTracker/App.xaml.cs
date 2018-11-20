using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Xml;
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
        String version = "0.9.0.0";

        private string[] blacklist = {
            "TimeTracker",
            "Neue Benachrichtigung",
            "Explorer",
            "Cortana",
            "Akkuinformationen",
            "Start",
            "UnlockingWindow",
            "Cortana",
            "Akkuinformationen",
            "Status",
            "Aktive Anwendungen",
            "Window Dialog",
            "Info-Center",
            "Windows-Standardsperrbildschirm",
            "Host für die Windows Shell-Oberfläche",
            "F12PopupWindow",
            "LockingWindow",
            "SurfaceDTX",
            "CTX_RX_SYSTRAY",
            "[]"
        };

        private static SettingsWindow SettingsWindow;

        private static bool paused = false;
        private static bool disturb = true;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Set connection string
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TimeTracker";

            if(!File.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            if(!File.Exists(path + "\\db.db"))
                System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + "default.db", path + "\\db.db", false);

            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            // Set up app to run in the background
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            SettingsWindow = new SettingsWindow();
            SettingsWindow.Closing += SettingsWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => changeActivity(null);

            //Icon icon = Icon.FromHandle(((Bitmap)Image.FromFile("Resources/time-tracking-icon-0.png")).GetHicon());

            _notifyIcon.Icon = TimeTracker.Properties.Resources.MyIcon;
            _notifyIcon.Visible = true;
            CreateContextMenu();

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

            using (mainEntities db = new mainEntities())
            {
                // Check if the tutorial shoukd be shown
                bool tutorialViewed = db.settings.Find("tutorialViewed") != null ? db.settings.Find("tutorialViewed").value == 1 : false;

                if(!tutorialViewed)
                {
                    new Tutorial().Show();
                    settings setting = new settings();

                    setting.key ="tutorialViewed";
                    setting.value = 1;

                    db.settings.Add(setting);
                    db.SaveChanges();
                }

            }

            // Check for update
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

            if(!node.FirstChild.InnerXml.Equals("tag:github.com,2008:Repository/145717546/" + version))
            {
                new NewVersion().Show();
            }
        }

        /************* Methods for handeling app running in background ***************/
        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Change Activity").Click += (s, e) => changeActivity(null);
            _notifyIcon.ContextMenuStrip.Items.Add("Pause").Click += (s, e) => pause();
            _notifyIcon.ContextMenuStrip.Items.Add("Do not Disturb").Click += (s, e) => doNotDisturb();
            _notifyIcon.ContextMenuStrip.Items.Add("View Data").Click += (s, e) => new HTMLDataWindow().Show();
            _notifyIcon.ContextMenuStrip.Items.Add("Edit last Activities").Click += (s, e) => new ManualEdit().Show();
            _notifyIcon.ContextMenuStrip.Items.Add("Settings").Click += (s, e) => ShowSettingsWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            SettingsWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
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
            saveWindows();
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

        private void ShowSettingsWindow()
        {
            if (SettingsWindow.IsVisible)
            {
                if (SettingsWindow.WindowState == WindowState.Minimized)
                {
                    SettingsWindow.WindowState = WindowState.Normal;
                }
                SettingsWindow.Activate();
            }
            else
            {
                SettingsWindow.redrawSettings();
                SettingsWindow.Show();
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

        private void SettingsWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                SettingsWindow.Hide(); // A hidden window can be shown again, a closed one not             
            }
        }

        /*** Methods for handling power state change ***/
        private void OnPowerChange(object s, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    // ToDo: Ask what user did during time away from pc
                    break;
                case PowerModes.Suspend:
                    saveWindows();
                    break;
            }
        }

        /*** Method for handling session change ***/
        private void OnSessionSwitch(object s, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    saveWindows();
                    break;
                case SessionSwitchReason.SessionLogoff:
                    saveWindows();
                    break;
                case SessionSwitchReason.SessionLogon:
                    // ToDo: Ask what user did during time away from pc
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

        private void pause()
        {
            if(paused)
            {
                _notifyIcon.ContextMenuStrip.Items[1].Text = "Pause";
                paused = false;
            } else
            {
                saveWindows();
                _notifyIcon.ContextMenuStrip.Items[1].Text = "Unpause";
                paused = true;
            }
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
                if (blacklist.Contains(arr.Last()) || arr.Last().Contains("\\"))
                {
                    saveWindows();
                    return;
                }

                using (mainEntities db = new mainEntities())
                {
                    // Determine if this window has been used in the last 5 minutes
                    bool hasBeenSeen = false;

                    // Load timeNotUsed from settings
                    long timeNotUsed = db.settings.Find("timeNotUsed") != null ? db.settings.Find("timeNotUsed").value : Constants.defaultTimeNotUsed; // 5 minutes
                    long timeout2 = db.settings.Find("timeout2") != null ? db.settings.Find("timeout2").value : Constants.defaultTimeout2; // 5 minutes
                    bool fuzzyMatching = db.settings.Find("fuzzyMatching") != null ? db.settings.Find("fuzzyMatching").value == 1 : Constants.fuzzyMatching;
                    List<window_active> old_windows = db.window_active.Where(wa => wa.handle == handleLong || (fuzzyMatching && wa.name.Equals(name))).ToList();

                    if (old_windows.Count() > 0)
                    {
                        hasBeenSeen = timeNotUsed > DateTime.Now.Subtract(old_windows.Max(wa => wa.to) ?? DateTime.Now.AddDays(-9999)).TotalMilliseconds || timeNotUsed > DateTime.Now.Subtract(old_windows.Max(wa => wa.from)).TotalMilliseconds;
                    }

                    /* Handle active window */
                    // Set to date for old window if one exists
                    window_active old_window = db.window_active.Where(wa => wa.to == null).OrderByDescending(wa => wa.from).FirstOrDefault();

                    // If window has not changed do nothing
                    if (old_window != null && (old_window.handle == handleLong || (fuzzyMatching && old_window.name.Equals(name))))
                        return;

                    if (old_window != null)
                    {
                        old_window.to = DateTime.Now;
                    }

                    window_active new_window = new window_active();

                    new_window.name = arr.Last();
                    new_window.from = DateTime.Now;
                    new_window.handle = handleLong;

                    if (arr.Length >= 2)
                    {
                        new_window.details = string.Join("- ", arr.Take(arr.Length - 1));
                    }

                    db.window_active.Add(new_window);

                    db.SaveChanges();

                    // Show notification if app has not been seen in last few minutes
                    if ((Constants.lastConfirmed == null || DateTime.Now.Subtract((DateTime)Constants.lastConfirmed).TotalSeconds > timeout2) && !hasBeenSeen && disturb)
                    {
                        changeActivity(arr.Last());
                    }
                    // Handle case that window has been seen shortly before but still no activity is running. In this case just start up another activity of the same kind
                    else if (!db.activity_active.Any(aa => aa.to == null))
                    {
                        activity_active last_activity = db.activity_active.OrderByDescending(aa => aa.to).FirstOrDefault();

                        activity_active new_activity = new activity_active();
                        new_activity.from = DateTime.Now;
                        new_activity.name = last_activity != null ? last_activity.name : "";
                        db.activity_active.Add(new_activity);

                        db.SaveChanges();
                    }
                }
            }
        }

        private void closeOldActivity()
        {
            using (mainEntities db = new mainEntities())
            {
                activity_active old_activity = db.activity_active.Where(aa => aa.to == null).OrderByDescending(aa => aa.from).FirstOrDefault();
                activity_active older_activity = db.activity_active.Where(aa => aa.to != null).OrderByDescending(aa => aa.from).FirstOrDefault();
                if (old_activity != null)
                {
                    old_activity.to = DateTime.Now;

                    if (older_activity != null && old_activity.name.Equals(older_activity.name) && old_activity.from.Subtract((DateTime)older_activity.to).TotalSeconds <= 1)
                    {
                        old_activity.from = older_activity.from;
                        db.activity_active.Remove(older_activity);
                    }

                    db.SaveChanges();
                }
            }
        }

        private void changeActivity(string name)
        {
            using (mainEntities db = new mainEntities())
            {
                if (name == null) // If no name is specified check which window is active
                {
                    window_active current_window = db.window_active.Where(wa => wa.to == null).OrderByDescending(wa => wa.from).FirstOrDefault();
                    if (current_window != null)
                        name = current_window.name;
                    else
                        name = "No window active";
                }

                closeOldActivity();

                activity_active last_activity = db.activity_active.OrderByDescending(aa => aa.to).FirstOrDefault();

                activity_active new_activity = new activity_active();
                new_activity.from = DateTime.Now;
                new_activity.name = last_activity != null ? last_activity.name : "";
                db.activity_active.Add(new_activity);

                db.SaveChanges();

                List<string> selectable_activities;

                selectable_activities = db.Database.SqlQuery<string>("SELECT name FROM activity_active GROUP BY name ORDER BY max([from]) DESC LIMIT 5").ToList();

                // Load timeout from settings
                long timeout = db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout;

                CloseAllToasts();

                CustomToast newToast = new CustomToast(new_activity.id.ToString(), name);
                newToast.Show();
            }
        }

        private void saveWindows()
        {
            // Save open activities and windows
            using (mainEntities db = new mainEntities())
            {
                List<window_active> open_windows = db.window_active.Where(wa => wa.to == null).ToList();
                open_windows.ForEach(wa => wa.to = DateTime.Now);

                closeOldActivity();

                db.SaveChanges();
            }
        }
    }
}
