func @_TimeTracker.ManualEdit.LoadData$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :41 :8) {
^entry :
br ^0

^0: // SimpleBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :43 :25) // Not a variable of known type: StorageHandler
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :43 :25) // StorageHandler.GetLastActivities() (InvocationExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :44 :12) // Not a variable of known type: DataGrid
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :44 :12) // DataGrid.ItemsSource (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :44 :35) // Not a variable of known type: Activities
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.ManualEdit.Close_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :49 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :49 :33)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :49 :33)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :49 :48)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :49 :48)
br ^0

^0: // SimpleBlock
// Entity from another assembly: Close
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :51 :12) // Close() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.ManualEdit.Save_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :54 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :54 :32)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :54 :32)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :54 :47)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :54 :47)
br ^0

^0: // SimpleBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :56 :12) // Not a variable of known type: StorageHandler
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :56 :43) // Not a variable of known type: Activities
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :56 :12) // StorageHandler.WriteActivities(Activities) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: LoadData
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\ManualEdit.xaml.cs" :57 :12) // LoadData() (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
