using CefSharp;
using CefSharp.Wpf;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                Cef.Initialize(CefSettings);
            }
            
            WebBrowser.Address = String.Format("file:///{0}DataView.html", CurrentDirectory);
            WebBrowser.JavascriptObjectRepository.Register("boundAsync", new MyScriptingClass(storageHandler, appStateTracker), true);
        }
    }
}
