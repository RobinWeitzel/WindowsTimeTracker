using System;
using System.Collections.Generic;
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
            this.WebBrowser.Source = new Uri(String.Format("file:///{0}DataView.html", curDir));
            WebBrowser.ObjectForScripting = new MyScriptingClass();
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
            public string getActivityBreakdown(int value)
            {
                DateTime day_in_question = DateTime.Today.AddDays(value);
                DateTime day_in_question_after = day_in_question.AddDays(1);
                using (mainEntities db = new mainEntities())
                {
                    List<activity_active> today_activities = db.activity_active.Where(aa =>
                    (aa.to == null || aa.to >= day_in_question) &&
                    aa.from < day_in_question_after
                    ).ToList();

                    List<Event> Events = today_activities.Select(ta => new Event
                    {
                        from = Math.Round(ta.from >= day_in_question ? ta.from.Subtract(day_in_question).TotalHours : 0, 2),
                        to = Math.Round((ta.from >= day_in_question ? ta.from.Subtract(day_in_question).TotalHours : 0) + (ta.to == null || ta.to >= day_in_question_after ? (day_in_question == DateTime.Today ? DateTime.Now : day_in_question_after) : (DateTime)ta.to).Subtract(ta.from >= day_in_question ? ta.from : day_in_question).TotalHours, 2),
                        name = ta.name
                    }).ToList();

                    return customJSONSerializer<Event>(Events);
                }
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

            public string getWindows(int value)
            {
                DateTime day_in_question = DateTime.Today.AddDays(value);
                DateTime day_in_question_after = day_in_question.AddDays(1);
                using (mainEntities db = new mainEntities())
                {
                    List<Helper> windowHelper = db.window_active
                            .Where(wa => (wa.to == null || wa.to >= day_in_question) && wa.from < day_in_question_after)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > day_in_question ? wa.from : day_in_question,
                                to = wa.to ?? (day_in_question_after > DateTime.Now ? DateTime.Now : day_in_question_after)
                            }).ToList();

                    foreach (Helper h in windowHelper)
                    {
                        h.time = Math.Max(Math.Round((h.to - h.from).TotalHours, 2), 0);
                    }

                    return customJSONSerializer<Helper2>(windowHelper.GroupBy(h => h.name).Where(g => g.Sum(h => h.time) >= 0.1).Select(g => new Helper2
                    {
                        name = g.Key,
                        value = g.Sum(h => h.time)
                    }).ToList());
                }
            }

            public string getActivities(int value)
            {
                DateTime start_of_week = DateTime.Today.AddDays(value).StartOfWeek(DayOfWeek.Monday);
                DateTime start_next_week = start_of_week.AddDays(7);
                using (mainEntities db = new mainEntities())
                {
                    DateTime minDate = DateTime.Now.AddDays(-30);
                    List<Helper> activityHelper = db.activity_active
                       .Where(aa => (aa.to == null || aa.to >= start_of_week) && aa.from < start_next_week)
                       .Select(aa => new Helper
                       {
                           name = aa.name,
                           from = aa.from > start_of_week ? aa.from : start_of_week,
                           to = aa.to ?? (start_next_week > DateTime.Now ? DateTime.Now : start_next_week)
                       }).ToList();

                    foreach (Helper h in activityHelper)
                    {
                        h.time = Math.Max(Math.Round((h.to - h.from).TotalHours, 2), 0);
                    }

                    return customJSONSerializer<Helper2>(activityHelper.GroupBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Where(g => g.Sum(h => h.time) >= 0.1).Select(g => new Helper2
                    {
                        name = g.Key,
                        value = g.Sum(h => h.time)
                    }).ToList());
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
