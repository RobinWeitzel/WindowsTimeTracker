using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using TimeTracker.Helper.Models;
using TimeTracker.Properties;

namespace TimeTracker.Helper
{
    [ComVisible(true)]
    public class MyScriptingClass
    {
        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;

        public MyScriptingClass(StorageHandler storageHandler, AppStateTracker appStateTracker)
        {
            StorageHandler = storageHandler;
            AppStateTracker = appStateTracker;
        }

        public string GetDayData(string date, int counter)
        {
            DateTime DayInQuestion = DateTime.Parse(date).Date;
            DateTime DayInQuestionAfter = DayInQuestion.AddDays(1);

            /////// Load the data for the timeline //////
            List<Activity> TodayActivities = StorageHandler.GetActivitiesByLambda(r => r.To >= DayInQuestion && r.From < DayInQuestionAfter);

            // Check if the current activity should also be shown in the graph.
            if (AppStateTracker.CurrentActivity != null && (DayInQuestionAfter > DateTime.Now && DayInQuestion < DateTime.Now))
                TodayActivities.Add(AppStateTracker.CurrentActivity);

            List<Timeline> Timelines = TodayActivities
                .GroupBy(a => a.Name.Split(new string[] { " - " }, StringSplitOptions.None).First())
                .Select(g => new Timeline
                {
                    Label = g.Key,
                    Values = g.Select(a => new TimelineValue
                    {
                        Start = Math.Round(a.From >= DayInQuestion ? a.From.Subtract(DayInQuestion).TotalMinutes : 0),
                        Length = Math.Round((a.To == null || a.To >= DayInQuestionAfter ? (DayInQuestion == DateTime.Today ? DateTime.Now : DayInQuestionAfter) : (DateTime)a.To).Subtract(a.From >= DayInQuestion ? a.From : DayInQuestion).TotalMinutes),
                        Title = a.Name.Split(new string[] { " - " }, StringSplitOptions.None).Last()
                    }).OrderBy(tv => tv.Start).ToList(),
                    Colors = g.Select(a => new TimelineValue
                    {
                        Start = Math.Round(a.From >= DayInQuestion ? a.From.Subtract(DayInQuestion).TotalMinutes : 0), // Need to calculate start again to keep the order intact
                        Title = a.Name
                    }).OrderBy(tv => tv.Start)
                    .Select(t => AppStateTracker.ColorAssingments[t.Title])
                    .ToList()
                })
                .OrderBy(t => t.Label)
                .ToList();

            string Json = JsonConvert.SerializeObject(new { value = Timelines, counter });

            // Add the data as a JSON string to the result
            return Json;
        }

        public string GetWeekBreakdownData(string date, int daysBack, int counter)
        {
            DateTime EndPeriod = DateTime.Parse(date).Date;
            DateTime StartPeriod = EndPeriod.AddDays(-daysBack);

            List<(string, Dictionary<string, double>)> Days = new List<(string, Dictionary<string, double>)>();

            foreach (DateTime Day in EachDay(StartPeriod, EndPeriod))
            {
                DateTime Start = Day;
                DateTime End = Day.AddDays(1);

                List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= Start && r.From < End).Select(aa => new Helper
                {
                    Name = aa.Name.Split(new string[] { " - " }, StringSplitOptions.None).First(),
                    From = aa.From > Start ? aa.From : Start,
                    To = (DateTime)aa.To > End ? End : (DateTime)aa.To
                }).ToList();

                // Check if the current activity should also be shown in the graph.
                if (AppStateTracker.CurrentActivity != null && End >= DateTime.Today)
                    ActivityHelper.Add(new Helper
                    {
                        Name = AppStateTracker.CurrentActivity.Name.Split(new string[] { " - " }, StringSplitOptions.None).First(),
                        From = AppStateTracker.CurrentActivity.From > Start ? AppStateTracker.CurrentActivity.From : Start,
                        To = DateTime.Now
                    });

                // Compute time between Start and finish of the activity rounding to hours
                foreach (Helper h in ActivityHelper)
                {
                    h.Time = Math.Max(Math.Round((h.To - h.From).TotalHours, 2), 0);
                }

                Dictionary<string, double> ActivityDict = new Dictionary<string, double>();

                ActivityHelper
                    .GroupBy(ah => ah.Name)
                    .ToList()
                    .ForEach(g =>
                    {
                        ActivityDict.Add(g.Key, g.Sum(h => h.Time));
                    });

                Days.Add((Day.DayOfWeek.ToString().Substring(0, 2), ActivityDict));
            }

