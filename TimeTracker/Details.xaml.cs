using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für Details.xaml
    /// </summary>
    public partial class Details : UserControl
    {
        public class Event
        {
            public TimeSpan Start { get; set; }
            public TimeSpan Duration { get; set; }
            public SolidColorBrush BColor { get; set; }
            public string Title { get; set; }
        }

        public class Timeline
        {
            public List<Event> Events { get; set; }
            public TimeSpan Duration { get; set; }
        }

        public ObservableCollection<Timeline> TimeLines { get; set; }
        public ObservableCollection<Timeline> TimeLines2 { get; set; }
        public Details()
        {
            InitializeComponent();

            loadData();

            this.DataContext = this;
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public Dictionary<string, SolidColorBrush> createColorMapper(List<string> activities)
        {
            Dictionary<string, SolidColorBrush> dict = new Dictionary<string, SolidColorBrush>();
            int counter = 0;
            Color color;
            foreach (string activity in activities.OrderBy(a => a))
            {
                if (Constants.useHashColors)
                {
                    byte[] hash = GetHash(activity);
                    byte r = hash[0];
                    byte g = hash[1];
                    byte b = hash[2];
                    color = Color.FromRgb(r, g, b);
                } else
                {
                    if (counter >= Constants.colors.Count())
                        counter = 0;

                    color = Constants.colors[counter];
                    counter++;
                }

                dict.Add(activity, new SolidColorBrush(color));
            }

            return dict;
        }

        public void loadData()
        {
            DateTime day_in_question = DatePicker.SelectedDate ?? DateTime.Today;
            DateTime day_in_question_after = day_in_question.AddDays(1);
            using (mainEntities db = new mainEntities())
            {
                List<activity_active> today_activities = db.activity_active.Where(aa => 
                (aa.to == null || aa.to >= day_in_question) &&
                aa.from < day_in_question_after
                ).ToList();

                Dictionary<string, SolidColorBrush> mapper = createColorMapper(today_activities.Select(ta => ta.name).Distinct().ToList());

                List<Event> Events = today_activities.Select(ta =>
                    new Event
                    {
                        Start = ta.from >= day_in_question ? ta.from.Subtract(day_in_question) : new TimeSpan(0),
                        Duration = (ta.to == null || ta.to >= day_in_question_after ? (day_in_question == DateTime.Today ? DateTime.Now : day_in_question_after) : (DateTime)ta.to).Subtract(ta.from >= day_in_question ? ta.from : day_in_question),
                        BColor = mapper[ta.name],
                        Title = ta.name
                    }
                ).ToList();

                // Black line for full hours
                for(int i = 1; i <= 24; i++)
                {
                    Events.Add(
                        new Event
                        {
                            Start = new TimeSpan(i, 0, 0).Add(new TimeSpan(0, 0, 30)),
                            Duration = new TimeSpan(0, 1, 0),
                            BColor = new SolidColorBrush(Colors.Black),
                            Title = ""
                        }
                    );
                }

                if(TimeLines == null)
                    TimeLines = new ObservableCollection<Timeline>();
                else
                    TimeLines.Clear();

                TimeLines.Add(new Timeline
                {
                    Events = Events,
                    Duration = new TimeSpan(24, 0, 0) // 1 Day
                });
                    
                List<window_active> today_windows = db.window_active.Where(wa =>
                (wa.to == null || wa.to >= day_in_question) &&
                wa.from < day_in_question_after
                ).ToList();

                Dictionary<string, SolidColorBrush> mapper2 = createColorMapper(today_windows.Select(tw => tw.name).Distinct().ToList());

                List<Event> Events2 = today_windows.Select(tw =>
                     new Event
                     {
                         Start = tw.from >= day_in_question ? tw.from.Subtract(day_in_question) : new TimeSpan(0),
                         Duration = (tw.to == null || tw.to >= day_in_question_after ? (day_in_question == DateTime.Today ? DateTime.Now : day_in_question_after) : (DateTime)tw.to).Subtract(tw.from >= day_in_question ? tw.from : day_in_question),
                         BColor = mapper2[tw.name],
                         Title = tw.name
                     }
                ).ToList();

                // Black line for full hours
                for (int i = 1; i <= 24; i++)
                {
                    Events2.Add(
                        new Event
                        {
                            Start = new TimeSpan(i, 0, 0).Add(new TimeSpan(0, 0, 30)),
                            Duration = new TimeSpan(0, 1, 0),
                            BColor = new SolidColorBrush(Colors.Black),
                            Title = ""
                        }
                    );
                }

                if (TimeLines2 == null)
                    TimeLines2 = new ObservableCollection<Timeline>();
                else
                    TimeLines2.Clear();

                TimeLines2.Add(new Timeline
                {
                    Events = Events2,
                    Duration = new TimeSpan(24, 0, 0) // 1 Day
                });
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData();
        }
    }
}
