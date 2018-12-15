candle TimeTrackerSetup.wxs -ext WixUtilExtension
light TimeTrackerSetup.wixobj -ext WixUIExtension -ext WixUtilExtension -dWixUILicenseRtf=license.rtf -out TimeTrackerSetup.msi
@pause