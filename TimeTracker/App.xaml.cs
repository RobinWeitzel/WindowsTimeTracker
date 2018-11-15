﻿using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
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
        private static DataWindow DataWindow;

        private static bool paused = false;

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

            DataWindow = new DataWindow();
            DataWindow.Closing += DataWindow_Closing;

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

            SystemEvents.SessionSwitch +=
       new SessionSwitchEventHandler(OnSessionSwitch);

            // Delete old records
            using (mainEntities db = new mainEntities())
            {
                long timeRecordsKept = db.settings.Find("timeRecordsKept") != null ? db.settings.Find("timeRecordsKept").value : Constants.defaultTimeRecordsKept;

                // If timeRecordsKept is 0 records are never deleted
                if (timeRecordsKept != 0)
                {
                    DateTime oldDate = DateTime.Now.Subtract(new TimeSpan((int)timeRecordsKept, 0, 0, 0, 0));
                    db.window_active.RemoveRange(db.window_active.Where(wa => wa.to != null && wa.to < oldDate));
                    db.activity_active.RemoveRange(db.activity_active.Where(aa => aa.to != null && aa.to < oldDate));
                }
            }
        }

        /************* Methods for handeling app running in background ***************/
        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Change Activity").Click += (s, e) => changeActivity(null);
            _notifyIcon.ContextMenuStrip.Items.Add("Pause").Click += (s, e) => pause();
            _notifyIcon.ContextMenuStrip.Items.Add("View Data").Click += (s, e) => ShowDataWindow();
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

        private static void ShowDataWindow()
        {
            if (DataWindow.IsVisible)
            {
                if (DataWindow.WindowState == WindowState.Minimized)
                {
                    DataWindow.WindowState = WindowState.Normal;
                }
                DataWindow.Activate();
            }
            else
            {
                DataWindow.Navigate(new Overview());
                DataWindow.Show();
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

        private void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                DataWindow.Hide(); // A hidden window can be shown again, a closed one not             
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
                    if (!hasBeenSeen)
                    {
                        changeActivity(arr.Last());
                    }
                    // Handle case that window has been seen shortly before but still no activity is running. In this case just start up another activity of the same kind
                    else if (!db.activity_active.Any(aa => aa.to == null))
                    {
                        closeOldActivity();

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
                activity_active old_activity = db.activity_active.OrderByDescending(aa => aa.from).FirstOrDefault();
                activity_active older_activity = db.activity_active.OrderByDescending(aa => aa.from).Skip(1).FirstOrDefault();
                if (old_activity != null)
                {
                    old_activity.to = DateTime.Now;

                    if (older_activity != null && old_activity.name.Equals(older_activity.name) && (old_activity.from - (DateTime)older_activity.to).Seconds <= 1)
                    {
                        old_activity.from = older_activity.from;
                        db.activity_active.Remove(older_activity);
                    }
                }

                db.SaveChanges();
            }
        }

        private void changeActivity(string name)
        {
            using (mainEntities db = new mainEntities())
            {
                bool lastActivities = db.settings.Find("lastActivities") != null ? db.settings.Find("lastActivities").value == 1 : Constants.lastActivities;

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
                open_windows.ForEach(aa => aa.to = DateTime.Now);

                List<activity_active> open_activities = db.activity_active.Where(aa => aa.to == null).ToList();
                open_activities.ForEach(aa => aa.to = DateTime.Now);

                db.SaveChanges();
            }
        }
    }
}
