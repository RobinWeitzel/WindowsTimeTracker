func @_TimeTracker.HTMLDataWindow.InitializeChromium$TimeTracker.StorageHandler.TimeTracker.AppStateTracker$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :22 :8) {
^entry (%_storageHandler : none, %_appStateTracker : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :22 :39)
cbde.store %_storageHandler, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :22 :39)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :22 :70)
cbde.store %_appStateTracker, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :22 :70)
br ^0

^0: // BinaryBranchBlock
// Entity from another assembly: AppDomain
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :24 :38) // AppDomain.CurrentDomain (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :24 :38) // AppDomain.CurrentDomain.BaseDirectory (SimpleMemberAccessExpression)
// Entity from another assembly: Cef
%5 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :25 :17) // Cef.IsInitialized (SimpleMemberAccessExpression)
%6 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :25 :16) // !Cef.IsInitialized (LogicalNotExpression)
cond_br %6, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :25 :16)

^1: // SimpleBlock
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :27 :39) // new CefSettings() (ObjectCreationExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :28 :16) // Not a variable of known type: Settings
%10 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :28 :16) // Settings.IgnoreCertificateErrors (SimpleMemberAccessExpression)
%11 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :28 :51) // true
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :29 :16) // Not a variable of known type: Settings
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :29 :16) // Settings.BrowserSubprocessPath (SimpleMemberAccessExpression)
// Entity from another assembly: String
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :29 :63) // "{0}CefSharp.BrowserSubprocess.exe" (StringLiteralExpression)
%15 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :29 :100) // Not a variable of known type: CurrentDirectory
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :29 :49) // String.Format("{0}CefSharp.BrowserSubprocess.exe", CurrentDirectory) (InvocationExpression)
// Entity from another assembly: Cef
%17 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :32 :31) // Not a variable of known type: Settings
%18 = constant 0 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :32 :65) // false
%19 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :32 :95) // null (NullLiteralExpression)
%20 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :32 :16) // Cef.Initialize(Settings, performDependencyCheck: false, browserProcessHandler: null) (InvocationExpression)
br ^2

^2: // SimpleBlock
%21 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :35 :26) // string (PredefinedType)
%22 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :35 :40) // @"{0}\index.html" (StringLiteralExpression)
%23 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :35 :59) // Not a variable of known type: CurrentDirectory
%24 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :35 :26) // string.Format(@"{0}\index.html", CurrentDirectory) (InvocationExpression)
%26 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :36 :51) // Not a variable of known type: Page
%27 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :36 :28) // new ChromiumWebBrowser(Page) (ObjectCreationExpression)
%28 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :39 :12) // Not a variable of known type: ChromeBrowser
%29 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :39 :12) // ChromeBrowser.Dock (SimpleMemberAccessExpression)
// Entity from another assembly: DockStyle
%30 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :39 :33) // DockStyle.Fill (SimpleMemberAccessExpression)
%31 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :42 :46) // new BrowserSettings() (ObjectCreationExpression)
%33 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :43 :12) // Not a variable of known type: BrowserSettings
%34 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :43 :12) // BrowserSettings.FileAccessFromFileUrls (SimpleMemberAccessExpression)
// Entity from another assembly: CefState
%35 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :43 :53) // CefState.Enabled (SimpleMemberAccessExpression)
%36 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :44 :12) // Not a variable of known type: BrowserSettings
%37 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :44 :12) // BrowserSettings.UniversalAccessFromFileUrls (SimpleMemberAccessExpression)
// Entity from another assembly: CefState
%38 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :44 :58) // CefState.Enabled (SimpleMemberAccessExpression)
%39 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :45 :12) // Not a variable of known type: ChromeBrowser
%40 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :45 :12) // ChromeBrowser.BrowserSettings (SimpleMemberAccessExpression)
%41 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :45 :44) // Not a variable of known type: BrowserSettings
%42 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :12) // Not a variable of known type: ChromeBrowser
%43 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :12) // ChromeBrowser.JavascriptObjectRepository (SimpleMemberAccessExpression)
%44 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :62) // "boundAsync" (StringLiteralExpression)
%45 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :97) // Not a variable of known type: storageHandler
%46 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :113) // Not a variable of known type: appStateTracker
%47 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :76) // new MyScriptingClass(storageHandler, appStateTracker) (ObjectCreationExpression)
%48 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :131) // true
%49 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :46 :12) // ChromeBrowser.JavascriptObjectRepository.Register("boundAsync", new MyScriptingClass(storageHandler, appStateTracker), true) (InvocationExpression)
%50 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :50 :12) // Not a variable of known type: WindowsFormsHost
%51 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :50 :12) // WindowsFormsHost.Child (SimpleMemberAccessExpression)
%52 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\HTMLDataWindow.xaml.cs" :50 :37) // Not a variable of known type: ChromeBrowser
br ^3

^3: // ExitBlock
return

}
