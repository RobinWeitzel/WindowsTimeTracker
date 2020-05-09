// Skipping function SetupClose(), it contains poisonous unsupported syntaxes

// Skipping function SetNewActivity(none, i1), it contains poisonous unsupported syntaxes

func @_TimeTracker.ActivityDialog.Window_Activated$object.System.EventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :143 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :143 :38)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :143 :38)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :143 :53)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :143 :53)
br ^0

^0: // SimpleBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :145 :12) // Not a variable of known type: ComboBox
%3 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :145 :12) // ComboBox.IsTextSearchEnabled (SimpleMemberAccessExpression)
%4 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :145 :43) // true
%5 = constant 0 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :146 :32) // false
%6 = constant 0 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :147 :39)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :148 :32) // Not a variable of known type: ComboBox
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :148 :32) // ComboBox.Text (SimpleMemberAccessExpression)
%9 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :148 :32) // ComboBox.Text.Length (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :149 :12) // Not a variable of known type: CancelClose
%11 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :149 :29) // true
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :149 :12) // CancelClose.Push(true) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.ActivityDialog.Window_Deactivated$object.System.EventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :152 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :152 :40)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :152 :40)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :152 :55)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :152 :55)
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: SetupClose
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :154 :12) // SetupClose() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.ActivityDialog.Button_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :157 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :157 :34)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :157 :34)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :157 :49)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :157 :49)
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: SetNewActivity
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :159 :12) // SetNewActivity() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.ActivityDialog.CloseButton_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :162 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :162 :39)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :162 :39)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :162 :54)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :162 :54)
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: SetNewActivity
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :164 :12) // SetNewActivity() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.ActivityDialog.ConfirmButton_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :167 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :167 :41)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :167 :41)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :167 :56)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :167 :56)
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: SetNewActivity
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :169 :27) // Not a variable of known type: ComboBox
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :169 :27) // ComboBox.Text (SimpleMemberAccessExpression)
%4 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :169 :42) // true
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ActivityDialog.xaml.cs" :169 :12) // SetNewActivity(ComboBox.Text, true) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function ComboBox_OnKeyUp(none, none), it contains poisonous unsupported syntaxes

