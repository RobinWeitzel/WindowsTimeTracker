func @_TimeTracker.ActivityDialog.InitializeComponent$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :97 :8) {
^entry :
br ^0

^0: // BinaryBranchBlock
%0 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :100 :16) // Not a variable of known type: _contentLoaded
cond_br %0, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :100 :16)

^1: // JumpBlock
return loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :101 :16)

^2: // SimpleBlock
%1 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :103 :29) // true
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :104 :56) // "/TimeTracker;component/activitydialog.xaml" (StringLiteralExpression)
// Entity from another assembly: System
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :104 :102) // System.UriKind (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :104 :102) // System.UriKind.Relative (SimpleMemberAccessExpression)
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :104 :41) // new System.Uri("/TimeTracker;component/activitydialog.xaml", System.UriKind.Relative) (ObjectCreationExpression)
// Entity from another assembly: System
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :107 :12) // System.Windows.Application (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :107 :53) // this (ThisExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :107 :59) // Not a variable of known type: resourceLocater
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\obj\\x64\\Debug\\ActivityDialog.g.cs" :107 :12) // System.Windows.Application.LoadComponent(this, resourceLocater) (InvocationExpression)
br ^3

^3: // ExitBlock
return

}
// Skipping function Connect(i32, none), it contains poisonous unsupported syntaxes

