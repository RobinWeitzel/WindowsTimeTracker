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
using System.Windows.Threading;
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
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private bool IsExit;

        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;
        private ProgramSwitchListener ProgramSwitchListener;
        private MachineStateListener MachineStateListener;
        private HotkeyListener HotkeyListener;
        private ASDL ASDL;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Set up app to run in the background
            base.OnStartup(e);

            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            NotifyIcon = new System.Windows.Forms.NotifyIcon();

            NotifyIcon.Icon = TimeTracker.Properties.Resources.MyIcon;
            NotifyIcon.Visible = true;
            CreateContextMenu();

            StorageHandler = new StorageHandler();
            AppStateTracker = new AppStateTracker(StorageHandler);
            ProgramSwitchListener = new ProgramSwitchListener();
            MachineStateListener = new MachineStateListener();
            HotkeyListener = new HotkeyListener();
            ASDL = new ASDL(AppStateTracker, ProgramSwitchListener, MachineStateListener, HotkeyListener);

            NotifyIcon.DoubleClick += (s, args) => ASDL.ChangeActivity();
            MachineStateListener.StateChanged += ListenerEvent;

            AppStateTracker.ChangeContextMenu += (s, args) => NotifyIcon.ContextMenuStrip.Items[1].Text = ((bool)args.Value) ? "Unpause" : "Pause";
            ASDL.ShowActivityDialog += CreateActivityDialog;
            ASDL.ShowAwayFromPCDialog += CreateAwayFromPCDialog;

            ShowTutorialIfNeeded();

            CheckForUpdates();
        }

        private void ListenerEvent(object sender, CustomEventArgs e)
        {
            if ((bool)e.Value)
            {
                HotkeyListener?.Dispose();
                HotkeyListener = new HotkeyListener();

                ASDL.ReattachHotkeyListener(HotkeyListener);
            }
        }

            /*private void handleError()
            {
                MessageBoxResult result = MessageBox.Show("Malformed CSV files. Files is being repaired after which the TimeTracker will restart.");

                List<Helper.Activity> good;
                // Read in CSV wiht activities
                using (var reader = new StreamReader(Variables.activityPath))
                using (var csv = new CsvReader(reader))
                {
                    good = new List<Helper.Activity>();

                    while (csv.Read())
                    {
                        try
                        {
                            var record = csv.GetRecord<Helper.Activity>();
                            good.Add(record);
                        } catch (Exception ignore)
                        {

                        }
                    }
                }

                using (TextWriter tw = new StreamWriter(Variables.activityPath))
                {
                    var csv = new CsvWriter(tw);
                    csv.WriteRecords(good);
                }

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }*/

            private void ShowTutorialIfNeeded()
        {
            // Check if the tutorial should be shown
            if (!Settings.Default.TutorialViewed)
            {
                new Tutorial().Show();
                Settings.Default.TutorialViewed = true;
                Settings.Default.Save();
            }
        }

        private void CheckForUpdates()
        {
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

                if (!node.FirstChild.InnerXml.Equals("tag:github.com,2008:Repository/145717546/" + AppStateTracker.Version))
                {
                    new NewVersion().Show();
                }
            }
            catch (WebException ignore)
            {

            }
        }

        /************* Methods for handeling app running in background ***************/
        private void CreateContextMenu()
        {
            NotifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            NotifyIcon.ContextMenuStrip.Items.Add("Change Activity").Click += (s, e) => ASDL.ChangeActivity();
            NotifyIcon.ContextMenuStrip.Items.Add("Pause").Click += (s, e) => AppStateTracker.Pause(null);
            NotifyIcon.ContextMenuStrip.Items.Add("Do not Disturb").Click += (s, e) => DoNotDisturb();
            //NotifyIcon.ContextMenuStrip.Items.Add("View Data").Click += (s, e) => new HTMLDataWindow().Show();
            NotifyIcon.ContextMenuStrip.Items.Add("Edit Activities").Click += (s, e) => new ManualEdit(StorageHandler).Show();
            NotifyIcon.ContextMenuStrip.Items.Add("Settings").Click += (s, e) => new SettingsWindow().Show();
            NotifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            IsExit = true;
            MainWindow.Close();
            NotifyIcon.Dispose();
            NotifyIcon = null;
            HotkeyListener.Dispose();
            HotkeyListener = null;
            this.Shutdown(1);
        }

        private void CreateActivityDialog(object sender, CustomEventArgs e)
        {
            if (!CloseAllToasts())
            {
                CustomToast newToast = new CustomToast(StorageHandler, AppStateTracker, (bool)e.Value);
                newToast.Show();
                if ((bool)e.Value)
                    newToast.Activate();
            }
        }

        private bool CloseAllToasts()
        {
            bool ToastAlreadySelected = false;
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                if (App.Current.Windows[intCounter].GetType().Name.Equals("CustomToast"))
                    if (App.Current.Windows[intCounter].IsActive)
                        ToastAlreadySelected = true;
                    else
                        App.Current.Windows[intCounter].Close();

            return ToastAlreadySelected;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppStateTracker.SaveCurrentWindow();
            AppStateTracker.SaveCurrentActivity();
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
            if (!IsExit)
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not             
            }
        }

        private void CreateAwayFromPCDialog(object sender, CustomEventArgs e)
        {
            new ManualTracking(StorageHandler, AppStateTracker, (DateTime)e.Value).Show();
        }

        private void DoNotDisturb()
        {
            if (AppStateTracker.Disturb)
            {
                NotifyIcon.ContextMenuStrip.Items[2].Text = "Disable \"Do not disturb\"";
                AppStateTracker.Disturb = false;
            }
            else
            {
                NotifyIcon.ContextMenuStrip.Items[2].Text = "Do not disturb";
                AppStateTracker.Disturb = true;
            }
        }
    }
}
