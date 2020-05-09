func @_TimeTracker.HotkeyListener.Dispose$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\HotkeyListener.cs" :66 :8) {
^entry :
br ^0

^0: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: UnhookWindowsHookEx
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\HotkeyListener.cs" :68 :32) // Not a variable of known type: HookId
%1 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Logic\\Listeners\\HotkeyListener.cs" :68 :12) // UnhookWindowsHookEx(HookId) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function OnKeyCombinationPressed(none), it contains poisonous unsupported syntaxes

// Skipping function SetHook(none), it contains poisonous unsupported syntaxes

// Skipping function HookCallback(i32, none, none), it contains poisonous unsupported syntaxes

