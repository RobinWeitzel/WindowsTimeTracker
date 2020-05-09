// Skipping function GetDayDataAsync(none, i32), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetDayData$string.int$(none, i32) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :81 :8) {
^entry (%_date : none, %_counter : i32):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :81 :33)
cbde.store %_date, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :81 :33)
%1 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :81 :46)
cbde.store %_counter, %1 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :81 :46)
br ^0

^0: // BinaryBranchBlock
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :83 :16) // Not a variable of known type: GetDayDataThread
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :83 :36) // null (NullLiteralExpression)
%4 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :83 :16) // comparison of unknown type: GetDayDataThread != null
cond_br %4, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :83 :16)

^1: // SimpleBlock
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :85 :16) // Not a variable of known type: GetDayDataThread
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :85 :16) // GetDayDataThread.Abort() (InvocationExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :86 :35) // null (NullLiteralExpression)
br ^2

^2: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetDayDataAsync
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :89 :39) // Not a variable of known type: date
%9 = cbde.load %1 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :89 :45)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :89 :23) // GetDayDataAsync(date, counter) (InvocationExpression)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :91 :19) // Not a variable of known type: task
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :91 :19) // task.Result (SimpleMemberAccessExpression)
return %13 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :91 :12)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function GetWeekBreakdownDataAsync(none, i32, i32), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetWeekBreakdownData$string.int.int$(none, i32, i32) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :8) {
^entry (%_date : none, %_daysBack : i32, %_counter : i32):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :43)
cbde.store %_date, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :43)
%1 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :56)
cbde.store %_daysBack, %1 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :56)
%2 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :70)
cbde.store %_counter, %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :171 :70)
br ^0

^0: // BinaryBranchBlock
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :173 :16) // Not a variable of known type: GetWeekBreakdownDataThread
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :173 :46) // null (NullLiteralExpression)
%5 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :173 :16) // comparison of unknown type: GetWeekBreakdownDataThread != null
cond_br %5, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :173 :16)

^1: // SimpleBlock
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :175 :16) // Not a variable of known type: GetWeekBreakdownDataThread
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :175 :16) // GetWeekBreakdownDataThread.Abort() (InvocationExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :176 :45) // null (NullLiteralExpression)
br ^2

^2: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetWeekBreakdownDataAsync
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :179 :49) // Not a variable of known type: date
%10 = cbde.load %1 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :179 :55)
%11 = cbde.load %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :179 :65)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :179 :23) // GetWeekBreakdownDataAsync(date, daysBack, counter) (InvocationExpression)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :181 :19) // Not a variable of known type: task
%15 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :181 :19) // task.Result (SimpleMemberAccessExpression)
return %15 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :181 :12)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function GetWeekSumDataAsync(none, i32, i32), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetWeekSumData$string.int.int$(none, i32, i32) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :8) {
^entry (%_date : none, %_daysBack : i32, %_counter : i32):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :37)
cbde.store %_date, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :37)
%1 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :50)
cbde.store %_daysBack, %1 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :50)
%2 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :64)
cbde.store %_counter, %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :252 :64)
br ^0

^0: // BinaryBranchBlock
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :254 :16) // Not a variable of known type: GetWeekSumDataThread
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :254 :40) // null (NullLiteralExpression)
%5 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :254 :16) // comparison of unknown type: GetWeekSumDataThread != null
cond_br %5, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :254 :16)

^1: // SimpleBlock
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :256 :16) // Not a variable of known type: GetWeekSumDataThread
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :256 :16) // GetWeekSumDataThread.Abort() (InvocationExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :257 :39) // null (NullLiteralExpression)
br ^2

^2: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetWeekSumDataAsync
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :260 :43) // Not a variable of known type: date
%10 = cbde.load %1 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :260 :49)
%11 = cbde.load %2 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :260 :59)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :260 :23) // GetWeekSumDataAsync(date, daysBack, counter) (InvocationExpression)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :262 :19) // Not a variable of known type: task
%15 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :262 :19) // task.Result (SimpleMemberAccessExpression)
return %15 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :262 :12)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function GetSettings(), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.SetPlayNotificationSound$bool$(i1) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :285 :8) {
^entry (%_playNotificationSound : i1):
%0 = cbde.alloca i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :285 :47)
cbde.store %_playNotificationSound, %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :285 :47)
br ^0

