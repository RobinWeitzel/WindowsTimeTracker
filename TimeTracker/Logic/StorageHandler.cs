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
            string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TimeTracker";
            WindowPath = Path + "\\Windows.csv";
            ActivityPath = Path + "\\Activities.csv";

            CreateFilesIfNoneExist(Path);
        }

        /// <summary>
        /// Repairs the activities csv file if it contains malformed entries
        /// </summary>
        private void RestoreActivitiesCsv()
        {
            List<Activity> Good;

            // Read in CSV with activities
            using (StreamReader Reader = new StreamReader(ActivityPath))
            using (CsvReader Csv = new CsvReader(Reader))
            {
                Good = new List<Activity>();

                while (Csv.Read())
                {
                    try
                    {
                        Activity Record = Csv.GetRecord<Activity>();
                        Good.Add(Record);
                    }
                    catch (Exception ignore)
                    {
                    }
                }
            }

            WriteActivities(Good);
        }

        /// <summary>
        /// Repairs the windows csv file if it contains malformed entries
        /// </summary>
        private void RestoreWindowsCsv()
        {
            List<Window> Good;

            // Read in CSV with activities
            using (StreamReader Reader = new StreamReader(WindowPath))
            using (CsvReader Csv = new CsvReader(Reader))
            {
                Good = new List<Window>();

                while (Csv.Read())
                {
                    try
                    {
                        Window Record = Csv.GetRecord<Window>();
                        Good.Add(Record);
                    }
                    catch (Exception ignore)
                    {
                    }
                }
            }

            WriteWindows(Good);
        }

        /// <summary>
        /// Gets the records from the activites csv file
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Activity> GetActivityRecords()
        {
            try
            {
                using (TextReader tr = new StreamReader(ActivityPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    return Csv.GetRecords<Activity>();
                }
            }
            catch (CsvHelper.MissingFieldException ex)
            {
                RestoreActivitiesCsv();
                return GetActivityRecords();
            } 
        }

        /// <summary>
        /// Gets the records from the windows csv file
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Window> GetWindowRecords()
        {
            try { 
                using (TextReader tr = new StreamReader(WindowPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    return Csv.GetRecords<Window>();
                }
            } catch (CsvHelper.MissingFieldException ex)
            {
                RestoreWindowsCsv();
                return GetWindowRecords();
            }
        }

        /// <summary>
        /// Gets the most recent activity from the csv-file or null, if none exists.
        /// </summary>
        /// <returns>Null or the most recent activity</returns>
        public Activity GetLastActivity()
        {
            return GetActivityRecords().OrderBy(r => r.To).LastOrDefault();
        }

        /// <summary>
        /// Get all activites ordered by their To-date
        /// </summary>
        /// <returns>List of ordered activities</returns>
        public List<Activity> GetLastActivities()
        {
            return GetActivityRecords().OrderByDescending(aa => aa.To).ToList();
        }

        /// <summary>
        /// Get all activities grouped by their name and ordered by their latest To-date
        /// </summary>
        /// <returns>List of ordered groups of activities</returns>
        public List<IGrouping<string, Activity>> GetLastActivitiesGrouped()
        {
            return GetActivityRecords().GroupBy(r => r.Name).OrderByDescending(rg => rg.Max(r => r.From)).ToList();
        }

        /// <summary>
        /// Returns a list of activies fitting the provided filter expression.
        /// </summary>
        /// <param name="filter">The filter for the activities</param>
        /// <returns></returns>
        public List<Activity> GetActivitiesByLambda(Func<Activity, bool> filter)
        {
            return GetActivityRecords().Where(filter).ToList();
        }

        /// <summary>
        /// Returns a list of windows fitting the provided filter expression.
        /// </summary>
        /// <param name="filter">The filter for the windows</param>
        /// <returns></returns>
        public List<Window> GetWindowsByLambda(Func<Window, bool> filter)
        {
            return GetWindowRecords().Where(filter).ToList();
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
        /// Writes an activity to the csv files.
        /// </summary>
        /// <param name="activity">The activity that should be written to the csv file.</param>
        public void WriteActivity(Activity activity)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(ActivityPath, true))
                {
                    CsvWriter Csv = new CsvWriter(tw);

                    Csv.WriteRecord(activity);
                    Csv.NextRecord();
                }
            } catch (CsvHelper.MissingFieldException ex)
            {
                RestoreActivitiesCsv();
                WriteActivity(activity);
            }
        }

        /// <summary>
        /// Writes multiple acitivites to the csv file overwriting the old data.
        /// </summary>
        /// <param name="activities">List of activies that should be written to the csv file.</param>
        public void WriteActivities(List<Activity> activities)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(ActivityPath))
                {
                    CsvWriter Csv = new CsvWriter(tw);
                    Csv.WriteRecords(activities);
                    Csv.NextRecord();
                }
            } catch (CsvHelper.MissingFieldException ex)
            {
                RestoreActivitiesCsv();
                WriteActivities(activities);
            }
        }

        /// <summary>
        /// Writes a window to the csv file.
        /// </summary>
        /// <param name="window">The window that should be written to the csv file.</param>
        public void WriteWindow(Window window)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(WindowPath, true))
                {
                    CsvWriter Csv = new CsvWriter(tw);

                    Csv.WriteRecord(window);
                    Csv.NextRecord();
                }
            } catch (CsvHelper.MissingFieldException ex)
            {
                RestoreWindowsCsv();
                WriteWindow(window);
            }
        }


        /// <summary>
        /// Writes multiple windows to the csv file overwriting the old data.
        /// </summary>
        /// <param name="activities">List of windows that should be written to the csv file.</param>
        public void WriteWindows(List<Window> windows)
        {
            try
            {
                using (TextWriter tw = new StreamWriter(WindowPath))
                {
                    CsvWriter Csv = new CsvWriter(tw);
                    Csv.WriteRecords(windows);
                    Csv.NextRecord();
                }
            } catch (CsvHelper.MissingFieldException ex)
            {
                RestoreWindowsCsv();
                WriteWindows(windows);
            }
        }
    }
}
