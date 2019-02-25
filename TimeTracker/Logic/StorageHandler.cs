using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Helper;

namespace TimeTracker
{
    /// <summary>
    /// Responsible for reading from and writing to the csv files.
    /// Uses 2 csv files, one for activities and one for windows.
    /// </summary>
    public class StorageHandler
    {
        public string ActivityPath { get; set; }
        public string WindowPath { get; set; }

        /// <summary>
        /// Responsible for reading from and writing to the csv files.
        /// Uses 2 csv files, one for activities and one for windows.
        /// </summary>
        public StorageHandler()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TimeTracker";
            WindowPath = path + "\\Windows.csv";
            ActivityPath = path + "\\Activities.csv";

            CreateFilesIfNoneExist(path);

            // AppDomain.CurrentDomain.SetData("DataDirectory", path); ToDo: Unclear for what this is needed
        }

        /// <summary>
        /// Create the TimeTracke directory in which the csv-files are kept if it does not yet exist.
        /// Also create the csv-files with headers if they do not yet exist.
        /// </summary>
        /// <param name="path"></param>
        private void CreateFilesIfNoneExist(string path)
        {
            if (!File.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            if (!File.Exists(WindowPath))
            {
                using (TextWriter tw = new StreamWriter(WindowPath))
                {
                    var csv = new CsvWriter(tw);
                    csv.WriteHeader<Window>();
                    csv.NextRecord();
                }
            }

            if (!File.Exists(ActivityPath))
            {
                using (TextWriter tw = new StreamWriter(ActivityPath))
                {
                    var csv = new CsvWriter(tw);
                    csv.WriteHeader<Activity>();
                    csv.NextRecord();
                }
            }
        }

        /// <summary>
        /// Gets the most recent activity from the csv-file or null, if none exists.
        /// </summary>
        /// <returns>Null or the most recent activity</returns>
        public Activity GetLastActivity()
        {
            using (TextReader tr = new StreamReader(ActivityPath))
            {
                CsvReader Csv = new CsvReader(tr);
                IEnumerable<Activity> Records = Csv.GetRecords<Activity>();

                return Records.OrderBy(r => r.To).LastOrDefault();
            }
        }

        public List<Activity> GetLastActivities()
        {
            using (TextReader tr = new StreamReader(ActivityPath))
            {
                CsvReader Csv = new CsvReader(tr);
                IEnumerable<Activity> Records = Csv.GetRecords<Activity>();

                return Records.OrderByDescending(aa => aa.To).ToList();
            }
        }

        public List<IGrouping<string, Activity>> GetLastActivitiesGrouped()
        {
            using (TextReader tr = new StreamReader(ActivityPath))
            {
                CsvReader Csv = new CsvReader(tr);
                IEnumerable<Activity> Records = Csv.GetRecords<Activity>();

                return Records.GroupBy(r => r.Name).OrderByDescending(rg => rg.Max(r => r.From)).ToList();
            }
        }

        /// <summary>
        /// Writes an activity to the csv files.
        /// </summary>
        /// <param name="activity">The activity that should be written to the csv file.</param>
        public void WriteActivity(Activity activity)
        {
            using (TextWriter tw = new StreamWriter(ActivityPath, true))
            {
                CsvWriter Csv = new CsvWriter(tw);

                Csv.WriteRecord(activity);
                Csv.NextRecord();
            }
        }

        public void WriteActivities(List<Activity> activities)
        {
            using (TextWriter tw = new StreamWriter(ActivityPath))
            {
                CsvWriter Csv = new CsvWriter(tw);
                Csv.WriteRecords(activities);
            }
        }

        /// <summary>
        /// Writes a window to the csv file.
        /// </summary>
        /// <param name="window">The window that should be written to the csv file.</param>
        public void WriteWindow(Window window)
        {
            using (TextWriter tw = new StreamWriter(WindowPath, true))
            {
                CsvWriter Csv = new CsvWriter(tw);

                Csv.WriteRecord(window);
                Csv.NextRecord();
            }
        }
    }
}