^0: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :287 :12) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :287 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :287 :12) // Properties.Settings.Default.PlayNotificationSound (SimpleMemberAccessExpression)
%4 = cbde.load %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :287 :64)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :288 :12) // Properties.Settings (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :288 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :288 :12) // Properties.Settings.Default.Save() (InvocationExpression)
// Entity from another assembly: JsonConvert
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :290 :47) // Properties.Settings (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :290 :47) // Properties.Settings.Default (SimpleMemberAccessExpression)
%10 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :290 :47) // Properties.Settings.Default.PlayNotificationSound (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :290 :19) // JsonConvert.SerializeObject(Properties.Settings.Default.PlayNotificationSound) (InvocationExpression)
return %11 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :290 :12)

^1: // ExitBlock
cbde.unreachable

}
func @_TimeTracker.Helper.MyScriptingClass.SetHotkeyDisabled$bool$(i1) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :293 :8) {
^entry (%_hotkeyDisabled : i1):
%0 = cbde.alloca i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :293 :40)
cbde.store %_hotkeyDisabled, %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :293 :40)
br ^0

^0: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :295 :12) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :295 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :295 :12) // Properties.Settings.Default.HotkeyDisabled (SimpleMemberAccessExpression)
%4 = cbde.load %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :295 :57)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :296 :12) // Properties.Settings (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :296 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :296 :12) // Properties.Settings.Default.Save() (InvocationExpression)
// Entity from another assembly: JsonConvert
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :298 :47) // Properties.Settings (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :298 :47) // Properties.Settings.Default (SimpleMemberAccessExpression)
%10 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :298 :47) // Properties.Settings.Default.HotkeyDisabled (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :298 :19) // JsonConvert.SerializeObject(Properties.Settings.Default.HotkeyDisabled) (InvocationExpression)
return %11 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :298 :12)

^1: // ExitBlock
cbde.unreachable

}
func @_TimeTracker.Helper.MyScriptingClass.SetOfflineTracking$bool$(i1) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :301 :8) {
^entry (%_offlineTracking : i1):
%0 = cbde.alloca i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :301 :41)
cbde.store %_offlineTracking, %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :301 :41)
br ^0

^0: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :303 :12) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :303 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :303 :12) // Properties.Settings.Default.OfflineTracking (SimpleMemberAccessExpression)
%4 = cbde.load %0 : memref<i1> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :303 :58)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :304 :12) // Properties.Settings (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :304 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :304 :12) // Properties.Settings.Default.Save() (InvocationExpression)
// Entity from another assembly: JsonConvert
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :306 :47) // Properties.Settings (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :306 :47) // Properties.Settings.Default (SimpleMemberAccessExpression)
%10 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :306 :47) // Properties.Settings.Default.OfflineTracking (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :306 :19) // JsonConvert.SerializeObject(Properties.Settings.Default.OfflineTracking) (InvocationExpression)
return %11 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :306 :12)

^1: // ExitBlock
cbde.unreachable

}
func @_TimeTracker.Helper.MyScriptingClass.SetTimeNotificationVisible$string$(none) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :309 :8) {
^entry (%_timeNotificationVisible : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :309 :49)
cbde.store %_timeNotificationVisible, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :309 :49)
br ^0

^0: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :311 :12) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :311 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :311 :12) // Properties.Settings.Default.TimeNotificationVisible (SimpleMemberAccessExpression)
// Entity from another assembly: Int32
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :311 :78) // Not a variable of known type: timeNotificationVisible
%5 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :311 :66) // Int32.Parse(timeNotificationVisible) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :312 :12) // Properties.Settings (SimpleMemberAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :312 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :312 :12) // Properties.Settings.Default.Save() (InvocationExpression)
// Entity from another assembly: JsonConvert
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :314 :47) // Properties.Settings (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :314 :47) // Properties.Settings.Default (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :314 :47) // Properties.Settings.Default.TimeNotificationVisible (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :314 :19) // JsonConvert.SerializeObject(Properties.Settings.Default.TimeNotificationVisible) (InvocationExpression)
return %12 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :314 :12)

^1: // ExitBlock
cbde.unreachable

}
func @_TimeTracker.Helper.MyScriptingClass.SetTimeBeforeAskingAgain$string$(none) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :317 :8) {
^entry (%_timeBeforeAskingAgain : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :317 :47)
cbde.store %_timeBeforeAskingAgain, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :317 :47)
br ^0

