using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper.Models
{
    class Settings
    {
        [JsonProperty(PropertyName = "timeNotificationVisible")]
        public long TimeNotificationVisible { get; set; } // Amount of time a notification is visible on screen

        [JsonProperty(PropertyName = "playNotificationSound")]
        public bool PlayNotificationSound { get; set; } // Make a sound every time a notification is shown

        [JsonProperty(PropertyName = "timeSinceAppLastUsed")]
        public long TimeSinceAppLastUsed { get; set; } // Amount of time that must have passed before the Timetracker asks again to what activity a window belongs

        [JsonProperty(PropertyName = "timeBeforeAskingAgain")]
        public long TimeBeforeAskingAgain { get; set; } // Amount of time in which the Timetracker does not ask for an activity after selecting one

        [JsonProperty(PropertyName = "offlineTracking")]
        public bool OfflineTracking { get; set; } // Upon returning to the computer, ask the user what he was doing

        [JsonProperty(PropertyName = "blacklist")]
        public List<string> Blacklist { get; set; } // Apps that will be ignored by TimeTracker

        [JsonProperty(PropertyName = "hotkeys")]
        public List<int> Hotkeys { get; set; }

        [JsonProperty(PropertyName = "hotkeyDisabled")]
        public bool HotkeyDisabled { get; set; } // Enable global hotkey

        [JsonProperty(PropertyName = "darkMode")]
        public bool DarkMode { get; set; }
    }
}
