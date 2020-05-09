// Skipping function Pause(none), it contains poisonous unsupported syntaxes

func @_TimeTracker.AppStateTracker.CreateCurrentWindow$string.string$(none, none) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :214 :8) {
^entry (%_name : none, %_details : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :214 :42)
cbde.store %_name, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :214 :42)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :214 :55)
cbde.store %_details, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :214 :55)
br ^0

^0: // JumpBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :216 :28) // new Window() (ObjectCreationExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :217 :12) // Not a variable of known type: Window
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :217 :12) // Window.Name (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :217 :26) // Not a variable of known type: name
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :218 :12) // Not a variable of known type: Window
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :218 :12) // Window.Details (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :218 :29) // Not a variable of known type: details
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :219 :12) // Not a variable of known type: Window
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :219 :12) // Window.From (SimpleMemberAccessExpression)
// Entity from another assembly: DateTime
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :219 :26) // DateTime.Now (SimpleMemberAccessExpression)
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :221 :28) // Not a variable of known type: Window
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :223 :19) // Not a variable of known type: Window
return %14 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\AppStateTracker.cs" :223 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function CreateCurrentActivity(none, none), it contains poisonous unsupported syntaxes

// Skipping function SaveCurrentWindow(none), it contains poisonous unsupported syntaxes

// Skipping function SaveCurrentActivity(none), it contains poisonous unsupported syntaxes

// Skipping function AssignColors(), it contains poisonous unsupported syntaxes

