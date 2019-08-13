using CefSharp;
using CefSharp.Wpf;
using System;
using TimeTracker.Helper;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für HTMLDataWindow.xaml
    /// </summary>
    public partial class HTMLDataWindow : System.Windows.Window
    {
        public HTMLDataWindow(StorageHandler storageHandler, AppStateTracker appStateTracker)
        {
            InitializeComponent();

            string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (!Cef.IsInitialized)
            {
                CefSettings CefSettings = new CefSettings();
                CefSettings.BrowserSubprocessPath = String.Format("{0}CefSharp.BrowserSubprocess.exe", CurrentDirectory); // **Path where the CefSharp.BrowserSubprocess.exe exists**
                CefSettings.CachePath = "ChromiumBrowserControlCache";
                CefSettings.IgnoreCertificateErrors = true;
                CefSettings.SetOffScreenRenderingBestPerformanceArgs();
                Cef.Initialize(CefSettings);
            }

            WebBrowser.FrameLoadEnd += (a, b) =>
            {
                WebBrowser.ShowDevTools();
            };
            WebBrowser.Address = String.Format("file:///{0}index.html", CurrentDirectory);
            WebBrowser.JavascriptObjectRepository.Register("boundAsync", new MyScriptingClass(storageHandler, appStateTracker), true);
        }
    }
}
