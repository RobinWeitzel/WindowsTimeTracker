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
        [JsonProperty(PropertyName = "blacklist")]
        public List<string> Blacklist { get; set; } // Apps that will be ignored by TimeTracker
    }
}