            // construct data
            List<Bardata> Bardata = Days.Select(d => new Bardata
            {
                Label = d.Item1,
                Datasets = d.Item2.Select(h => new Dataset
                {
                    Title = h.Key,
                    Value = h.Value,
                    Color = AppStateTracker.ColorAssingments[h.Key]
                }).OrderBy(dd => dd.Title).ToList()
            }).OrderBy(d => d.Label).ToList();

            string Json = JsonConvert.SerializeObject(new { value = Bardata, counter });

            // Add the data as a JSON string to the result
            return Json;
        }

        public string GetWeekSumData(string date, int daysBack, int counter)
        {
            DateTime EndPeriod = DateTime.Parse(date).Date;
            DateTime StartPeriod = EndPeriod.AddDays(-daysBack);

            List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= StartPeriod && r.From < EndPeriod).Select(aa => new Helper
            {
                Name = aa.Name,
                From = aa.From > StartPeriod ? aa.From : StartPeriod,
                To = (DateTime)aa.To > EndPeriod ? EndPeriod : (DateTime)aa.To
            }).ToList();

            // Check if the current activity should also be shown in the graph.
            if (AppStateTracker.CurrentActivity != null && EndPeriod >= DateTime.Today)
                ActivityHelper.Add(new Helper
                {
                    Name = AppStateTracker.CurrentActivity.Name,
                    From = AppStateTracker.CurrentActivity.From > StartPeriod ? AppStateTracker.CurrentActivity.From : StartPeriod,
                    To = DateTime.Now
                });

            // Compute time between Start and finish of the activity rounding to hours
            foreach (Helper h in ActivityHelper)
            {
                h.Time = Math.Max(Math.Round((h.To - h.From).TotalHours, 2), 0);
            }

            Dictionary<string, Dictionary<string, double>> Grouping = ActivityHelper
                .GroupBy(ah => ah.Name.Split(new string[] { " - " }, StringSplitOptions.None).First())
                .ToDictionary(g => g.Key, g => g
                    .GroupBy(a => a.Name.Split(new string[] { " - " }, StringSplitOptions.None).Last())
                    .ToDictionary(gg => gg.Key, gg => gg.Sum(h => h.Time))
                );

            // Extract labels and titles
            List<Bardata> Bardata = Grouping
                .OrderBy(g => g.Key)
                .Select(g => new Bardata
                {
                    Label = g.Key,
                    Datasets = g.Value.Select(d => new Dataset
                    {
                        Title = d.Key,
                        Value = d.Value,
                        Color = AppStateTracker.ColorAssingments[g.Key + " - " + d.Key]
                    }).OrderBy(d => d.Title).ToList()
                }).OrderBy(d => d.Label).ToList();

            string Json = JsonConvert.SerializeObject(new { value = Bardata, counter });

