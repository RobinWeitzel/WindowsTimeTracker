// Skipping function OnStartup(none), it contains poisonous unsupported syntaxes

func @_TimeTracker.App.OnExit$System.Windows.ExitEventArgs$(none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :93 :8) {
^entry (%_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :93 :39)
cbde.store %_e, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :93 :39)
br ^0

^0: // BinaryBranchBlock
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :95 :16) // Not a variable of known type: _instanceMutex
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :95 :34) // null (NullLiteralExpression)
%3 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :95 :16) // comparison of unknown type: _instanceMutex != null
cond_br %3, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :95 :16)

^1: // SimpleBlock
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :96 :16) // Not a variable of known type: _instanceMutex
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :96 :16) // _instanceMutex.ReleaseMutex() (InvocationExpression)
br ^2

^2: // SimpleBlock
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :97 :12) // base (BaseExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :97 :24) // Not a variable of known type: e
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :97 :12) // base.OnExit(e) (InvocationExpression)
br ^3

^3: // ExitBlock
return

}
func @_TimeTracker.App.ShowTutorialIfNeeded$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :103 :8) {
^entry :
br ^0

^0: // BinaryBranchBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Settings
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :106 :17) // Settings.Default (SimpleMemberAccessExpression)
%1 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :106 :17) // Settings.Default.TutorialViewed (SimpleMemberAccessExpression)
%2 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :106 :16) // !Settings.Default.TutorialViewed (LogicalNotExpression)
cond_br %2, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :106 :16)

^1: // SimpleBlock
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :108 :16) // new Tutorial() (ObjectCreationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :108 :16) // new Tutorial().Show() (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Settings
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :109 :16) // Settings.Default (SimpleMemberAccessExpression)
%6 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :109 :16) // Settings.Default.TutorialViewed (SimpleMemberAccessExpression)
%7 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :109 :50) // true
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Settings
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :110 :16) // Settings.Default (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :110 :16) // Settings.Default.Save() (InvocationExpression)
br ^2

^2: // ExitBlock
return

}
// Skipping function CheckForUpdates(), it contains poisonous unsupported syntaxes

func @_TimeTracker.App.DoNotDisturb$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :156 :8) {
^entry :
br ^0

^0: // BinaryBranchBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :158 :16) // Not a variable of known type: AppStateTracker
%1 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :158 :16) // AppStateTracker.Disturb (SimpleMemberAccessExpression)
cond_br %1, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :158 :16)

^1: // SimpleBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :16) // Not a variable of known type: NotifyIcon
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :16) // NotifyIcon.ContextMenuStrip (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :16) // NotifyIcon.ContextMenuStrip.Items (SimpleMemberAccessExpression)
%5 = constant 3 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :50)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :16) // NotifyIcon.ContextMenuStrip.Items[3] (ElementAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :16) // NotifyIcon.ContextMenuStrip.Items[3].Text (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :160 :60) // "Disable \"Do not disturb\"" (StringLiteralExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :161 :16) // Not a variable of known type: AppStateTracker
%10 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :161 :16) // AppStateTracker.Disturb (SimpleMemberAccessExpression)
%11 = constant 0 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :161 :42) // false
br ^3

^2: // SimpleBlock
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :16) // Not a variable of known type: NotifyIcon
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :16) // NotifyIcon.ContextMenuStrip (SimpleMemberAccessExpression)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :16) // NotifyIcon.ContextMenuStrip.Items (SimpleMemberAccessExpression)
%15 = constant 3 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :50)
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :16) // NotifyIcon.ContextMenuStrip.Items[3] (ElementAccessExpression)
%17 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :16) // NotifyIcon.ContextMenuStrip.Items[3].Text (SimpleMemberAccessExpression)
%18 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :165 :60) // "Do not disturb" (StringLiteralExpression)
%19 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :166 :16) // Not a variable of known type: AppStateTracker
%20 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :166 :16) // AppStateTracker.Disturb (SimpleMemberAccessExpression)
%21 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :166 :42) // true
br ^3

^3: // ExitBlock
return

}
// Skipping function CreateContextMenu(), it contains poisonous unsupported syntaxes

func @_TimeTracker.App.ExitApplication$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :190 :8) {
^entry :
br ^0

^0: // SimpleBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :192 :12) // Identifier from another assembly: MainWindow
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :192 :12) // MainWindow.Close() (InvocationExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :193 :12) // Not a variable of known type: NotifyIcon
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :193 :12) // NotifyIcon.Dispose() (InvocationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :194 :25) // null (NullLiteralExpression)
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :195 :12) // Not a variable of known type: HotkeyListener
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :195 :12) // HotkeyListener.Dispose() (InvocationExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :196 :29) // null (NullLiteralExpression)
// Entity from another assembly: Shutdown
%8 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :197 :21)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :197 :12) // Shutdown(1) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.App.Application_Exit$object.System.Windows.ExitEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :207 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :207 :38)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :207 :38)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :207 :53)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :207 :53)
br ^0

