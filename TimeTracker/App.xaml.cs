using DesktopNotifications;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private string[] blacklist = { "TimeTracker" };
        private SettingsWindow SettingsWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Set up app to run in the background
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            SettingsWindow = new SettingsWindow();
            SettingsWindow.Closing += SettingsWindow_Closing;


            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
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

        }

        /************* Methods for handeling app running in background ***************/
        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Settings").Click += (s, e) => ShowSettingsWindow();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();

        }
        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
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
                SettingsWindow.redrawList();
                SettingsWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true; MainWindow.Hide(); // A hidden window can be shown again, a closed one not             
            }
        }

        private void SettingsWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true; SettingsWindow.Hide(); // A hidden window can be shown again, a closed one not             
            }
        }

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
                        // Perform a normal launch
                        OpenWindowIfNeeded();
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

                            db.SaveChanges();
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

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (GetActiveWindowTitle() == null)
            {
                return;
            }

            string[] arr = GetActiveWindowTitle().Split(new string[] { "- " }, StringSplitOptions.None);
            if (arr.Length >= 1)
            {
                string name = arr.Last();
                // Stop if the current activity is blacklisted
                if (blacklist.Contains(arr.Last())) {
                    return;
                }

                using (mainEntities db = new mainEntities())
                {
                    // Determine if this window has been used in the last 5 minutes
                    bool hasBeenSeen = false;
                    int timeNotUsed = 1000 * 60 * 5; // 5 minutes

                    List<window_active> old_windows = db.window_active.Where(wa => wa.name.Equals(name)).ToList();

                    if (old_windows != null)
                    {
                        hasBeenSeen = timeNotUsed > DateTime.Now.Subtract(old_windows.Max(wa => wa.to) ?? DateTime.Now.AddDays(-9999)).TotalMilliseconds;
                    }

                    /* Handle active window */
                    // Set to date for old window if one exists
                    window_active old_window = db.window_active.Where(wa => wa.to == null).FirstOrDefault();
                    if (old_window != null)
                    {
                        old_window.to = DateTime.Now;
                    }

                    window_active new_window = new window_active();

                    new_window.name = arr.Last();
                    new_window.from = DateTime.Now;

                    Console.WriteLine("Window: " + name);
                    if (arr.Length >= 2)
                    {
                        Console.WriteLine("Additional information: " + string.Join("- ", arr.Take(arr.Length - 1)));
                        new_window.details = string.Join("- ", arr.Take(arr.Length - 1));
                    }

                    db.window_active.Add(new_window);

                    // Show notification if app has not been seen in last few minutes
                    if(!hasBeenSeen)
                    {
                        /* Handle active activity */
                        activity_active old_activity = db.activity_active.Where(aa => aa.to == null).FirstOrDefault();
                        if (old_activity != null)
                        {
                            old_activity.to = DateTime.Now;
                        }

                        activity_active new_activity = new activity_active();
                        new_activity.from = DateTime.Now;
                        new_activity.name = old_activity.name ?? db.activities.FirstOrDefault().name;
                        db.activity_active.Add(new_activity);

                        db.SaveChanges();

                        showNotification(new_activity.id, arr.Last(), "For which project are you using this app?", db.activities.Select(a => a.name).ToArray(), new_activity.name);

                    } else
                    {
                        db.SaveChanges();
                    }
                }
            }
        }

        private void showNotification(long tag_long, string title, string subtitle, string[] activities, string selected)
        {
            string tag = tag_long.ToString();
            string group = "ProjectQuestions";
            // Create the XML document (BE SURE TO REFERENCE WINDOWS.DATA.XML.DOM) 
            var doc = new XmlDocument();
            doc.LoadXml(createToast(tag_long, title, subtitle, activities, selected).GetContent());
            // And create the toast notification 
            var toast = new ToastNotification(doc);
            int timeout = 10000;
            
            toast.Tag = tag;
            toast.Group = group;
            toast.Data = new NotificationData();
            toast.Data.Values["progressValue"] = "0";
            toast.Data.Values["progressValueString"] = Math.Round(timeout / 1000.0).ToString() + " Second(s)";

            // And then show it 
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);

            /*
            Timer timer = new Timer();
            uint counter = 1;
            int intervall = 1000;
            timer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) =>
            {
                if (timeout / intervall < counter)
                {
                    DesktopNotificationManagerCompat.History.Remove(tag, group);
                    timer.Stop();
                }
                else
                {
                    string val = (counter / (timeout / (double)intervall)).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                    string timeRemaining = Math.Round((timeout - ((double)intervall * counter)) / 1000).ToString();
                    UpdateProgressBar(counter, val, timeRemaining, tag, group);
                    counter++;
                }
            });

            timer.Interval = intervall;
            timer.Enabled = true;

            timer.Start();*/

            Task.Delay(timeout).ContinueWith(_ =>
            {
                DesktopNotificationManagerCompat.History.Remove(tag, group);
            });

        }

        /*private void UpdateProgressBar(uint seq, string val, string timeRemaining, string tag, string group)
        {
            var data = new NotificationData
            {
                SequenceNumber = seq
            };

            data.Values["progressValue"] = val;
            data.Values["progressValueString"] = timeRemaining + " Second(s)";

            DesktopNotificationManagerCompat.CreateToastNotifier().Update(data, tag, group);
        }*/

        private ToastContent createToast(long tag_long, string title, string subtitle, string[] activities, string selected)
        {
            ToastContent content = new ToastContent()
            {
                Duration = ToastDuration.Short,
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
                            },
                            /*new AdaptiveProgressBar()
                            {
                                Title = "Time until automatically dismissed",
                                Value = new BindableProgressBarValue("progressValue"),
                                ValueStringOverride = new BindableString("progressValueString"),
                                Status = ""
                            }*/
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
                            new ToastButton("Confirm", "action=dismiss&activityId=" + tag_long)
                            {
                                ActivationType = ToastActivationType.Background,
                            }
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
    }
}
