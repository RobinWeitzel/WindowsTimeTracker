using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper
{
    /// <summary>
    /// One activity.
    /// This dictates how values are stored in the Activities.csv
    /// </summary>
    public class Activity
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
