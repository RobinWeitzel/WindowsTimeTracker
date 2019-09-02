using CefSharp;
using CefSharp.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using TimeTracker.Helper;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für HTMLDataWindow.xaml
    /// </summary>
    public partial class HTMLDataWindow : System.Windows.Window
    {
        public ChromiumWebBrowser ChromeBrowser;

        public HTMLDataWindow(StorageHandler storageHandler, AppStateTracker appStateTracker)
        {
            InitializeComponent();
            InitializeChromium(storageHandler, appStateTracker);
        }

        public void InitializeChromium(StorageHandler storageHandler, AppStateTracker appStateTracker)
        {
            string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!Cef.IsInitialized)
            {
                CefSettings Settings = new CefSettings();
                Settings.IgnoreCertificateErrors = true;
                Settings.BrowserSubprocessPath = String.Format("{0}CefSharp.BrowserSubprocess.exe", CurrentDirectory);

                // Initialize cef with the provided settings
                Cef.Initialize(Settings, performDependencyCheck: false, browserProcessHandler: null);
            }
            // Create a browser component
            String Page = string.Format(@"{0}\index.html", CurrentDirectory);
            ChromeBrowser = new ChromiumWebBrowser(Page);

            // Add it to the form and fill it to the form window.
            ChromeBrowser.Dock = DockStyle.Fill;

            // Allow the use of local resources in the browser
            BrowserSettings BrowserSettings = new BrowserSettings();
            BrowserSettings.FileAccessFromFileUrls = CefState.Enabled;
            BrowserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            ChromeBrowser.BrowserSettings = BrowserSettings;
            ChromeBrowser.JavascriptObjectRepository.Register("boundAsync", new MyScriptingClass(storageHandler, appStateTracker), true);

            //ChromeBrowser.FrameLoadEnd += (e, r) => ChromeBrowser.ShowDevTools();

            WindowsFormsHost.Child = ChromeBrowser;
        }
    }
}