^0: // BinaryBranchBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :209 :16) // Not a variable of known type: AppStateTracker
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :209 :35) // null (NullLiteralExpression)
%4 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :209 :16) // comparison of unknown type: AppStateTracker != null
cond_br %4, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :209 :16)

^1: // SimpleBlock
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :211 :16) // Not a variable of known type: AppStateTracker
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :211 :16) // AppStateTracker.SaveCurrentWindow() (InvocationExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :212 :16) // Not a variable of known type: AppStateTracker
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :212 :16) // AppStateTracker.SaveCurrentActivity() (InvocationExpression)
br ^2

^2: // ExitBlock
return

}
func @_TimeTracker.App.CloseAllToasts$$() -> i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :222 :8) {
^entry :
br ^0

^0: // ForInitializerBlock
%0 = constant 0 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :224 :40) // false
%1 = cbde.alloca i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :224 :17) // ToastAlreadySelected
cbde.store %0, %1 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :224 :17)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: App
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :34) // App.Current (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :34) // App.Current.Windows (SimpleMemberAccessExpression)
%4 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :34) // App.Current.Windows.Count (SimpleMemberAccessExpression)
%5 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :62)
%6 = subi %4, %5 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :34)
%7 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :21) // intCounter
cbde.store %6, %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :21)
br ^1

^1: // BinaryBranchBlock
%8 = cbde.load %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :65)
%9 = constant 0 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :78)
%10 = cmpi "sgt", %8, %9 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :65)
cond_br %10, ^2, ^3 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :65)

^2: // BinaryBranchBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: App
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20) // App.Current (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20) // App.Current.Windows (SimpleMemberAccessExpression)
%13 = cbde.load %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :40)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20) // App.Current.Windows[intCounter] (ElementAccessExpression)
%15 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20) // App.Current.Windows[intCounter].GetType() (InvocationExpression)
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20) // App.Current.Windows[intCounter].GetType().Name (SimpleMemberAccessExpression)
%17 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :74) // "ActivityDialog" (StringLiteralExpression)
%18 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20) // App.Current.Windows[intCounter].GetType().Name.Equals("ActivityDialog") (InvocationExpression)
cond_br %18, ^4, ^5 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :226 :20)

^4: // BinaryBranchBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: App
%19 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :227 :24) // App.Current (SimpleMemberAccessExpression)
%20 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :227 :24) // App.Current.Windows (SimpleMemberAccessExpression)
%21 = cbde.load %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :227 :44)
%22 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :227 :24) // App.Current.Windows[intCounter] (ElementAccessExpression)
%23 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :227 :24) // App.Current.Windows[intCounter].IsActive (SimpleMemberAccessExpression)
cond_br %23, ^6, ^7 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :227 :24)

^6: // SimpleBlock
%24 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :228 :47) // true
cbde.store %24, %1 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :228 :24)
br ^5

^7: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: App
%25 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :230 :24) // App.Current (SimpleMemberAccessExpression)
%26 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :230 :24) // App.Current.Windows (SimpleMemberAccessExpression)
%27 = cbde.load %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :230 :44)
%28 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :230 :24) // App.Current.Windows[intCounter] (ElementAccessExpression)
%29 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :230 :24) // App.Current.Windows[intCounter].Close() (InvocationExpression)
br ^5

^5: // SimpleBlock
%30 = cbde.load %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :81)
%31 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :81)
%32 = subi %30, %31 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :81)
cbde.store %32, %7 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :225 :81)
br ^1

^3: // JumpBlock
%33 = cbde.load %1 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :232 :19)
return %33 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :232 :12)

^8: // ExitBlock
cbde.unreachable

}
// Skipping function CreateActivityDialog(none, none), it contains poisonous unsupported syntaxes

func @_TimeTracker.App.CreateAwayFromPCDialog$object.TimeTracker.CustomEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :262 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :262 :44)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :262 :44)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :262 :59)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :262 :59)
br ^0

^0: // SimpleBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :33) // Not a variable of known type: StorageHandler
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :49) // Not a variable of known type: AppStateTracker
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :76) // Not a variable of known type: e
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :76) // e.Value (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :66) // (DateTime)e.Value (CastExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :12) // new AwayFromPCDialog(StorageHandler, AppStateTracker, (DateTime)e.Value) (ObjectCreationExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\App.xaml.cs" :264 :12) // new AwayFromPCDialog(StorageHandler, AppStateTracker, (DateTime)e.Value).Show() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function ListenerEvent(none, none), it contains poisonous unsupported syntaxes

