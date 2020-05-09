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
using System.Threading;
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

        private Thread GetDayDataThread = null;
        private async Task<string> GetDayDataAsync(string date, int counter)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            string Json = "";
            await Task.Factory.StartNew(() =>
            {
                GetDayDataThread = Thread.CurrentThread;

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
                        .Where(t => AppStateTracker.ColorAssingments.ContainsKey(t.Title))
                        .Select(t => AppStateTracker.ColorAssingments[t.Title])
                        .ToList()
                    })
                    .OrderBy(t => t.Label)
                    .ToList();

                Json = JsonConvert.SerializeObject(new { value = Timelines, counter });
            });

            GetDayDataThread = null;
            tcs.SetResult(Json);
            return tcs.Task.Result;
        }

        public string GetDayData(string date, int counter)
        {
            if (GetDayDataThread != null)
            {
                GetDayDataThread.Abort();
                GetDayDataThread = null;
            }

            var task = GetDayDataAsync(date, counter);

            return task.Result;
        }

        private Thread GetWeekBreakdownDataThread = null;

        private async Task<string> GetWeekBreakdownDataAsync(string date, int daysBack, int counter)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            string Json = "";
            await Task.Factory.StartNew(() =>
            {
                GetWeekBreakdownDataThread = Thread.CurrentThread;

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
                    if (AppStateTracker.CurrentActivity != null && StartPeriod == DateTime.Today)
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
                    Datasets = d.Item2
                    .Where(h => AppStateTracker.ColorAssingments.ContainsKey(h.Key))
                    .Select(h => new Dataset
                    {
                        Title = h.Key,
                        Value = h.Value,
                        Color = AppStateTracker.ColorAssingments[h.Key]
                    }).OrderBy(dd => dd.Title).ToList()
                }).ToList();

                Json = JsonConvert.SerializeObject(new { value = Bardata, counter });
            });

            GetWeekBreakdownDataThread = null;
            tcs.SetResult(Json);
            return tcs.Task.Result;
        }

        public string GetWeekBreakdownData(string date, int daysBack, int counter)
        {
            if (GetWeekBreakdownDataThread != null)
            {
                GetWeekBreakdownDataThread.Abort();
                GetWeekBreakdownDataThread = null;
            }

            var task = GetWeekBreakdownDataAsync(date, daysBack, counter);

            return task.Result;
        }

        private Thread GetWeekSumDataThread = null;

        private async Task<string> GetWeekSumDataAsync(string date, int daysBack, int counter)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            string Json = "";
            await Task.Factory.StartNew(() =>
            {
                GetWeekSumDataThread = Thread.CurrentThread;
                DateTime Date = DateTime.Parse(date);
                int DayOfWeek = (int)Date.DayOfWeek;
                List<int> Mapping = new List<int> { 7, 1, 2, 3, 4, 5, 6 };
                DateTime EndPeriod = Date.AddDays(7 - Mapping[DayOfWeek]);
                DateTime StartPeriod = Date.AddDays(-Mapping[DayOfWeek]);

                List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= StartPeriod && r.From <= EndPeriod).Select(aa => new Helper
                {
                    Name = aa.Name,
                    From = aa.From > StartPeriod ? aa.From : StartPeriod,
                    To = (DateTime)aa.To > EndPeriod ? EndPeriod : (DateTime)aa.To
                }).ToList();

                // Check if the current activity should also be shown in the graph.
                if (AppStateTracker.CurrentActivity != null && EndPeriod == DateTime.Today)
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
                        Datasets = g.Value
                        .Where(d => AppStateTracker.ColorAssingments.ContainsKey(g.Key + " - " + d.Key))
                        .Select(d => new Dataset
                        {
                            Title = d.Key,
                            Value = d.Value,
                            Color = AppStateTracker.ColorAssingments[g.Key + " - " + d.Key]
                        }).OrderBy(d => d.Title).ToList()
                    }).OrderBy(d => d.Label).ToList();

                Json = JsonConvert.SerializeObject(new { value = Bardata, counter });
            });

            GetWeekSumDataThread = null;
            tcs.SetResult(Json);
            return tcs.Task.Result;
        }

        public string GetWeekSumData(string date, int daysBack, int counter)
        {
            if (GetWeekSumDataThread != null)
            {
                GetWeekSumDataThread.Abort();
                GetWeekSumDataThread = null;
            }

            var task = GetWeekSumDataAsync(date, daysBack, counter);

            return task.Result;
        }

        public string GetSettings()
        {
            List<System.Windows.Input.Key> Hotkeys = Properties.Settings.Default.Hotkeys ?? new List<System.Windows.Input.Key>();
            Models.Settings Settings = new Models.Settings
            {
                TimeNotificationVisible = Properties.Settings.Default.TimeNotificationVisible,
                TimeBeforeAskingAgain = Properties.Settings.Default.TimeBeforeAskingAgain,
                TimeSinceAppLastUsed = Properties.Settings.Default.TimeSinceAppLastUsed,
                OfflineTracking = Properties.Settings.Default.OfflineTracking,
                HotkeyDisabled = Properties.Settings.Default.HotkeyDisabled,
                Hotkeys = Hotkeys.Select(k => KeyInterop.VirtualKeyFromKey(k)).ToList(),
                PlayNotificationSound = Properties.Settings.Default.PlayNotificationSound
            };

            string Json = JsonConvert.SerializeObject(Settings);

            // Add the data as a JSON string to the result
            return Json;
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

        public string SetTimeNotificationVisible(int timeNotificationVisible)
        {
            Properties.Settings.Default.TimeNotificationVisible = timeNotificationVisible;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.TimeNotificationVisible);
        }

        public string SetTimeBeforeAskingAgain(int timeBeforeAskingAgain)
        {
            Properties.Settings.Default.TimeBeforeAskingAgain = timeBeforeAskingAgain;
            Properties.Settings.Default.Save();

            return JsonConvert.SerializeObject(Properties.Settings.Default.TimeBeforeAskingAgain);
        }

        public string SetTimeSinceAppLastUsed(int timeSinceAppLastUsed)
        {
            Properties.Settings.Default.TimeSinceAppLastUsed = timeSinceAppLastUsed;
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

        private Thread GetReportData1Thread = null;

        private async Task<string> GetReportData1Async(List<Object> activities, string start, string end, int zoom, int counter)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            string Json = "";
            await Task.Factory.StartNew(() =>
            {
                GetReportData1Thread = Thread.CurrentThread;

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
                    if (AppStateTracker.CurrentActivity != null && Start == DateTime.Today && Activities.Contains(AppStateTracker.CurrentActivity.Name))
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
                    Bardata = Days.OrderBy(d => d.Item1).Select(d => new Bardata
                    {
                        Label = d.Item1.ToString("dd.MM"),
                        Datasets = d.Item2
                        .Where(h => AppStateTracker.ColorAssingments.ContainsKey(h.Key))
                        .Select(h => new Dataset
                        {
                            Title = h.Key,
                            Value = h.Value,
                            Color = AppStateTracker.ColorAssingments[h.Key]
                        }).OrderBy(dd => dd.Title).ToList()
                    }).ToList();
                }
                else if (zoom == 1)
                {
                    Bardata = Days
                        .GroupBy(d => new
                        {
                            week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d.Item1, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                            year = d.Item1.Year
                        })
                        .OrderBy(g => g.Key.year)
                        .ThenBy(g => g.Key.week)
                        .Select(g => new Bardata
                        {
                            Label = "Week " + (g.Key.week + 1),
                            Datasets = g.SelectMany(d => d.Item2).GroupBy(d => d.Key)
                            .Where(gg => AppStateTracker.ColorAssingments.ContainsKey(gg.Key))
                            .Select(gg => new Dataset { Title = gg.Key, Value = gg.Sum(d => d.Value), Color = AppStateTracker.ColorAssingments[gg.Key] }).OrderBy(d => d.Title).ToList()
                        }).ToList();
                }
                else
                {
                    Bardata = Days
                        .OrderBy(d => d.Item1)
                        .GroupBy(d => new
                        {
                            month = d.Item1.ToString("MMM", CultureInfo.InvariantCulture),
                            year = d.Item1.Year
                        })
                        .Select(g => new Bardata
                        {
                            Label = g.Key.month,
                            Datasets = g.SelectMany(d => d.Item2).GroupBy(d => d.Key)
                            .Where(gg => AppStateTracker.ColorAssingments.ContainsKey(gg.Key))
                            .Select(gg => new Dataset { Title = gg.Key, Value = gg.Sum(d => d.Value), Color = AppStateTracker.ColorAssingments[gg.Key] }).OrderBy(d => d.Title).ToList()
                        }).ToList();
                }

                Json = JsonConvert.SerializeObject(new { value = Bardata, counter });
            });

            GetReportData1Thread = null;
            tcs.SetResult(Json);
            return tcs.Task.Result;
        }

        public string GetReportData1(List<Object> activities, string start, string end, int zoom, int counter)
        {
            if (GetReportData1Thread != null)
            {
                GetReportData1Thread.Abort();
                GetReportData1Thread = null;
            }

            var task = GetReportData1Async(activities, start, end, zoom, counter);

            return task.Result;
        }

        private Thread GetReportData2Thread = null;
        private async Task<string> GetReportData2Async(List<Object> activities, string start, string end, int counter)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            string Json = "";
            await Task.Factory.StartNew(() =>
            {
                GetReportData2Thread = Thread.CurrentThread;

                List<string> Activities = activities.Cast<string>().ToList();
                DateTime StartPeriod = DateTime.Parse(start).Date.AddDays(1);
                DateTime EndPeriod = DateTime.Parse(end).Date;

                List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= StartPeriod && r.From <= EndPeriod && Activities.Contains(r.Name)).Select(aa => new Helper
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
                        Datasets = g.Value
                        .Where(d => AppStateTracker.ColorAssingments.ContainsKey(g.Key + " - " + d.Key))
                        .Select(d => new Dataset
                        {
                            Title = d.Key,
                            Value = d.Value,
                            Color = AppStateTracker.ColorAssingments[g.Key + " - " + d.Key]
                        }).OrderBy(d => d.Title).ToList()
                    }).OrderBy(d => d.Label).ToList();

                Json = JsonConvert.SerializeObject(new { value = Bardata, counter });
            });

            GetReportData2Thread = null;
            tcs.SetResult(Json);
            return tcs.Task.Result;
        }

        public string GetReportData2(List<Object> activities, string start, string end, int counter)
        {
            if (GetReportData2Thread != null)
            {
                GetReportData2Thread.Abort();
                GetReportData2Thread = null;
            }

            var task = GetReportData2Async(activities, start, end, counter);

            return task.Result;
        }

        private Thread GetReportData3Thread = null;
        private async Task<string> GetReportData3Async(List<Object> activities, string start, string end, int counter)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            string Json = "";
            await Task.Factory.StartNew(() =>
            {
                GetReportData3Thread = Thread.CurrentThread;

                List<string> Activities = activities.Cast<string>().ToList();
                DateTime StartPeriod = DateTime.Parse(start).Date.AddDays(1);
                DateTime EndPeriod = DateTime.Parse(end).Date;

                List<Window> Windows = StorageHandler.GetWindowsByLambda(r => r.To >= StartPeriod && r.From <= EndPeriod);


                List<Helper> ActivityHelper = StorageHandler.GetActivitiesByLambda(r => r.To >= StartPeriod && r.From <= EndPeriod && Activities.Contains(r.Name)).Select(aa => new Helper
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

                List<Helper> WindowHelper = new List<Helper>();

                foreach (Helper h in ActivityHelper)
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

                // Compute time between Start and finish of the activity rounding to minutes
                foreach (Helper h in WindowHelper)
                {
                    h.Time = Math.Max(Math.Round((h.To - h.From).TotalHours, 2), 0);
                }

                // Extract labels and titles
                List<Piedata> PiedataHelper = WindowHelper
                    .GroupBy(h => h.Name)
                    .Select(g => new Piedata
                    {
                        Label = g.Key,
                        Value = g.Sum(h => h.Time)
                    })
                    .OrderByDescending(p => p.Value)
                    .ToList();

                Piedata Others = new Piedata
                {
                    Label = "Others",
                    Value = 0
                };
                List<Piedata> Piedata = new List<Piedata>();

                for (int i = 0; i < PiedataHelper.Count; i++)
                {
                    if(i < 10)
                    {
                        Piedata.Add(PiedataHelper[i]);
                    } else
                    {
                        Others.Value += PiedataHelper[i].Value;
                    }
                }

                Piedata.Add(Others);

                Json = JsonConvert.SerializeObject(new { value = Piedata, counter });
            });

            GetReportData3Thread = null;
            tcs.SetResult(Json);
            return tcs.Task.Result;
        }

        public string GetReportData3(List<Object> activities, string start, string end, int counter)
        {
            if (GetReportData3Thread != null)
            {
                GetReportData3Thread.Abort();
                GetReportData3Thread = null;
            }

            var task = GetReportData3Async(activities, start, end, counter);

            return task.Result;
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }

}
