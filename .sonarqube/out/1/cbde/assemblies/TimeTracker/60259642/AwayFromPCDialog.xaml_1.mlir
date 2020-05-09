func @_TimeTracker.AwayFromPCDialog.SetNewActivity$string$(none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :92 :8) {
^entry (%_name : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :92 :36)
cbde.store %_name, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :92 :36)
br ^0

^0: // SimpleBlock
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :94 :12) // Not a variable of known type: AppStateTracker
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :94 :50) // Not a variable of known type: name
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :94 :56) // Not a variable of known type: FromDate
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :94 :12) // AppStateTracker.CreateCurrentActivity(name, FromDate) (InvocationExpression)
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :95 :12) // Not a variable of known type: AppStateTracker
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :95 :48) // Not a variable of known type: ToDate
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :95 :12) // AppStateTracker.SaveCurrentActivity(ToDate) (InvocationExpression)
// Entity from another assembly: Close
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :96 :12) // Close() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.AwayFromPCDialog.ConfirmButton_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :101 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :101 :41)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :101 :41)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :101 :56)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :101 :56)
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: SetNewActivity
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :103 :27) // Not a variable of known type: ComboBox
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :103 :27) // ComboBox.Text (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :103 :12) // SetNewActivity(ComboBox.Text) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.AwayFromPCDialog.ComboBox_OnKeyUp$object.System.Windows.Input.KeyEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :106 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :106 :38)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :106 :38)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :106 :53)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :106 :53)
br ^0

^0: // BinaryBranchBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :108 :16) // Not a variable of known type: e
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :108 :16) // e.Key (SimpleMemberAccessExpression)
// Entity from another assembly: Key
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :108 :25) // Key.Enter (SimpleMemberAccessExpression)
%5 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :108 :16) // comparison of unknown type: e.Key == Key.Enter
cond_br %5, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :108 :16)

^1: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: SetNewActivity
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :110 :31) // Not a variable of known type: ComboBox
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :110 :31) // ComboBox.Text (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :110 :16) // SetNewActivity(ComboBox.Text) (InvocationExpression)
br ^2

^2: // ExitBlock
return

}
func @_TimeTracker.AwayFromPCDialog.Button_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :114 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :114 :34)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :114 :34)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :114 :49)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :114 :49)
br ^0

^0: // SimpleBlock
// Entity from another assembly: Close
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\AwayFromPCDialog.xaml.cs" :116 :12) // Close() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
