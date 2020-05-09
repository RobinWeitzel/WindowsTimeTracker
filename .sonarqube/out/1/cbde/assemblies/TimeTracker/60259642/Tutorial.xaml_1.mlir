func @_TimeTracker.Tutorial.DrawImage$$() -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :30 :8) {
^entry :
br ^0

^0: // SimpleBlock
%0 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :36) // "Resources/" (StringLiteralExpression)
%1 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :51) // Not a variable of known type: Images
%2 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :58) // Not a variable of known type: Position
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :51) // Images[Position] (ElementAccessExpression)
%4 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :36) // Binary expression on unsupported types "Resources/" + Images[Position]
// Entity from another assembly: UriKind
%5 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :69) // UriKind.Relative (SimpleMemberAccessExpression)
%6 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :32 :28) // new Uri("Resources/" + Images[Position], UriKind.Relative) (ObjectCreationExpression)
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :34 :12) // Not a variable of known type: Image
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :34 :12) // Image.Source (SimpleMemberAccessExpression)
%10 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :34 :43) // Not a variable of known type: uriSource
%11 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :34 :27) // new BitmapImage(uriSource) (ObjectCreationExpression)
%12 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :35 :12) // Not a variable of known type: TextBlock
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :35 :12) // TextBlock.Text (SimpleMemberAccessExpression)
%14 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :35 :29) // Not a variable of known type: Text
%15 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :35 :34) // Not a variable of known type: Position
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :35 :29) // Text[Position] (ElementAccessExpression)
br ^1

^1: // ExitBlock
return

}
func @_TimeTracker.Tutorial.Button_Click$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :38 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :38 :34)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :38 :34)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :38 :49)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :38 :49)
br ^0

^0: // BinaryBranchBlock
// Entity from another assembly: Math
%2 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :40 :32) // Not a variable of known type: Position
%3 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :40 :43)
%4 = subi %2, %3 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :40 :32)
%5 = constant 0 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :40 :46)
%6 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :40 :23) // Math.Max(Position - 1, 0) (InvocationExpression)
%7 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :42 :12) // Not a variable of known type: Next
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :42 :12) // Next.Content (SimpleMemberAccessExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :42 :27) // "Next" (StringLiteralExpression)
%10 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :43 :16) // Not a variable of known type: Position
%11 = constant 0 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :43 :28)
%12 = cmpi "eq", %10, %11 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :43 :16)
cond_br %12, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :43 :16)

^1: // SimpleBlock
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :44 :16) // Not a variable of known type: Previous
%14 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :44 :16) // Previous.IsEnabled (SimpleMemberAccessExpression)
%15 = constant 0 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :44 :37) // false
br ^2

^2: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: DrawImage
%16 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :46 :12) // DrawImage() (InvocationExpression)
br ^3

^3: // ExitBlock
return

}
func @_TimeTracker.Tutorial.Button_Click_1$object.System.Windows.RoutedEventArgs$(none, none) -> () loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :49 :8) {
^entry (%_sender : none, %_e : none):
%0 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :49 :36)
cbde.store %_sender, %0 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :49 :36)
%1 = cbde.alloca none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :49 :51)
cbde.store %_e, %1 : memref<none> loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :49 :51)
br ^0

^0: // BinaryBranchBlock
%2 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :16) // Not a variable of known type: Position
%3 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :28) // Not a variable of known type: Images
%4 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :28) // Images.Length (SimpleMemberAccessExpression)
%5 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :44)
%6 = subi %4, %5 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :28)
%7 = cmpi "eq", %2, %6 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :16)
cond_br %7, ^1, ^2 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :51 :16)

^1: // JumpBlock
%8 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :53 :16) // this (ThisExpression)
%9 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :53 :16) // this.Close() (InvocationExpression)
return loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :54 :16)

^2: // BinaryBranchBlock
// Entity from another assembly: Math
%10 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :32) // Not a variable of known type: Position
%11 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :43)
%12 = addi %10, %11 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :32)
%13 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :46) // Not a variable of known type: Images
%14 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :46) // Images.Length (SimpleMemberAccessExpression)
%15 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :62)
%16 = subi %14, %15 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :46)
%17 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :57 :23) // Math.Min(Position + 1, Images.Length - 1) (InvocationExpression)
%18 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :59 :12) // Not a variable of known type: Previous
%19 = cbde.unknown : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :59 :12) // Previous.IsEnabled (SimpleMemberAccessExpression)
%20 = constant 1 : i1 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :59 :33) // true
%21 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :16) // Not a variable of known type: Position
%22 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :28) // Not a variable of known type: Images
%23 = cbde.unknown : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :28) // Images.Length (SimpleMemberAccessExpression)
%24 = constant 1 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :44)
%25 = subi %23, %24 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :28)
%26 = cmpi "eq", %21, %25 : i32 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :16)
cond_br %26, ^3, ^4 loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :60 :16)

^3: // SimpleBlock
%27 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :61 :16) // Not a variable of known type: Next
%28 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :61 :16) // Next.Content (SimpleMemberAccessExpression)
%29 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :61 :31) // "Close" (StringLiteralExpression)
br ^4

^4: // SimpleBlock
// Skipped because MethodDeclarationSyntax or ClassDeclarationSyntax or NamespaceDeclarationSyntax: DrawImage
%30 = cbde.unknown : none loc("C:\\Users\\robin\\source\\repos\\WindowsTimeTracker\\TimeTracker\\Tutorial.xaml.cs" :63 :12) // DrawImage() (InvocationExpression)
br ^5

^5: // ExitBlock
return

}
