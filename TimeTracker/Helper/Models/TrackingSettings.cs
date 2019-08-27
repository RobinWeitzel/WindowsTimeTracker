using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper.Models
{
    class TrackingSettings
    {
        [JsonProperty(PropertyName = "offlineTracking")]
        public bool OfflineTracking { get; set; } // Upon returning to the computer, ask the user what he was doing

        [JsonProperty(PropertyName = "blacklist")]
        public List<string> Blacklist { get; set; } // Apps that will be ignored by TimeTracker
    }
}
