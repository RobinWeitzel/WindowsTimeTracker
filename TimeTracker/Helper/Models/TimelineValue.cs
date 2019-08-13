using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper.Models
{
    class TimelineValue
    {
        [JsonProperty(PropertyName = "start")]
        public double Start { get; set; }
        [JsonProperty(PropertyName = "length")]
        public double Length { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
