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
                Csv.Configuration.Delimiter = ",";
                Csv.Configuration.BadDataFound = null;

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
                Csv.Configuration.Delimiter = ",";
                Csv.Configuration.BadDataFound = null;
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
        /// Gets the most recent activity from the csv-file or null, if none exists.
        /// </summary>
        /// <returns>Null or the most recent activity</returns>
        public Activity GetLastActivity()
        {
            try
            {
                using (TextReader tr = new StreamReader(ActivityPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Activity> Records = Csv.GetRecords<Activity>();
                    return Records.OrderBy(r => r.To).LastOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    return GetLastActivity();
                }
                return null;
            }
        }

        /// <summary>
        /// Get all activites ordered by their To-date
        /// </summary>
        /// <returns>List of ordered activities</returns>
        public List<Activity> GetLastActivities()
        {
            try
            {
                using (TextReader tr = new StreamReader(ActivityPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Activity> Records = Csv.GetRecords<Activity>();
                    return Records.OrderByDescending(aa => aa.To).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    return GetLastActivities();
                }
                return new List<Activity>();
            }
        }

        /// <summary>
        /// Get all activities grouped by their name and ordered by their latest To-date
        /// </summary>
        /// <returns>List of ordered groups of activities</returns>
        public List<IGrouping<string, Activity>> GetLastActivitiesGrouped()
        {
            try
            {
                using (TextReader tr = new StreamReader(ActivityPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Activity> Records = Csv.GetRecords<Activity>();
                    return Records.GroupBy(r => r.Name).OrderByDescending(rg => rg.Max(r => r.From)).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    return GetLastActivitiesGrouped();
                }
                return new List<IGrouping<string, Activity>>();
            }
        }

        /// <summary>
        /// Get all activities grouped by their name and ordered by their earliest To-date
        /// </summary>
        /// <returns>List of ordered groups of activities</returns>
        public List<IGrouping<string, Activity>> GetEarliestActivitiesGrouped()
        {
            try
            {
                using (TextReader tr = new StreamReader(ActivityPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Activity> Records = Csv.GetRecords<Activity>();
                    return Records.GroupBy(r => r.Name).OrderBy(rg => rg.Min(r => r.From)).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    return GetEarliestActivitiesGrouped();
                }
                return new List<IGrouping<string, Activity>>();
            }
        }

        /// <summary>
        /// Returns a list of activies fitting the provided filter expression.
        /// </summary>
        /// <param name="filter">The filter for the activities</param>
        /// <returns></returns>
        public List<Activity> GetActivitiesByLambda(Func<Activity, bool> filter)
        {
            try
            {
                using (TextReader tr = new StreamReader(ActivityPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Activity> Records = Csv.GetRecords<Activity>();
                    return Records.Where(filter).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    return GetActivitiesByLambda(filter);
                }
                return new List<Activity>();
            }
        }

        /// <summary>
        /// Get all windows grouped by their name and ordered by their earliest To-date
        /// </summary>
        /// <returns>List of ordered groups of activities</returns>
        public List<IGrouping<string, Window>> GetLastestWindowsGrouped()
        {
            try
            {
                using (TextReader tr = new StreamReader(WindowPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Window> Records = Csv.GetRecords<Window>();
                    return Records.GroupBy(r => r.Name).OrderByDescending(rg => rg.Max(r => r.From)).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    return GetLastestWindowsGrouped();
                }
                return new List<IGrouping<string, Window>>();
            }
        }

        /// <summary>
        /// Returns a list of windows fitting the provided filter expression.
        /// </summary>
        /// <param name="filter">The filter for the windows</param>
        /// <returns></returns>
        public List<Window> GetWindowsByLambda(Func<Window, bool> filter)
        {
            try
            {
                using (TextReader tr = new StreamReader(WindowPath))
                {
                    CsvReader Csv = new CsvReader(tr);
                    Csv.Configuration.Delimiter = ",";
                    IEnumerable<Window> Records = Csv.GetRecords<Window>();
                    return Records.Where(filter).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreWindowsCsv();
                    return GetWindowsByLambda(filter);
                }
                return new List<Window>();
            }
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
                    Csv.Configuration.Delimiter = ",";
                    Csv.WriteHeader<Window>();
                    Csv.NextRecord();
                }
            }

            if (!File.Exists(ActivityPath))
            {
                using (TextWriter tw = new StreamWriter(ActivityPath))
                {
                    CsvWriter Csv = new CsvWriter(tw);
                    Csv.Configuration.Delimiter = ",";
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
                    Csv.Configuration.Delimiter = ",";
                    Csv.WriteRecord(activity);
                    Csv.NextRecord();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    WriteActivity(activity);
                }
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
                    Csv.Configuration.Delimiter = ",";
                    Csv.WriteRecords(activities);
                    Csv.NextRecord();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreActivitiesCsv();
                    WriteActivities(activities);
                }
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
                    Csv.Configuration.Delimiter = ",";
                    Csv.WriteRecord(window);
                    Csv.NextRecord();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreWindowsCsv();
                    WriteWindow(window);
                }
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
                    Csv.Configuration.Delimiter = ",";
                    Csv.WriteRecords(windows);
                    Csv.NextRecord();
                }
            }
            catch (Exception ex)
            {
                if (ex is CsvHelper.MissingFieldException || ex is CsvHelperException)
                {
                    RestoreWindowsCsv();
                    WriteWindows(windows);
                }
            }
        }
    }
}
