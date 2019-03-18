using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper
{
    public class Window
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
