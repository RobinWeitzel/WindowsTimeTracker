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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Xml;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : System.Windows.Application
    {
        /* Variables */
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;
        private ProgramSwitchListener ProgramSwitchListener;
        private MachineStateListener MachineStateListener;
        private HotkeyListener HotkeyListener;
        private ASDL ASDL;

        /// <summary>
        /// Sets up the base of the application.
        /// Everything is coordinated from here.
        /// </summary>
        /// <param name="e">The startup event</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set up app to run in the background
            base.OnStartup(e);

            // Sets up the main window
            MainWindow = new MainWindow();

            // Sets up the taskbar icon and the menu that show if you left-click on it
            NotifyIcon = new System.Windows.Forms.NotifyIcon();
            NotifyIcon.Icon = new Icon(TimeTracker.Properties.Resources.icon, SystemInformation.SmallIconSize);
            NotifyIcon.Visible = true;
            CreateContextMenu();

            // Creates classes needed to track activities and windows
            StorageHandler = new StorageHandler();
            AppStateTracker = new AppStateTracker(StorageHandler);
            ProgramSwitchListener = new ProgramSwitchListener();
            MachineStateListener = new MachineStateListener();
            HotkeyListener = new HotkeyListener();
            ASDL = new ASDL(AppStateTracker, ProgramSwitchListener, MachineStateListener, HotkeyListener);

            // Attaches listeners
            NotifyIcon.DoubleClick += (s, args) => ASDL.ChangeActivity();
            MachineStateListener.StateChanged += ListenerEvent;
            AppStateTracker.ChangeContextMenu += (s, args) => NotifyIcon.ContextMenuStrip.Items[2].Text = ((bool)args.Value) ? "Unpause" : "Pause";
            ASDL.ShowActivityDialog += CreateActivityDialog;
            ASDL.ShowAwayFromPCDialog += CreateAwayFromPCDialog;

            ShowTutorialIfNeeded();

            CheckForUpdates();
        }

        /// <summary>
        /// Show the tutorail if it has never been shown before.
        /// </summary>
        private void ShowTutorialIfNeeded()
        {
            // Check if the tutorial should be shown
            if (true || !Settings.Default.TutorialViewed)
            {
                new Tutorial().Show();
                Settings.Default.TutorialViewed = true;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Checks Github (https://github.com/RobinWeitzel/WindowsTimeTracker/releases) if a new release is available.
        /// </summary>
        private void CheckForUpdates()
        {
            try
            {
                string Url = "https://github.com/RobinWeitzel/WindowsTimeTracker/releases.atom";
                string XmlStr;

                using (var wc = new WebClient())
                {
                    XmlStr = wc.DownloadString(Url);
                }

                System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
                XmlDoc.LoadXml(XmlStr);

                XmlNode Root = XmlDoc.DocumentElement;

                // Add the namespace.  
                XmlNamespaceManager Nsmgr = new XmlNamespaceManager(XmlDoc.NameTable);
                Nsmgr.AddNamespace("f", "http://www.w3.org/2005/Atom");

                XmlNode Node = Root.SelectSingleNode("descendant::f:entry", Nsmgr);

                // If the current version is not queal to the newest version
                if (!Node.FirstChild.InnerXml.Equals("tag:github.com,2008:Repository/145717546/" + AppStateTracker.Version))
                {
                    new NewVersion().Show();
                }
            }
            catch (WebException ignore) // Triggered if the user has no internet in which case the error should be ignored (no point in checking for an update without internet)
            {

            }
        }

        /// <summary>
        /// Toggles the do-not-disturb mode.
        /// In this mode, the TimeTracker keeps tracking but no longer asks the user if he is working on different activity.
        /// </summary>
        private void DoNotDisturb()
        {
            if (AppStateTracker.Disturb)
            {
                NotifyIcon.ContextMenuStrip.Items[3].Text = "Disable \"Do not disturb\"";
                AppStateTracker.Disturb = false;
            }
            else
            {
                NotifyIcon.ContextMenuStrip.Items[3].Text = "Do not disturb";
                AppStateTracker.Disturb = true;
            }
        }

        /// <summary>
        /// Sets up the taskbar menu, attaching all relevant menu items and events.
        /// </summary>
        private void CreateContextMenu()
        {
            NotifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            NotifyIcon.ContextMenuStrip.Items.Add("Open").Click += (s, e) => new HTMLDataWindow(StorageHandler, AppStateTracker).Show();
            NotifyIcon.ContextMenuStrip.Items.Add("Change Activity").Click += (s, e) => ASDL.ChangeActivity();
            NotifyIcon.ContextMenuStrip.Items.Add("Pause").Click += (s, e) => AppStateTracker.Pause(null);
            NotifyIcon.ContextMenuStrip.Items.Add("Do not Disturb").Click += (s, e) => DoNotDisturb();
            NotifyIcon.ContextMenuStrip.Items.Add("Edit Activities").Click += (s, e) => new ManualEdit(StorageHandler).Show();
            NotifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();

            ToolStripItem Item = NotifyIcon.ContextMenuStrip.Items[0];
            Item.Font = new Font(Item.Font, System.Drawing.FontStyle.Bold);
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        private void ExitApplication()
        {
            MainWindow.Close();
            NotifyIcon.Dispose();
            NotifyIcon = null;
            HotkeyListener.Dispose();
            HotkeyListener = null;
            Shutdown(1);
        }


        /// <summary>
        /// Event called when the application is about to exit.
        /// Saves the current window and activity.
        /// </summary>
        /// <param name="sender">The class that sent the event</param>
        /// <param name="e">The event</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppStateTracker.SaveCurrentWindow();
            AppStateTracker.SaveCurrentActivity();
        }

        /// <summary>
        /// Closes all dialogs (so that no more than one is shown).
        /// If a dialog is currently selected it is not closed (because that means the user is interacting with it).
        /// In that case, the method returns True.
        /// </summary>
        /// <returns>True, if the user is currently interacting with an activity dialog, otherwise False</returns>
        private bool CloseAllToasts()
        {
            bool ToastAlreadySelected = false;
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                if (App.Current.Windows[intCounter].GetType().Name.Equals("ActivityDialog"))
                    if (App.Current.Windows[intCounter].IsActive)
                        ToastAlreadySelected = true;
                    else
                        App.Current.Windows[intCounter].Close();

            return ToastAlreadySelected;
        }

        /// <summary>
        /// Displays the activity dialog.
        /// </summary>
        /// <param name="sender">The class that triggered the event</param>
        /// <param name="e">The event</param>
        private void CreateActivityDialog(object sender, CustomEventArgs e)
        {
            try
            {
                if (!CloseAllToasts()) // If the users is currently not interacting with a dialog.
                {
                    ActivityDialog newToast = new ActivityDialog(StorageHandler, AppStateTracker, (bool)e.Value);
                    newToast.Show();
                    if ((bool)e.Value)
                        newToast.Activate();
                }
            } catch (Exception ignore)
            {

            }
        }

        /// <summary>
        /// Show the away from PC dialog.
        /// </summary>
        /// <param name="sender">The class that sent the event</param>
        /// <param name="e">The event</param>
        private void CreateAwayFromPCDialog(object sender, CustomEventArgs e)
        {
            new AwayFromPCDialog(StorageHandler, AppStateTracker, (DateTime)e.Value).Show();
        }

        /// <summary>
        /// Called when the machine state changes.
        /// Needed to reattach the hotkey listener after a machine went to sleep and woke up again.
        /// </summary>
        /// <param name="sender">The class that triggered the event</param>
        /// <param name="e">The event</param>
        private void ListenerEvent(object sender, CustomEventArgs e)
        {
            if ((bool)e.Value) // True if the event is a log in/wake up
            {
                HotkeyListener?.Dispose();
                HotkeyListener = new HotkeyListener();

                ASDL.ReattachHotkeyListener(HotkeyListener);
            }
        }
    }
}