^0: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :319 :12) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :319 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :319 :12) // Properties.Settings.Default.TimeBeforeAskingAgain (SimpleMemberAccessExpression)
// Entity from another assembly: Int32
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :319 :76) // Not a variable of known type: timeBeforeAskingAgain
%5 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :319 :64) // Int32.Parse(timeBeforeAskingAgain) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :320 :12) // Properties.Settings (SimpleMemberAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :320 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :320 :12) // Properties.Settings.Default.Save() (InvocationExpression)
// Entity from another assembly: JsonConvert
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :322 :47) // Properties.Settings (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :322 :47) // Properties.Settings.Default (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :322 :47) // Properties.Settings.Default.TimeBeforeAskingAgain (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :322 :19) // JsonConvert.SerializeObject(Properties.Settings.Default.TimeBeforeAskingAgain) (InvocationExpression)
return %12 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :322 :12)

^1: // ExitBlock
cbde.unreachable

}
func @_TimeTracker.Helper.MyScriptingClass.SetTimeSinceAppLastUsed$string$(none) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :325 :8) {
^entry (%_timeSinceAppLastUsed : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :325 :46)
cbde.store %_timeSinceAppLastUsed, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :325 :46)
br ^0

^0: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :327 :12) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :327 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :327 :12) // Properties.Settings.Default.TimeSinceAppLastUsed (SimpleMemberAccessExpression)
// Entity from another assembly: Int32
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :327 :75) // Not a variable of known type: timeSinceAppLastUsed
%5 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :327 :63) // Int32.Parse(timeSinceAppLastUsed) (InvocationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :328 :12) // Properties.Settings (SimpleMemberAccessExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :328 :12) // Properties.Settings.Default (SimpleMemberAccessExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :328 :12) // Properties.Settings.Default.Save() (InvocationExpression)
// Entity from another assembly: JsonConvert
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :330 :47) // Properties.Settings (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :330 :47) // Properties.Settings.Default (SimpleMemberAccessExpression)
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :330 :47) // Properties.Settings.Default.TimeSinceAppLastUsed (SimpleMemberAccessExpression)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :330 :19) // JsonConvert.SerializeObject(Properties.Settings.Default.TimeSinceAppLastUsed) (InvocationExpression)
return %12 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :330 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function SetHotkeys(none), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetTrackingSettings$$() -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :341 :8) {
^entry :
br ^0

^0: // JumpBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :343 :47) // new TrackingSettings              {                  Blacklist = Properties.Settings.Default.Blacklist.Cast<string>().ToList()              } (ObjectCreationExpression)
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: Properties
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :345 :28) // Properties.Settings (SimpleMemberAccessExpression)
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :345 :28) // Properties.Settings.Default (SimpleMemberAccessExpression)
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :345 :28) // Properties.Settings.Default.Blacklist (SimpleMemberAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :345 :28) // Properties.Settings.Default.Blacklist.Cast<string>() (InvocationExpression)
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :345 :28) // Properties.Settings.Default.Blacklist.Cast<string>().ToList() (InvocationExpression)
// Entity from another assembly: JsonConvert
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :348 :54) // Not a variable of known type: Settings
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :348 :26) // JsonConvert.SerializeObject(Settings) (InvocationExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :351 :19) // Not a variable of known type: Json
return %10 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :351 :12)

^1: // ExitBlock
cbde.unreachable

}
// Skipping function SetBlacklist(none), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.OpenUrl$string$(none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :372 :8) {
^entry (%_url : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :372 :28)
cbde.store %_url, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :372 :28)
br ^0

^0: // SimpleBlock
// Entity from another assembly: Process
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :374 :26) // Not a variable of known type: url
%2 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :374 :12) // Process.Start(url) (InvocationExpression)
br ^1

^1: // ExitBlock
return

}
// Skipping function GetActivities(), it contains poisonous unsupported syntaxes

// Skipping function GetReportData1Async(none, none, none, i32, i32), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetReportData1$System.Collections.Generic.List$object$.string.string.int.int$(none, none, none, i32, i32) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :8) {
^entry (%_activities : none, %_start : none, %_end : none, %_zoom : i32, %_counter : i32):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :37)
cbde.store %_activities, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :37)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :62)
cbde.store %_start, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :62)
%2 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :76)
cbde.store %_end, %2 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :76)
%3 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :88)
cbde.store %_zoom, %3 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :88)
%4 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :98)
cbde.store %_counter, %4 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :500 :98)
br ^0

