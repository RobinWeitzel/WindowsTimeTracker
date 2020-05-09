func @_TimeTracker.ASDL.ReattachHotkeyListener$TimeTracker.HotkeyListener$(none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :47 :8) {
^entry (%_hotkeyListener : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :47 :43)
cbde.store %_hotkeyListener, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :47 :43)
br ^0

^0: // SimpleBlock
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :49 :12) // Not a variable of known type: hotkeyListener
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :49 :12) // hotkeyListener.KeyCombinationPressed (SimpleMemberAccessExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: ListenerEvent
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :49 :12) // Binary expression on unsupported types hotkeyListener.KeyCombinationPressed += ListenerEvent
// No identifier name for binary assignment expression
br ^1

^1: // ExitBlock
return

}
// Skipping function ListenerEvent(none, none), it contains poisonous unsupported syntaxes

// Skipping function ActivitySwitched(none), it contains poisonous unsupported syntaxes

func @_TimeTracker.ASDL.ChangeActivity$bool$(i1) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :151 :8) {
^entry (%_focusToast : i1):
%0 = cbde.alloca i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :151 :35)
cbde.store %_focusToast, %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :151 :35)
br ^0

^0: // SimpleBlock
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :153 :12) // Not a variable of known type: AppStateTracker
%2 = constant 0 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :153 :34) // false
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :153 :12) // AppStateTracker.Pause(false) (InvocationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :154 :12) // Not a variable of known type: ShowActivityDialog
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :154 :38) // this (ThisExpression)
%6 = cbde.load %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :154 :64)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :154 :44) // new CustomEventArgs(focusToast) (ObjectCreationExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\ASDL.cs" :154 :12) // ShowActivityDialog.Invoke(this, new CustomEventArgs(focusToast)) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
