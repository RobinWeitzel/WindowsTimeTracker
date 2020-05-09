func @_TimeTracker.App.InitializeComponent$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :46 :8) {
^entry :
br ^0

^0: // SimpleBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :51 :12) // this (ThisExpression)
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :51 :12) // this.Exit (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :51 :61) // this (ThisExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :51 :25) // new System.Windows.ExitEventHandler(this.Application_Exit) (ObjectCreationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :51 :12) // Binary expression on unsupported types this.Exit += new System.Windows.ExitEventHandler(this.Application_Exit)
// No identifier name for binary assignment expression
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.App.Main$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :60 :8) {
^entry :
br ^0

^0: // SimpleBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :64 :34) // new TimeTracker.App() (ObjectCreationExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :65 :12) // Not a variable of known type: app
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :65 :12) // app.InitializeComponent() (InvocationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :66 :12) // Not a variable of known type: app
%5 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\App.g.cs" :66 :12) // app.Run() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
