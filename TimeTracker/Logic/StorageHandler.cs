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
        }

        /// <summary>
        /// Create the TimeTracke directory in which the csv-files are kept if it does not yet exist.
        /// Also creates the csv-files with headers if they do not yet exist.
        /// </summary>
        /// <param name="path"></param>
        private void CreateFilesIfNoneExist(string path)
        {
            if (!File.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(WindowPath))
            {
                using (TextWriter tw = new StreamWriter(WindowPath))
                {
                    CsvWriter Csv = new CsvWriter(tw);
                    Csv.WriteHeader<Window>();
                    Csv.NextRecord();
                }
            }

            if (!File.Exists(ActivityPath))
            {
                using (TextWriter tw = new StreamWriter(ActivityPath))
                {
                    CsvWriter Csv = new CsvWriter(tw);
                    Csv.WriteHeader<Activity>();
                    Csv.NextRecord();
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

        /// <summary>
        /// Get all activites ordered by their To-date
        /// </summary>
        /// <returns>List of ordered activities</returns>
        public List<Activity> GetLastActivities()
        {
            using (TextReader tr = new StreamReader(ActivityPath))
            {
                CsvReader Csv = new CsvReader(tr);
                IEnumerable<Activity> Records = Csv.GetRecords<Activity>();

                return Records.OrderByDescending(aa => aa.To).ToList();
            }
        }

        /// <summary>
        /// Get all activities grouped by their name and ordered by their latest To-date
        /// </summary>
        /// <returns>List of ordered groups of activities</returns>
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
        /// Returns a list of activies fitting the provided filter expression.
        /// </summary>
        /// <param name="filter">The filter for the activities</param>
        /// <returns></returns>
        public List<Activity> GetActivitiesByLambda(Func<Activity, bool> filter)
        {
            using (TextReader tr = new StreamReader(ActivityPath))
            {
                CsvReader Csv = new CsvReader(tr);
                IEnumerable<Activity> Records = Csv.GetRecords<Activity>();

                return Records.Where(filter).ToList();
            }
        }

        /// <summary>
        /// Returns a list of windows fitting the provided filter expression.
        /// </summary>
        /// <param name="filter">The filter for the windows</param>
        /// <returns></returns>
        public List<Window> GetWindowsByLambda(Func<Window, bool> filter)
        {
            using (TextReader tr = new StreamReader(WindowPath))
            {
                CsvReader Csv = new CsvReader(tr);
                IEnumerable<Window> Records = Csv.GetRecords<Window>();

                return Records.Where(filter).ToList();
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

        /// <summary>
        /// Writes multiple acitivites to the csv file overwriting the old data.
        /// </summary>
        /// <param name="activities">List of activies that should be written to the csv file.</param>
        public void WriteActivities(List<Activity> activities)
        {
            using (TextWriter tw = new StreamWriter(ActivityPath))
            {
                CsvWriter Csv = new CsvWriter(tw);
                Csv.WriteRecords(activities);
                Csv.NextRecord();
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
