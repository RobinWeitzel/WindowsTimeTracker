using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper
{
    class Window
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public void save()
        {
            using (TextWriter tw = new StreamWriter(Variables.windowPath, true))
            {
                var csv = new CsvWriter(tw);

                To = DateTime.Now;
                Variables.windowsLastSeen[Name] = (DateTime) To;

                if (From > To)
                    return;

                csv.WriteRecord(this);
                csv.NextRecord();
            }
        }
    }
}
