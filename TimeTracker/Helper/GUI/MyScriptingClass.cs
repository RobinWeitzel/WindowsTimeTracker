using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

        public string GetDayData(string date)
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
                    }).OrderBy(tv => tv.Start).ToList()
                })
                .OrderBy(t => t.Label)
                .ToList();

            string Json = JsonConvert.SerializeObject(Timelines);

            // Add the data as a JSON string to the result
            return Json;
        }

        public string GetWeekBreakdownData(string date, int daysBack)
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
                    Value = h.Value
                }).OrderBy(dd => dd.Title).ToList()
            }).OrderBy(d => d.Label).ToList();

            string Json = JsonConvert.SerializeObject(Bardata);

            // Add the data as a JSON string to the result
            return Json;
        }

        public string GetWeekSumData(string date, int daysBack)
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
                        Value = d.Value
                    }).OrderBy(d => d.Title).ToList()
                }).OrderBy(d => d.Label).ToList();

            string Json = JsonConvert.SerializeObject(Bardata);

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
                PlayNotificationSound = Properties.Settings.Default.PlayNotificationSound,
                OfflineTracking = Properties.Settings.Default.OfflineTracking,
                Blacklist = Properties.Settings.Default.Blacklist.Cast<string>().ToList(),
            };

            string Json = JsonConvert.SerializeObject(Settings);

            // Add the data as a JSON string to the result
            return Json;
        }

        /*public string GetOverviewData(int value)
        {
            string Result = "{";

            DateTime DayInQuestion = DateTime.Today.AddDays(value);
            DateTime DayInQuestionAfter = DayInQuestion.AddDays(1);

            DateTime StartOfWeek = DateTime.Today.AddDays(value).StartOfWeek(DayOfWeek.Monday);
            DateTime StartOfNextWeek = StartOfWeek.AddDays(7);

            /////// Compute colors //////
            Dictionary<string, string> Colors = new ColorHandler().getColorDictionary(StorageHandler.GetLastActivitiesGrouped().Select(g => g.Key).ToList());
            new ColorHandler().getColorDictionary(StorageHandler.GetLastestWindowsGrouped().Select(g => g.Key).ToList(), false).ToList()
                .ForEach(g => {
                    if (!Colors.ContainsKey(g.Key))
                        Colors.Add(g.Key, g.Value);
                });

            string ColorsString = Colors.Aggregate("", (acc, kv) => acc += "\"" + kv.Key + "\":\"" + kv.Value + "\",");
            ColorsString = ColorsString.Remove(ColorsString.Length - 1); // Remove the last comma

            Result += "\"colors\": {" + ColorsString + "},";


            /////// Load the data for the timeline //////
            List<Activity> TodayActivities = StorageHandler.GetActivitiesByLambda(r => r.To >= DayInQuestion && r.From < DayInQuestionAfter);

            // Check if the current activity should also be shown in the graph.
            if (AppStateTracker.CurrentActivity != null && (DayInQuestionAfter > DateTime.Now && DayInQuestion < DateTime.Now))
                TodayActivities.Add(AppStateTracker.CurrentActivity);

            List<Event> Events = TodayActivities.Select(ta => new Event
            {
                From = Math.Round(ta.From >= DayInQuestion ? ta.From.Subtract(DayInQuestion).TotalHours : 0, 2),
                To = Math.Round((ta.From >= DayInQuestion ? ta.From.Subtract(DayInQuestion).TotalHours : 0) + (ta.To == null || ta.To >= DayInQuestionAfter ? (DayInQuestion == DateTime.Today ? DateTime.Now : DayInQuestionAfter) : (DateTime)ta.To).Subtract(ta.From >= DayInQuestion ? ta.From : DayInQuestion).TotalHours, 2),
                Name = ta.Name
            }).OrderBy(ta => ta.From).ToList();

            // Add the data as a JSON string to the result
            Result += "\"overview\":" + CustomJSONSerializer(Events) + ",";


            ////// Load the data for the left chart //////
            DateTime MinDate = DateTime.Now.AddDays(-30);

            List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= StartOfWeek && r.From < StartOfNextWeek).Select(aa => new Helper
            {
                Name = aa.Name,
                From = aa.From > StartOfWeek ? aa.From : StartOfWeek,
                To = (DateTime)aa.To
            }).ToList();

            // Check if the current activity should also be shown in the graph.
            if (AppStateTracker.CurrentActivity != null && StartOfNextWeek >= DateTime.Today)
                ActivityHelper.Add(new Helper
                {
                    Name = AppStateTracker.CurrentActivity.Name,
                    From = AppStateTracker.CurrentActivity.From > StartOfWeek ? AppStateTracker.CurrentActivity.From : StartOfWeek,
                    To = DateTime.Now
                });

            // Compute time between Start and finish of the activity rounding to hours
            foreach (Helper h in ActivityHelper)
            {
                h.Time = Math.Max(Math.Round((h.To - h.From).TotalHours, 2), 0);
            }

            // Add the data as a JSON string to the result
            Result += "\"activities\":" + CustomJSONSerializer(
                ActivityHelper
                .GroupBy(h => h.Name.Split(new string[] { " - " }, StringSplitOptions.None).First())
                .Where(g => g.Sum(h => h.Time) >= 0.1)
                .Select(g => new Helper2
                {
                    Name = g.Key,
                    Value = g.Sum(h => h.Time)
                })
                .ToList()) + ",";


            ////// Load the data for the right chart //////
            List<Window> Windows = StorageHandler.GetWindowsByLambda(r => r.To >= DayInQuestion && r.From <= DayInQuestionAfter);

            List<Helper> Helper = TodayActivities.Select(ta => new Helper
            {
                From = ta.From > DayInQuestion ? ta.From : DayInQuestion,
                To = ta.To ?? (DayInQuestionAfter > DateTime.Now ? DateTime.Now : DayInQuestionAfter),
                Name = ta.Name
            }).ToList();

            List<Helper> WindowHelper = new List<Helper>();

            foreach (Helper h in Helper)
            {
                WindowHelper.AddRange(
                Windows
                    .Where(r => r.To >= h.From && r.From <= h.To)
                    .Select(r => new Helper
                    {
                        Name = r.Name,
                        From = r.From > h.From ? r.From : h.From, // If my window started before the Start of the activity only measure from the beginning of the activity
                        To = (DateTime)(r.To > h.To ? h.To : r.To) // If my window ended after the End of the activity only measure until the End of the activity
                    }));

                // Check if the current window should also be shown in the graph.
                if (AppStateTracker.CurrentWindow != null)
                    WindowHelper.Add(new Helper
                    {
                        Name = AppStateTracker.CurrentWindow.Name,
                        From = AppStateTracker.CurrentWindow.From < h.From ? h.From : AppStateTracker.CurrentWindow.From, // If my window started before the Start of the activity only measure from the beginning of the activity
                        To = h.To
                    });
            }

            // Compute time between Start and finish of the window rounding to hours
            foreach (Helper h in WindowHelper)
            {
                h.Time = Math.Max((h.To - h.From).TotalHours, 0);
            }

            // Add the data as a JSON string to the result
            Result += "\"windows\":" + CustomJSONSerializer(WindowHelper.GroupBy(h => h.Name).Where(g => g.Sum(h => h.Time) >= 0.1).Select(g => new Helper2
            {
                Name = g.Key,
                Value = Math.Round(g.Sum(h => h.Time), 2)
            }).ToList()) + "}";

            return Result;
        }*/

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        /*public string GetDetailsData1(string name, string startString, string endString)
        {
            string Result = "{";
            DateTime Start = DateTime.ParseExact(startString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            DateTime End = DateTime.ParseExact(endString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            /////// Compute colors //////
            Dictionary<string, string> Colors = new ColorHandler().getColorDictionary(StorageHandler.GetEarliestActivitiesGrouped().Select(g => g.Key).ToList());

            string ColorsString = Colors.Aggregate("", (acc, kv) => acc += "\"" + kv.Key + "\":\"" + kv.Value + "\",");
            ColorsString = ColorsString.Remove(ColorsString.Length - 1); // Remove the last comma

            Result += "\"colors\": {" + ColorsString + "},";


            List<Activity> allActivities = StorageHandler.GetActivitiesByLambda(r => r.To >= Start && r.From <= End);
            List<Helper> Helpers = new List<Helper>();

            foreach (DateTime Day in EachDay(Start, End))
            {
                foreach (Activity r in allActivities.Where(r => r.To >= Day && r.From < Day.AddDays(1)))
                {
                    Helpers.Add(new Helper
                    {
                        Name = r.Name,
                        From = r.From > Day ? r.From : Day, // If my window started before the Start of the activity only measure from the beginning of the activity
                        To = (DateTime)(r.To > Day.AddDays(1) ? Day.AddDays(1) : r.To) // If my window ended after the End of the activity only measure until the End of the activity
                    });
                }
            }

            foreach (Helper h in Helpers)
            {
                h.Time = Math.Max((h.To - h.From).TotalHours, 0);
            }

            List<Helper> FilteredHelpers = Helpers.Where(h => h.Name.Contains(name)).ToList();
            List<Helper2> FilteredNameGroupedHelpers;
            if (FilteredHelpers.GroupBy(h => h.Name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Count() == 1) // all activities belong to the same group
            {
                FilteredNameGroupedHelpers = FilteredHelpers.GroupBy(h => h.Name).Select(g => new Helper2
                {
                    Name = g.Key,
                    Value = g.Sum(h => h.Time)
                }).ToList();
            }
            else
            {
                FilteredNameGroupedHelpers = FilteredHelpers.GroupBy(h => h.Name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Select(g => new Helper2
                {
                    Name = g.Key,
                    Value = g.Sum(h => h.Time)
                }).ToList();
            }

            List<Helper3> FilteredDayGroupedHelpers = FilteredHelpers.GroupBy(h => h.From.ToString("dd'.'MM'.'yyyy", CultureInfo.InvariantCulture)).Select(g => new Helper3
            {
                Date = g.Key,
                TimeSpent = g.Sum(h => h.Time)
            }).ToList();

            double AbsoluteAverageTotal = Helpers.Sum(h => h.Time);
            double FilteredAbsoluteAverageTotal = FilteredHelpers.Sum(h => h.Time);
            double RelativeAverageTotal = AbsoluteAverageTotal == 0 ? 0 : FilteredAbsoluteAverageTotal / AbsoluteAverageTotal;

            double AbsoluteAveragePerDay = AbsoluteAverageTotal / (End - Start).TotalDays;
            double FilteredAbsoluteAveragePerDay = FilteredAbsoluteAverageTotal / (End - Start).TotalDays;
            double RelativeAveragePerDay = AbsoluteAveragePerDay == 0 ? 0 : FilteredAbsoluteAveragePerDay / AbsoluteAveragePerDay;

            double AbsoluteAveragePerWeek = AbsoluteAverageTotal / Math.Ceiling((End - Start).TotalDays / 7);
            double FilteredAbsoluteAveragePerWeek = FilteredAbsoluteAverageTotal / Math.Ceiling((End - Start).TotalDays / 7);
            double RelativeAveragePerWeek = AbsoluteAveragePerWeek == 0 ? 0 : FilteredAbsoluteAveragePerWeek / AbsoluteAveragePerWeek;

            double AbsoluteAveragePerMonth = AbsoluteAverageTotal / ((End.Year - Start.Year) * 12 + End.Month - Start.Month + 1);
            double FilteredAbsoluteAveragePerMonth = FilteredAbsoluteAverageTotal / ((End.Year - Start.Year) * 12 + End.Month - Start.Month + 1);
            double RelativeAveragePerMonth = AbsoluteAveragePerMonth == 0 ? 0 : FilteredAbsoluteAveragePerMonth / AbsoluteAveragePerMonth;

            Result += "\"filteredAbsoluteAveragePerDay\":" + FilteredAbsoluteAveragePerDay.ToString("0.00", CultureInfo.InvariantCulture) + ",";
            Result += "\"relativeAveragePerDay\":" + RelativeAveragePerDay.ToString("0.00", CultureInfo.InvariantCulture) + ",";

            Result += "\"filteredAbsoluteAveragePerWeek\":" + FilteredAbsoluteAveragePerWeek.ToString("0.00", CultureInfo.InvariantCulture) + ",";
            Result += "\"relativeAveragePerWeek\":" + RelativeAveragePerWeek.ToString("0.00", CultureInfo.InvariantCulture) + ",";

            Result += "\"filteredAbsoluteAveragePerMonth\":" + FilteredAbsoluteAveragePerMonth.ToString("0.00", CultureInfo.InvariantCulture) + ",";
            Result += "\"relativeAveragePerMonth\":" + RelativeAveragePerMonth.ToString("0.00", CultureInfo.InvariantCulture) + ",";

            Result += "\"filteredAbsoluteAverageTotal\":" + FilteredAbsoluteAverageTotal.ToString("0.00", CultureInfo.InvariantCulture) + ",";
            Result += "\"relativeAverageTotal\":" + RelativeAverageTotal.ToString("0.00", CultureInfo.InvariantCulture) + ",";

            Result += "\"filteredDayGroupedList\":" + CustomJSONSerializer(FilteredDayGroupedHelpers) + ",";

            Result += "\"filteredNameGroupedList\":" + CustomJSONSerializer(FilteredNameGroupedHelpers) + "}";
            return Result;

        }*/
    }

}