            // Add the data as a JSON string to the result
            return Json;
        }

        public string GetSettings()
        {
            Models.Settings Settings = new Models.Settings
            {
                TimeNotificationVisible = Properties.Settings.Default.TimeNotificationVisible,
                TimeBeforeAskingAgain = Properties.Settings.Default.TimeBeforeAskingAgain,
                TimeSinceAppLastUsed = Properties.Settings.Default.TimeSinceAppLastUsed,
                DarkMode = Properties.Settings.Default.DarkMode,
                HotkeyDisabled = Properties.Settings.Default.HotkeyDisabled,
                Hotkeys = Properties.Settings.Default.Hotkeys.Select(k => KeyInterop.VirtualKeyFromKey(k)).ToList(),
                PlayNotificationSound = Properties.Settings.Default.PlayNotificationSound
            };

            string Json = JsonConvert.SerializeObject(Settings);

            // Add the data as a JSON string to the result
            return Json;
        }

        public void SetSettings(IDictionary<string, object> settings)
        {
            Properties.Settings.Default.TimeNotificationVisible = (int)settings["TimeNotificationVisible"];
            Properties.Settings.Default.TimeBeforeAskingAgain = (int)settings["TimeBeforeAskingAgain"];
            Properties.Settings.Default.TimeSinceAppLastUsed = (int)settings["TimeSinceAppLastUsed"];
            Properties.Settings.Default.DarkMode = (bool)settings["DarkMode"];
            Properties.Settings.Default.HotkeyDisabled = (bool)settings["HotkeyDisabled"];
            List<Object> HotkeyList = settings["Hotkeys"] as List<Object>;
            Properties.Settings.Default.Hotkeys = HotkeyList.Cast<int>().Select(k => KeyInterop.KeyFromVirtualKey(k)).ToList();
            Properties.Settings.Default.Save();
        }

        public string SetDarkMode(bool darkMode)
        {
            Properties.Settings.Default.DarkMode = darkMode;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.DarkMode);
        }

        public string SetPlayNotificationSound(bool playNotificationSound)
        {
            Properties.Settings.Default.PlayNotificationSound = playNotificationSound;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.PlayNotificationSound);
        }

        public string SetHotkeyDisabled(bool hotkeyDisabled)
        {
            Properties.Settings.Default.HotkeyDisabled = hotkeyDisabled;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.HotkeyDisabled);
        }

        public string SetOfflineTracking(bool offlineTracking)
        {
            Properties.Settings.Default.OfflineTracking = offlineTracking;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.OfflineTracking);
        }

        public string SetTimeNotificationVisible(string timeNotificationVisible)
        {
            Properties.Settings.Default.TimeNotificationVisible = Int32.Parse(timeNotificationVisible);
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.TimeNotificationVisible);
        }

        public string SetTimeBeforeAskingAgain(string timeBeforeAskingAgain)
        {
            Properties.Settings.Default.TimeBeforeAskingAgain = Int32.Parse(timeBeforeAskingAgain);
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.TimeBeforeAskingAgain);
        }

        public string SetTimeSinceAppLastUsed(string timeSinceAppLastUsed)
        {
            Properties.Settings.Default.TimeSinceAppLastUsed = Int32.Parse(timeSinceAppLastUsed);
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.TimeSinceAppLastUsed);
        }

        public string SetHotkeys(List<Object> hotkeys)
        {
            Properties.Settings.Default.Hotkeys = hotkeys.Cast<int>().Select(k => KeyInterop.KeyFromVirtualKey(k)).ToList();
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.Hotkeys.Select(k => KeyInterop.VirtualKeyFromKey(k)).ToList());
        }

        public string GetTrackingSettings()
        {
            Models.TrackingSettings Settings = new TrackingSettings
            {
                OfflineTracking = Properties.Settings.Default.OfflineTracking,
                Blacklist = Properties.Settings.Default.Blacklist.Cast<string>().ToList()
            };

            string Json = JsonConvert.SerializeObject(Settings);

            // Add the data as a JSON string to the result
            return Json;
        }

        public string SetBlacklist(List<Object> blacklist)
        {
            StringCollection stringCollection = new StringCollection();
            foreach (string item in blacklist.Cast<string>())
            {
                stringCollection.Add(item);
            }
            Properties.Settings.Default.Blacklist = stringCollection;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.Blacklist.Cast<string>().ToList());
        }

        /// <summary>
        /// Opens a browser window poiting to the provided link
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event (contains the url to which the browser should point)</param>
        public void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public string GetActivities()
        {
            List<string> Activities = StorageHandler.GetActivitiesByLambda(a => true).GroupBy(a => a.Name).Select(g => g.Key).ToList();
            return JsonConvert.SerializeObject(Activities);
        }

        public string GetReportData1(List<Object> activities, string start, string end, int zoom)
        {
            List<string> Activities = activities.Cast<string>().ToList();
            DateTime StartPeriod = DateTime.Parse(start).Date;
            DateTime EndPeriod = DateTime.Parse(end).Date;

            List<(DateTime, Dictionary<string, double>)> Days = new List<(DateTime, Dictionary<string, double>)>();

            foreach (DateTime Day in EachDay(StartPeriod, EndPeriod))
            {
                DateTime Start = Day;
                DateTime End = Day.AddDays(1);

                List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= Start && r.From < End && Activities.Contains(r.Name)).Select(aa => new Helper
                {
                    Name = aa.Name.Split(new string[] { " - " }, StringSplitOptions.None).First(),
                    From = aa.From > Start ? aa.From : Start,
                    To = (DateTime)aa.To > End ? End : (DateTime)aa.To
                }).ToList();

                // Check if the current activity should also be shown in the graph.
                if (AppStateTracker.CurrentActivity != null && End >= DateTime.Today && Activities.Contains(AppStateTracker.CurrentActivity.Name))
                    ActivityHelper.Add(new Helper
                    {
                        Name = AppStateTracker.CurrentActivity.Name.Split(new string[] { " - " }, StringSplitOptions.None).First(),
                        From = AppStateTracker.CurrentActivity.From > Start ? AppStateTracker.CurrentActivity.From : Start,
                        To = DateTime.Now
                    });

                // Compute time between Start and finish of the activity rounding to hours
                foreach (Helper h in ActivityHelper)
                {
                    h.Time = Math.Max(Math.Round((h.To - h.From).TotalHours, 2), 0);
                }

                Dictionary<string, double> ActivityDict = new Dictionary<string, double>();

                ActivityHelper
                    .GroupBy(ah => ah.Name)
                    .ToList()
                    .ForEach(g =>
                    {
                        ActivityDict.Add(g.Key, g.Sum(h => h.Time));
                    });

                Days.Add((Day.Date, ActivityDict));
            }

            // construct data
            List<Bardata> Bardata;
            if (zoom == 0)
            {
                Bardata = Days.Select(d => new Bardata
                {
                    Label = d.Item1.ToString("dd.MM"),
                    Datasets = d.Item2.Select(h => new Dataset
                    {
                        Title = h.Key,
                        Value = h.Value,
                        Color = AppStateTracker.ColorAssingments[h.Key]
                    }).OrderBy(dd => dd.Title).ToList()
                }).OrderBy(d => d.Label).ToList();
            }
            else if (zoom == 1)
            {
                Bardata = Days
                    .GroupBy(d => new
                    {
                        week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d.Item1, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                        year = d.Item1.Year
                    })
                    .Select(g => new Bardata
                    {
                        Label = "Week " + (g.Key.week + 1),
                        Datasets = g.SelectMany(d => d.Item2).GroupBy(d => d.Key).Select(gg => new Dataset { Title = gg.Key, Value = gg.Sum(d => d.Value), Color = AppStateTracker.ColorAssingments[gg.Key] }).OrderBy(d => d.Title).ToList()
                    }).OrderBy(d => d.Label).ToList();
            }
            else
            {
                Bardata = Days
                    .GroupBy(d => new
                    {
                        month = d.Item1.ToString("MMM", CultureInfo.InvariantCulture),
                        year = d.Item1.Year
                    })
                    .Select(g => new Bardata
                    {
                        Label = g.Key.month,
                        Datasets = g.SelectMany(d => d.Item2).GroupBy(d => d.Key).Select(gg => new Dataset { Title = gg.Key, Value = gg.Sum(d => d.Value), Color = AppStateTracker.ColorAssingments[gg.Key] }).OrderBy(d => d.Title).ToList()
                    }).OrderBy(d => d.Label).ToList();
            }

            string Json = JsonConvert.SerializeObject(Bardata);

            // Add the data as a JSON string to the result
            return Json;
        }

        public string GetReportData2(List<Object> activities, string start, string end)
        {
            List<string> Activities = activities.Cast<string>().ToList();
            DateTime StartPeriod = DateTime.Parse(start).Date;
            DateTime EndPeriod = DateTime.Parse(end).Date;

            List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= StartPeriod && r.From < EndPeriod && Activities.Contains(r.Name)).Select(aa => new Helper
            {
                Name = aa.Name,
                From = aa.From > StartPeriod ? aa.From : StartPeriod,
                To = (DateTime)aa.To > EndPeriod ? EndPeriod : (DateTime)aa.To
            }).ToList();

            // Check if the current activity should also be shown in the graph.
            if (AppStateTracker.CurrentActivity != null && EndPeriod >= DateTime.Today && Activities.Contains(AppStateTracker.CurrentActivity.Name))
                ActivityHelper.Add(new Helper
                {
                    Name = AppStateTracker.CurrentActivity.Name,
                    From = AppStateTracker.CurrentActivity.From > StartPeriod ? AppStateTracker.CurrentActivity.From : StartPeriod,
                    To = DateTime.Now
                });

            // Compute time between Start and finish of the activity rounding to hours
            foreach (Helper h in ActivityHelper)
            {
                h.Time = Math.Max(Math.Round((h.To - h.From).TotalHours, 2), 0);
            }

            Dictionary<string, Dictionary<string, double>> Grouping = ActivityHelper
                .GroupBy(ah => ah.Name.Split(new string[] { " - " }, StringSplitOptions.None).First())
                .ToDictionary(g => g.Key, g => g
                    .GroupBy(a => a.Name.Split(new string[] { " - " }, StringSplitOptions.None).Last())
                    .ToDictionary(gg => gg.Key, gg => gg.Sum(h => h.Time))
                );

            // Extract labels and titles
            List<Bardata> Bardata = Grouping
                .OrderBy(g => g.Key)
                .Select(g => new Bardata
                {
                    Label = g.Key,
                    Datasets = g.Value.Select(d => new Dataset
                    {
                        Title = d.Key,
                        Value = d.Value,
                        Color = AppStateTracker.ColorAssingments[g.Key + " - " + d.Key]
                    }).OrderBy(d => d.Title).ToList()
                }).OrderBy(d => d.Label).ToList();

            string Json = JsonConvert.SerializeObject(Bardata);

            // Add the data as a JSON string to the result
            return Json;

        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }

}
