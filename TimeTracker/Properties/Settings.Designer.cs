﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeTracker.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TutorialViewed {
            get {
                return ((bool)(this["TutorialViewed"]));
            }
            set {
                this["TutorialViewed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public long TimeSinceAppLastUsed {
            get {
                return ((long)(this["TimeSinceAppLastUsed"]));
            }
            set {
                this["TimeSinceAppLastUsed"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public long TimeNotificationVisible {
            get {
                return ((long)(this["TimeNotificationVisible"]));
            }
            set {
                this["TimeNotificationVisible"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public long TimeBeforeAskingAgain {
            get {
                return ((long)(this["TimeBeforeAskingAgain"]));
            }
            set {
                this["TimeBeforeAskingAgain"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool PlayNotificationSound {
            get {
                return ((bool)(this["PlayNotificationSound"]));
            }
            set {
                this["PlayNotificationSound"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>TimeTracker</string>
  <string>Neue Benachrichtigung</string>
  <string>Explorer</string>
  <string>Cortana</string>
  <string>Akkuinformationen</string>
  <string>Start</string>
  <string>UnlockingWindow</string>
  <string>Cortana</string>
  <string>Status</string>
  <string>Aktive Anwendungen</string>
  <string>Window Dialog</string>
  <string>Info-Center</string>
  <string>Windows-Standardsperrbildschirm</string>
  <string>Host für die Windows Shell-Oberfläche</string>
  <string>F12PopupWindow</string>
  <string>LockingWindow</string>
  <string>CTX_RX_SYSTRAY</string>
  <string>[]</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Blacklist {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Blacklist"]));
            }
            set {
                this["Blacklist"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool OfflineTracking {
            get {
                return ((bool)(this["OfflineTracking"]));
            }
            set {
                this["OfflineTracking"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Generic.List<System.Windows.Input.Key> Hotkeys {
            get {
                return ((global::System.Collections.Generic.List<System.Windows.Input.Key>)(this["Hotkeys"]));
            }
            set {
                this["Hotkeys"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool HotkeyDisabled {
            get {
                return ((bool)(this["HotkeyDisabled"]));
            }
            set {
                this["HotkeyDisabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DarkMode {
            get {
                return ((bool)(this["DarkMode"]));
            }
            set {
                this["DarkMode"] = value;
            }
        }
    }
}
