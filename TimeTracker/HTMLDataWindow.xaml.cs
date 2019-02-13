using CefSharp;
using CefSharp.Wpf;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeTracker
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

    /// <summary>
    /// Interaktionslogik für HTMLDataWindow.xaml
    /// </summary>
    public partial class HTMLDataWindow : Window
    {
        public HTMLDataWindow()
        {
            InitializeComponent();

            string curDir = AppDomain.CurrentDomain.BaseDirectory;

            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //WebBrowser.RegisterJsObject("callbackObj", new MyScriptingClass());
            //WebBrowser.ObjectForScripting = new MyScriptingClass();

            if (!Cef.IsInitialized)
            {
                CefSettings cefSettings = new CefSettings();
                cefSettings.BrowserSubprocessPath = String.Format("{0}CefSharp.BrowserSubprocess.exe", curDir); // **Path where the CefSharp.BrowserSubprocess.exe exists**
                cefSettings.CachePath = "ChromiumBrowserControlCache";
                cefSettings.IgnoreCertificateErrors = true;
                Cef.Initialize(cefSettings);
            }
            
            WebBrowser.Address = String.Format("file:///{0}DataView.html", curDir);
            WebBrowser.JavascriptObjectRepository.Register("boundAsync", new MyScriptingClass(), true);
        }

        [ComVisible(true)]
        public class MyScriptingClass
        {

            public class Event
            {
                public string name { get; set; }
                public double from { get; set; }
                public double to { get; set; }
            }

            public class Helper
            {
                public string name { get; set; }
                public DateTime from { get; set; }
                public DateTime to { get; set; }
                public double time { get; set; }
            }

            public class Helper2
            {
                public string name { get; set; }
                public double value { get; set; }
            }

            public class Helper3
            {
                public string date { get; set; }
                public double timeSpent { get; set; }
            }

            public string getOverviewData(int value)
            {
                string result = "{";

                DateTime day_in_question = DateTime.Today.AddDays(value);
                DateTime day_in_question_after = day_in_question.AddDays(1);

                DateTime start_of_week = DateTime.Today.AddDays(value).StartOfWeek(DayOfWeek.Monday);
                DateTime start_next_week = start_of_week.AddDays(7);

                List<TimeTracker.Helper.Activity> today_activities;

                using (TextReader tr = new StreamReader(Variables.activityPath))
                {
                    var csv = new CsvReader(tr);
                    var records = csv.GetRecords<TimeTracker.Helper.Activity>();
                    today_activities = records.Where(r => r.To >= day_in_question && r.From < day_in_question_after).ToList();

                    if (Variables.currentActivity != null && (day_in_question_after > DateTime.Now && day_in_question < DateTime.Now))
                        today_activities.Add(Variables.currentActivity);

                    List<Event> Events = today_activities.Select(ta => new Event
                    {
                        from = Math.Round(ta.From >= day_in_question ? ta.From.Subtract(day_in_question).TotalHours : 0, 2),
                        to = Math.Round((ta.From >= day_in_question ? ta.From.Subtract(day_in_question).TotalHours : 0) + (ta.To == null || ta.To >= day_in_question_after ? (day_in_question == DateTime.Today ? DateTime.Now : day_in_question_after) : (DateTime)ta.To).Subtract(ta.From >= day_in_question ? ta.From : day_in_question).TotalHours, 2),
                        name = ta.Name
                    }).OrderBy(ta => ta.from).ToList();

                    result += "\"overview\":" + customJSONSerializer<Event>(Events) + ",";
                }

                using (TextReader tr = new StreamReader(Variables.activityPath))
                {
                    var csv = new CsvReader(tr);
                    var records = csv.GetRecords<TimeTracker.Helper.Activity>();

                    DateTime minDate = DateTime.Now.AddDays(-30);
                    List<Helper> activityHelper = records
                       .Where(r => r.To >= start_of_week && r.From < start_next_week)
                       .Select(aa => new Helper
                       {
                           name = aa.Name,
                           from = aa.From > start_of_week ? aa.From : start_of_week,
                           to = (DateTime)aa.To
                       }).ToList();

                    if (Variables.currentActivity != null && start_next_week >= DateTime.Today)
                        activityHelper.Add(new Helper {
                            name = Variables.currentActivity.Name,
                            from = Variables.currentActivity.From > start_of_week ? Variables.currentActivity.From : start_of_week,
                            to = DateTime.Now
                        });

                    foreach (Helper h in activityHelper)
                    {
                        h.time = Math.Max(Math.Round((h.to - h.from).TotalHours, 2), 0);
                    }

                    result += "\"activities\":" + customJSONSerializer<Helper2>(activityHelper.GroupBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Where(g => g.Sum(h => h.time) >= 0.1).Select(g => new Helper2
                    {
                        name = g.Key,
                        value = g.Sum(h => h.time)
                    }).ToList()) + ",";
                }

                using (TextReader tr = new StreamReader(Variables.windowPath))
                {
                    var csv = new CsvReader(tr);
                    var records = csv.GetRecords<TimeTracker.Helper.Window>();
                    List<TimeTracker.Helper.Window> list = records.Where(r => r.To >= day_in_question && r.From <= day_in_question_after).ToList();

                    List<Helper> Helper = today_activities.Select(ta => new Helper
                    {
                        from = ta.From > day_in_question ? ta.From : day_in_question,
                        to = ta.To ?? (day_in_question_after > DateTime.Now ? DateTime.Now : day_in_question_after),
                        name = ta.Name
                    }).ToList();

                    List<Helper> windowHelper = new List<Helper>();

                    foreach (Helper h in Helper)
                    {
                        windowHelper.AddRange(
                        list
                            .Where(r => r.To >= h.from && r.From <= h.to)
                            .Select(r => new Helper
                            {
                                name = r.Name,
                                from = r.From > h.from ? r.From : h.from, // If my window started before the start of the activity only measure from the beginning of the activity
                                to = (DateTime) (r.To > h.to ? h.to : r.To) // If my window ended after the end of the activity only measure until the end of the activity
                            }));

                        if (Variables.currentWindow != null)
                            windowHelper.Add(new Helper {
                                name = Variables.currentWindow.Name,
                                from = Variables.currentWindow.From < h.from ? h.from : Variables.currentWindow.From, // If my window started before the start of the activity only measure from the beginning of the activity
                                to = h.to
                            });
                    }

                    foreach (Helper h in windowHelper)
                    {
                        h.time = Math.Max((h.to - h.from).TotalHours, 0);
                    }

                    result += "\"windows\":" + customJSONSerializer<Helper2>(windowHelper.GroupBy(h => h.name).Where(g => g.Sum(h => h.time) >= 0.1).Select(g => new Helper2
                    {
                        name = g.Key,
                        value = Math.Round(g.Sum(h => h.time), 2)
                    }).ToList()) + "}";
                }
                return result;
            }

            private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                    yield return day;
            }

            public string getDetailsData1(string name, string startString, string endString)
            {
                string result = "{";
                DateTime start = DateTime.ParseExact(startString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(endString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                using (TextReader tr = new StreamReader(Variables.activityPath))
                {
                    var csv = new CsvReader(tr);
                    IEnumerable<TimeTracker.Helper.Activity> records = csv.GetRecords<TimeTracker.Helper.Activity>();
                    List<TimeTracker.Helper.Activity> allActivities = records.Where(r => r.To >= start && r.From <= end).ToList();

                    List<Helper> list = new List<Helper>();

                    foreach(var day in EachDay(start, end))
                    {
                        foreach(TimeTracker.Helper.Activity r in allActivities.Where(r => r.To >= day && r.From < day.AddDays(1))) {
                            list.Add(new Helper
                            {
                                name = r.Name,
                                from = r.From > day ? r.From : day, // If my window started before the start of the activity only measure from the beginning of the activity
                                to = (DateTime)(r.To > day.AddDays(1) ? day.AddDays(1) : r.To) // If my window ended after the end of the activity only measure until the end of the activity
                            });
                        }
                    }

                    foreach (Helper h in list)
                    {
                        h.time = Math.Max((h.to - h.from).TotalHours, 0);
                    }
 
                    List<Helper> filteredList = list.Where(h => h.name.Contains(name)).ToList();
                    List<Helper2> filteredNameGroupedList;
                    if (filteredList.GroupBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Count() == 1) // all activities belong to the same group
                    {
                        filteredNameGroupedList = filteredList.GroupBy(h => h.name).Select(g => new Helper2
                        {
                            name = g.Key,
                            value = g.Sum(h => h.time)
                        }).ToList();
                    } else
                    {
                        filteredNameGroupedList = filteredList.GroupBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Select(g => new Helper2
                        {
                            name = g.Key,
                            value = g.Sum(h => h.time)
                        }).ToList();
                    }
                    
                    List<Helper3> filteredDayGroupedList = filteredList.GroupBy(h => h.from.ToString("dd'.'MM'.'yyyy", CultureInfo.InvariantCulture)).Select(g => new Helper3
                    {
                        date = g.Key,
                        timeSpent = g.Sum(h => h.time)
                    }).ToList();

                    //double absoluteAveragePerDay = list.GroupBy(h => h.from.DayOfYear).Aggregate(0.0, (acc, g) => acc + g.Sum(h => h.time)) / (end - start).TotalDays;
                    //double filteredAbsoluteAveragePerDay = filteredList.GroupBy(h => h.from.DayOfYear).Aggregate(0.0, (acc, g) => acc + g.Sum(h => h.time)) / (end - start).TotalDays;
                    //double relativeAveragePerDay = absoluteAveragePerDay == 0 ? 0 : filteredAbsoluteAveragePerDay / absoluteAveragePerDay;

                    //double absoluteAveragePerWeek = list.GroupBy(h => Math.Ceiling(h.from.DayOfYear / 7.0)).Aggregate(0.0, (acc, g) => acc + g.Sum(h => h.time)) / Math.Ceiling((end - start).TotalDays / 7);
                    //double filteredAbsoluteAveragePerWeek = filteredList.GroupBy(h => Math.Ceiling(h.from.DayOfYear / 7.0)).Aggregate(0.0, (acc, g) => acc + g.Sum(h => h.time)) / Math.Ceiling((end - start).TotalDays / 7);
                    //double relativeAveragePerWeek = absoluteAveragePerWeek == 0 ? 0 : filteredAbsoluteAveragePerWeek / absoluteAveragePerWeek;

                    //double absoluteAveragePerMonth = list.GroupBy(h => h.from.Month).Aggregate(0.0, (acc, g) => acc + g.Sum(h => h.time)) / (end.Month - start.Month + 1);
                    //double filteredAbsoluteAveragePerMonth = filteredList.GroupBy(h => h.from.Month).Aggregate(0.0, (acc, g) => acc + g.Sum(h => h.time)) / (end.Month - start.Month + 1);
                    //double relativeAveragePerMonth = absoluteAveragePerMonth == 0 ? 0 : filteredAbsoluteAveragePerMonth / absoluteAveragePerMonth;

                    double absoluteAverageTotal = list.Sum(h => h.time);
                    double filteredAbsoluteAverageTotal = filteredList.Sum(h => h.time);
                    double relativeAverageTotal = absoluteAverageTotal == 0 ? 0 : filteredAbsoluteAverageTotal / absoluteAverageTotal;

                    double absoluteAveragePerDay = absoluteAverageTotal / (end - start).TotalDays;
                    double filteredAbsoluteAveragePerDay = filteredAbsoluteAverageTotal / (end - start).TotalDays;
                    double relativeAveragePerDay = absoluteAveragePerDay == 0 ? 0 : filteredAbsoluteAveragePerDay / absoluteAveragePerDay;

                    double absoluteAveragePerWeek = absoluteAverageTotal / Math.Ceiling((end - start).TotalDays / 7);
                    double filteredAbsoluteAveragePerWeek = filteredAbsoluteAverageTotal / Math.Ceiling((end - start).TotalDays / 7);
                    double relativeAveragePerWeek = absoluteAveragePerWeek == 0 ? 0 : filteredAbsoluteAveragePerWeek / absoluteAveragePerWeek;

                    double absoluteAveragePerMonth = absoluteAverageTotal / ((end.Year - start.Year) * 12 + end.Month - start.Month + 1);
                    double filteredAbsoluteAveragePerMonth = filteredAbsoluteAverageTotal / ((end.Year - start.Year) * 12 + end.Month - start.Month + 1);
                    double relativeAveragePerMonth = absoluteAveragePerMonth == 0 ? 0 : filteredAbsoluteAveragePerMonth / absoluteAveragePerMonth;

                    result += "\"filteredAbsoluteAveragePerDay\":" + filteredAbsoluteAveragePerDay.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";
                    result += "\"relativeAveragePerDay\":" + relativeAveragePerDay.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";

                    result += "\"filteredAbsoluteAveragePerWeek\":" + filteredAbsoluteAveragePerWeek.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";
                    result += "\"relativeAveragePerWeek\":" + relativeAveragePerWeek.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";

                    result += "\"filteredAbsoluteAveragePerMonth\":" + filteredAbsoluteAveragePerMonth.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";
                    result += "\"relativeAveragePerMonth\":" + relativeAveragePerMonth.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";

                    result += "\"filteredAbsoluteAverageTotal\":" + filteredAbsoluteAverageTotal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";
                    result += "\"relativeAverageTotal\":" + relativeAverageTotal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + ",";

                    result += "\"filteredDayGroupedList\":" + customJSONSerializer<Helper3>(filteredDayGroupedList) + ",";

                    result += "\"filteredNameGroupedList\":" + customJSONSerializer<Helper2>(filteredNameGroupedList) + "}";
                    return result;
                }
            }
        }

        public static bool IsNumericType(object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static string customJSONSerializer<T> (List<T> objs)
        {
            List<string> helper = new List<string>();

            foreach(T obj in objs)
            {
                Type type = obj.GetType();
                PropertyInfo[] properties = type.GetProperties();

                string helper2 = "{";

                foreach (PropertyInfo property in properties)
                {
                    string value;

                    if (property.PropertyType.Name.Equals("String"))
                        value = "\"" + property.GetValue(obj, null) + "\"";
                    else
                        value = ((double)property.GetValue(obj, null)).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                    helper2 += "\"" + property.Name + "\":" + value + ",";
                }

                helper2 = helper2.Remove(helper2.Length - 1);

                helper2 += "}";

                helper.Add(helper2);
            }

            return "[" + String.Join(", ", helper.ToArray()) + "]";
        }
    }
}
