using DesktopNotifications;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
//using static DesktopNotifications.NotificationActivator;
//using DesktopNotifications;

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

        private static string lastNameSelected;
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

            DesktopNotificationManagerCompat.RegisterAumidAndComServer<MyNotificationActivator>("Robin.TimeTracker");
            DesktopNotificationManagerCompat.RegisterActivator<MyNotificationActivator>();

            // If launched from a toast
            if (e.Args.Contains("-ToastActivated"))
            {
                // Our NotificationActivator code will run after this completes,
                // and will show a window if necessary.
            }
            else
            {
                // Show the window
                // In App.xaml, be sure to remove the StartupUri so that a window doesn't
                // get created by default, since we're creating windows ourselves (and sometimes we
                // don't want to create a window if handling a background activation).
                //new MainWindow().Show();
            }

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
            DesktopNotificationManagerCompat.History.Clear();
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

        /*** Methods for andling toast notifications ***/

        // The GUID CLSID must be unique to your app. Create a new GUID if copying this code.
        [ClassInterface(ClassInterfaceType.None)]
        [ComSourceInterfaces(typeof(INotificationActivationCallback))]
        [Guid("7dc8e5b7-fcf1-42a0-ab75-85203b4b76ff"), ComVisible(true)]
        public class MyNotificationActivator : NotificationActivator
        {
            public override void OnActivated(string invokedArgs, NotificationUserInput userInput, string appUserModelId)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    // Tapping on the top-level header launches with empty args
                    if (invokedArgs.Length == 0)
                    {
                        ShowDataWindow();
                        return;
                    }

                    NotificationUserInput helper = userInput;

                    // Parse the query string (using NuGet package QueryString.NET)
                    QueryString args = QueryString.Parse(invokedArgs);

                    // See what action is being requested 
                    if (args["action"].Equals("dismiss"))
                    {
                        using (mainEntities db = new mainEntities())
                        {
                            activity_active current_activity = db.activity_active.Find(int.Parse(args["activityId"]));

                            current_activity.name = userInput["activity"];
                            lastNameSelected = userInput["activity"];
                            db.SaveChanges();
                        }
                    } else if(args["action"].Equals("other"))
                    {
                        showNotification2(
                            int.Parse(args["activityId"]),
                            args["name"],
                            "For which project are you using this app?"
                        );
                    } else if(args["action"].Equals("customToast"))
                    {
                        using (mainEntities db = new mainEntities())
                        {
                            CustomToast newToast = new CustomToast(args["activityId"], args["window"]);
                            newToast.Show();
                        }
                    }
                });
            }
            private void OpenWindowIfNeeded()
            {
                // Make sure we have a window open (in case user clicked toast while app closed)
                if (App.Current.Windows.Count == 0)
                {
                    new MainWindow().Show();
                }

                // Activate the window, bringing it to focus
                App.Current.Windows[0].Activate();

                // And make sure to maximize the window too, in case it was currently minimized
                App.Current.Windows[0].WindowState = WindowState.Normal;
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
                        if (db.activities.FirstOrDefault() == null)
                            return;
                        activity_active last_activity = db.activity_active.OrderByDescending(aa => aa.to).FirstOrDefault();

                        activity_active new_activity = new activity_active();
                        new_activity.from = DateTime.Now;
                        new_activity.name = last_activity != null ? last_activity.name : db.activities.FirstOrDefault().name;
                        db.activity_active.Add(new_activity);

                        db.SaveChanges();
                    }
                }
            }
        }

        private void changeActivity(string name)
        {
            using (mainEntities db = new mainEntities())
            {
                if (db.activities.FirstOrDefault() == null)
                    return;

                bool lastActivities = db.settings.Find("lastActivities") != null ? db.settings.Find("lastActivities").value == 1 : Constants.lastActivities;

                if (name == null) // If no name is specified check which window is active
                {
                    window_active current_window = db.window_active.Where(wa => wa.to == null).OrderByDescending(wa => wa.from).FirstOrDefault();
                    if (current_window != null)
                        name = current_window.name;
                    else
                        name = "No window active";
                }

                /* Handle active activity */
                activity_active old_activity = db.activity_active.Where(aa => aa.to == null).OrderByDescending(aa => aa.from).FirstOrDefault();
                if (old_activity != null)
                {
                    old_activity.to = DateTime.Now;
                }

                activity_active last_activity = db.activity_active.OrderByDescending(aa => aa.from).FirstOrDefault();

                activity_active new_activity = new activity_active();
                new_activity.from = DateTime.Now;
                new_activity.name = lastNameSelected ?? (last_activity != null ? last_activity.name : db.activities.FirstOrDefault().name);
                db.activity_active.Add(new_activity);

                db.SaveChanges();

                List<string> selectable_activities;

                if (lastActivities) {
                    selectable_activities = db.Database.SqlQuery<string>("SELECT name FROM activity_active GROUP BY name ORDER BY max([from]) DESC LIMIT 5").ToList();
                } else {
                    selectable_activities = db.activities.Select(a => a.name).ToList();
                    if (!selectable_activities.Contains(new_activity.name)) // If a custom activity was entered add this as an option
                        selectable_activities.Add(new_activity.name);
                }

                // Load timeout from settings
                long timeout = db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout;

                bool useNativeToast = db.settings.Find("useNativeToast") != null ? db.settings.Find("useNativeToast").value == 1 : Constants.useNativeToast;

                if (useNativeToast)
                    showNotification(
                        new_activity.id,
                        name,
                        "For which project are you using this app?",
                        selectable_activities.ToArray(),
                        new_activity.name,
                        timeout
                    );
                else
                {
                    CustomToast newToast = new CustomToast(new_activity.id.ToString(), name);
                    newToast.Show();
                }
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

        private void showNotification(long tag_long, string title, string subtitle, string[] activities, string selected, long timeout)
        {
            string tag = tag_long.ToString();
            string group = "ProjectQuestions";
            // Create the XML document (BE SURE TO REFERENCE WINDOWS.DATA.XML.DOM) 
            var doc = new XmlDocument();
            doc.LoadXml(createToast(tag_long, title, subtitle, activities, selected).GetContent());
            // And create the toast notification 
            var toast = new ToastNotification(doc);

            toast.Tag = tag;
            toast.Group = group;

            // And then show it 
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);

            Task.Delay((int)timeout).ContinueWith(_ =>
            {
                DesktopNotificationManagerCompat.History.Remove(tag, group);
            });
        }

        private static void showNotification2(long tag_long, string title, string subtitle)
        {
            string tag = tag_long.ToString();
            string group = "ProjectQuestions";
            // Create the XML document (BE SURE TO REFERENCE WINDOWS.DATA.XML.DOM) 
            var doc = new XmlDocument();
            doc.LoadXml(createToast2(tag_long, title, subtitle).GetContent());
            // And create the toast notification 
            var toast = new ToastNotification(doc);

            toast.Tag = tag + "Other Activity";
            toast.Group = group;

            // And then show it 
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);
        }

        private ToastContent createToast(long tag_long, string title, string subtitle, string[] activities, string selected)
        {
            ToastContent content = new ToastContent()
            {
                Duration = ToastDuration.Long,
                Header = new ToastHeader("792374127", "TimeTracker", "")
                {
                    Id = "792374127",
                    Title = "TimeTracker",
                    Arguments = "",
                },
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children = {
                            new AdaptiveText()
                            {
                                Text = title,
                                HintMaxLines = 1
                            },

                            new AdaptiveText()
                            {
                                Text = subtitle
                            }
                        }
                    }
                }
            };

            switch (activities.Length)
            {
                case 0:
                    content.Actions = new ToastActionsCustom()
                    {
                        Inputs = {
                        new ToastSelectionBox("activity")
                        {
                            DefaultSelectionBoxItemId = "no activity created",
                            Items =
                            {
                                new ToastSelectionBoxItem("no activity created", "no activity created")
                            }
                        }
                        },
                        Buttons = {
                            new ToastButton("Other", "action=other&activityId=" + tag_long + "&name=" + title)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
                        }
                    };
                    break;
                case 1:
                    content.Actions = new ToastActionsCustom()
                    {
                        Inputs = {
                        new ToastSelectionBox("activity")
                        {
                            DefaultSelectionBoxItemId = selected,
                            Items =
                            {
                                new ToastSelectionBoxItem(activities[0], activities[0])
                            }
                        }
                        },
                        Buttons = {
                            new ToastButton("Other", "action=other&activityId=" + tag_long + "&name=" + title)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                        }
                    };
                    break;
                case 2:
                    content.Actions = new ToastActionsCustom()
                    {
                        Inputs = {
                        new ToastSelectionBox("activity")
                        {
                            DefaultSelectionBoxItemId = selected,
                            Items =
                            {
                                new ToastSelectionBoxItem(activities[0], activities[0]),
                                new ToastSelectionBoxItem(activities[1], activities[1])
                            }
                        }
                        },
                        Buttons = {
                            new ToastButton("Other", "action=other&activityId=" + tag_long + "&name=" + title)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
                        }
                    };
                    break;
                case 3:
                    content.Actions = new ToastActionsCustom()
                    {
                        Inputs = {
                        new ToastSelectionBox("activity")
                        {
                            DefaultSelectionBoxItemId = selected,
                            Items =
                            {
                                new ToastSelectionBoxItem(activities[0], activities[0]),
                                new ToastSelectionBoxItem(activities[1], activities[1]),
                                new ToastSelectionBoxItem(activities[2], activities[2])
                            }
                        }
                        },
                        Buttons = {
                            new ToastButton("Other", "action=other&activityId=" + tag_long + "&name=" + title)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
                        }
                    };
                    break;
                case 4:
                    content.Actions = new ToastActionsCustom()
                    {
                        Inputs = {
                        new ToastSelectionBox("activity")
                        {
                            DefaultSelectionBoxItemId = selected,
                            Items =
                            {
                                new ToastSelectionBoxItem(activities[0], activities[0]),
                                new ToastSelectionBoxItem(activities[1], activities[1]),
                                new ToastSelectionBoxItem(activities[2], activities[2]),
                                new ToastSelectionBoxItem(activities[3], activities[3])
                            }
                        }
                        },
                        Buttons = {
                            new ToastButton("Other", "action=other&activityId=" + tag_long + "&name=" + title)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
                        }
                    };
                    break;
                case 5:
                    content.Actions = new ToastActionsCustom()
                    {
                        Inputs = {
                        new ToastSelectionBox("activity")
                        {
                            DefaultSelectionBoxItemId = selected,
                            Items =
                            {
                                new ToastSelectionBoxItem(activities[0], activities[0]),
                                new ToastSelectionBoxItem(activities[1], activities[1]),
                                new ToastSelectionBoxItem(activities[2], activities[2]),
                                new ToastSelectionBoxItem(activities[3], activities[3]),
                                new ToastSelectionBoxItem(activities[4], activities[4])
                            }
                        }
                        },
                        Buttons = {
                            new ToastButton("Other", "action=other&activityId=" + tag_long + "&name=" + title)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
                        }
                    };
                    break;
            }

            return content;
        }

        private static ToastContent createToast2(long tag_long, string title, string subtitle) {
            ToastContent content = new ToastContent()
            {
                Duration = ToastDuration.Long,
                Header = new ToastHeader("792374127", "TimeTracker", "")
                {
                    Id = "792374127",
                    Title = "TimeTracker",
                    Arguments = "",
                },
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children = {
                            new AdaptiveText()
                            {
                                Text = title,
                                HintMaxLines = 1
                            },

                            new AdaptiveText()
                            {
                                Text = subtitle
                            }
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Inputs = {
                         new ToastTextBox("activity")
                            {
                                PlaceholderContent = "Activity - Subactivity"
                            }
                        },
                    Buttons = {
                            new ToastButton("Cancle", "action=cancel2&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            },
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
                        }
                }
            };

            return content;
        }
    }
}
