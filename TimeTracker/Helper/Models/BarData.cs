using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper.Models
{
    class Bardata
    {
        [JsonProperty(PropertyName = "labels")]
        public List<string> Labels { get; set; }

        [JsonProperty(PropertyName = "datasets")]
        public List<Dataset> Datasets { get; set; }
    }
}
