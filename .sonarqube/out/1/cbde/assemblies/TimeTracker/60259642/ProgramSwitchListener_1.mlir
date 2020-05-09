func @_TimeTracker.ProgramSwitchListener.GetActiveWindowTitle$System.IntPtr$(none) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :63 :8) {
^entry (%_handle : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :63 :44)
cbde.store %_handle, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :63 :44)
br ^0

^0: // BinaryBranchBlock
%1 = constant 256 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :65 :31)
%2 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :65 :22) // NChars
cbde.store %1, %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :65 :22)
%3 = cbde.load %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :66 :51)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :66 :33) // new StringBuilder(NChars) (ObjectCreationExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :67 :16) // Not a variable of known type: handle
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :67 :16) // handle.ToInt64() (InvocationExpression)
%8 = constant 0 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :67 :36)
%9 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :67 :16) // comparison of unknown type: handle.ToInt64() == 0
cond_br %9, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :67 :16)

^1: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetForegroundWindow
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :68 :25) // GetForegroundWindow() (InvocationExpression)
br ^2

^2: // BinaryBranchBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetWindowText
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :30) // Not a variable of known type: handle
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :38) // Not a variable of known type: Buff
%13 = cbde.load %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :44)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :16) // GetWindowText(handle, Buff, NChars) (InvocationExpression)
%15 = constant 0 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :54)
%16 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :16) // comparison of unknown type: GetWindowText(handle, Buff, NChars) > 0
cond_br %16, ^3, ^4 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :70 :16)

^3: // JumpBlock
%17 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :72 :23) // Not a variable of known type: Buff
%18 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :72 :23) // Buff.ToString() (InvocationExpression)
return %18 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :72 :16)

^4: // JumpBlock
%19 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :74 :19) // null (NullLiteralExpression)
return %19 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\ProgramSwitchListener.cs" :74 :12)

^5: // ExitBlock
cbde.unreachable

}
// Skipping function WinEventProc(none, none, none, i32, i32, none, none), it contains poisonous unsupported syntaxes