^0: // BinaryBranchBlock
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :502 :16) // Not a variable of known type: GetReportData1Thread
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :502 :40) // null (NullLiteralExpression)
%7 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :502 :16) // comparison of unknown type: GetReportData1Thread != null
cond_br %7, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :502 :16)

^1: // SimpleBlock
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :504 :16) // Not a variable of known type: GetReportData1Thread
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :504 :16) // GetReportData1Thread.Abort() (InvocationExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :505 :39) // null (NullLiteralExpression)
br ^2

^2: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetReportData1Async
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :508 :43) // Not a variable of known type: activities
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :508 :55) // Not a variable of known type: start
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :508 :62) // Not a variable of known type: end
%14 = cbde.load %3 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :508 :67)
%15 = cbde.load %4 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :508 :73)
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :508 :23) // GetReportData1Async(activities, start, end, zoom, counter) (InvocationExpression)
%18 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :510 :19) // Not a variable of known type: task
%19 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :510 :19) // task.Result (SimpleMemberAccessExpression)
return %19 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :510 :12)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function GetReportData2Async(none, none, none, i32), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetReportData2$System.Collections.Generic.List$object$.string.string.int$(none, none, none, i32) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :8) {
^entry (%_activities : none, %_start : none, %_end : none, %_counter : i32):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :37)
cbde.store %_activities, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :37)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :62)
cbde.store %_start, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :62)
%2 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :76)
cbde.store %_end, %2 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :76)
%3 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :88)
cbde.store %_counter, %3 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :579 :88)
br ^0

^0: // BinaryBranchBlock
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :581 :16) // Not a variable of known type: GetReportData2Thread
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :581 :40) // null (NullLiteralExpression)
%6 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :581 :16) // comparison of unknown type: GetReportData2Thread != null
cond_br %6, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :581 :16)

^1: // SimpleBlock
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :583 :16) // Not a variable of known type: GetReportData2Thread
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :583 :16) // GetReportData2Thread.Abort() (InvocationExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :584 :39) // null (NullLiteralExpression)
br ^2

^2: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetReportData2Async
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :587 :43) // Not a variable of known type: activities
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :587 :55) // Not a variable of known type: start
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :587 :62) // Not a variable of known type: end
%13 = cbde.load %3 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :587 :67)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :587 :23) // GetReportData2Async(activities, start, end, counter) (InvocationExpression)
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :589 :19) // Not a variable of known type: task
%17 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :589 :19) // task.Result (SimpleMemberAccessExpression)
return %17 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :589 :12)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function GetReportData3Async(none, none, none, i32), it contains poisonous unsupported syntaxes

func @_TimeTracker.Helper.MyScriptingClass.GetReportData3$System.Collections.Generic.List$object$.string.string.int$(none, none, none, i32) -> none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :8) {
^entry (%_activities : none, %_start : none, %_end : none, %_counter : i32):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :37)
cbde.store %_activities, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :37)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :62)
cbde.store %_start, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :62)
%2 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :76)
cbde.store %_end, %2 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :76)
%3 = cbde.alloca i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :88)
cbde.store %_counter, %3 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :693 :88)
br ^0

^0: // BinaryBranchBlock
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :695 :16) // Not a variable of known type: GetReportData3Thread
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :695 :40) // null (NullLiteralExpression)
%6 = cbde.unknown : i1  loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :695 :16) // comparison of unknown type: GetReportData3Thread != null
cond_br %6, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :695 :16)

^1: // SimpleBlock
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :697 :16) // Not a variable of known type: GetReportData3Thread
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :697 :16) // GetReportData3Thread.Abort() (InvocationExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :698 :39) // null (NullLiteralExpression)
br ^2

^2: // JumpBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: GetReportData3Async
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :701 :43) // Not a variable of known type: activities
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :701 :55) // Not a variable of known type: start
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :701 :62) // Not a variable of known type: end
%13 = cbde.load %3 : memref<i32> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :701 :67)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :701 :23) // GetReportData3Async(activities, start, end, counter) (InvocationExpression)
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :703 :19) // Not a variable of known type: task
%17 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :703 :19) // task.Result (SimpleMemberAccessExpression)
return %17 : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Helper\\GUI\\MyScriptingClass.cs" :703 :12)

^3: // ExitBlock
cbde.unreachable

}
// Skipping function EachDay(none, none), it contains poisonous unsupported syntaxes

