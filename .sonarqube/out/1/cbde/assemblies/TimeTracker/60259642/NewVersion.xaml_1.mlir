func @_TimeTracker.NewVersion.Hyperlink_RequestNavigate$object.System.Windows.Navigation.RequestNavigateEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :33 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :33 :47)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :33 :47)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :33 :62)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :33 :62)
br ^0

^0: // SimpleBlock
// Entity from another assembly: Process
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :35 :47) // Not a variable of known type: e
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :35 :47) // e.Uri (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :35 :47) // e.Uri.AbsoluteUri (SimpleMemberAccessExpression)
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :35 :26) // new ProcessStartInfo(e.Uri.AbsoluteUri) (ObjectCreationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :35 :12) // Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)) (InvocationExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :36 :12) // Not a variable of known type: e
%8 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :36 :12) // e.Handled (SimpleMemberAccessExpression)
%9 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\NewVersion.xaml.cs" :36 :24) // true
br ^1

^1: // ExitBlock
return

}
