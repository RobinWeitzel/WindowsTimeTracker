using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper.Models
{
    class Bardata2
    {
        [JsonProperty(PropertyName = "label")]
        public double Label { get; set; }

        [JsonProperty(PropertyName = "datasets")]
        public List<Dataset> Datasets { get; set; }
    }
}
