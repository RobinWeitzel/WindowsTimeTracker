start /w /d "C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64"  signtool.exe sign /n "Robin Weitzel" /tr http://timestamp.digicert.com /td sha256 /fd sha256 /a "C:\Users\Robin\Source\Repos\WindowsTimeTracker\TimeTracker\bin\x64\Release\TimeTracker.exe"
candle TimeTrackerSetup.wxs -ext WixUtilExtension
light TimeTrackerSetup.wixobj -ext WixUIExtension -ext WixUtilExtension -dWixUILicenseRtf=license.rtf -out TimeTrackerSetup.msi
del TimeTrackerSetup.wixobj
del TimeTrackerSetup.wixpdb
start /w /d "C:\Program Files (x86)\Windows Kits\10\bin\10.0.17763.0\x64"  signtool.exe sign /n "Robin Weitzel" /tr http://timestamp.digicert.com /td sha256 /fd sha256 /a "C:\Users\Robin\Source\Repos\WindowsTimeTracker\TimeTracker\WiX Installer\TimeTrackerSetup.msi"
@pause
