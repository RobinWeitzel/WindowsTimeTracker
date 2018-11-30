using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Helper
{
    class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public void save(DateTime? to = null)
        {
            using (TextWriter tw = new StreamWriter(Variables.activityPath, true))
            {
                var csv = new CsvWriter(tw);
                Id = Guid.NewGuid();
                To = to ?? DateTime.Now;

                csv.WriteRecord(this);
                csv.NextRecord();
            }
        }
    }
}
