using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaktionslogik für Overview.xaml
    /// </summary>
    public partial class Overview : UserControl
    {
        public SeriesCollection WindowSeries { get; set; }
        public SeriesCollection ActivitySeries { get; set; }
        public SeriesCollection ActivityGroupSeries { get; set; }
        public Func<ChartPoint, string> labelPoint { get; set; }

        public Overview()
        {
            InitializeComponent();

            WindowSeries = new SeriesCollection();
            ActivityGroupSeries = new SeriesCollection();
            ActivitySeries = new SeriesCollection();

            labelPoint = chartPoint => {
                int hours = (int)Math.Floor(chartPoint.Y / 60);
                int minutes = (int)Math.Round(chartPoint.Y % 60);

                if (hours > 0)
                    return hours.ToString() + "h " + minutes.ToString() + "m";
                else
                    return minutes.ToString() + "m";
            };

            loadData(Time_Picker.Text);

            DataContext = this;
        }

        public class Helper
        {
            public string name { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
            public double time { get; set; }
        }

        public void loadData(string newItem)
        {
            if (ActivityGroupSeries == null ||WindowSeries == null || ActivitySeries == null)
                return;

            if (newItem == null)
                newItem = ((dynamic)Time_Picker.SelectedItem).Content;

            using (mainEntities db = new mainEntities())
            {
                List<PieSeries> WindowSeriesItems = new List<PieSeries>();
                List<PieSeries> ActivityGroupSeriesItems = new List<PieSeries>();
                List<PieSeries> ActivitySeriesItems = new List<PieSeries>();
                List<Helper> windowHelper = new List<Helper>();
                List<Helper> activityHelper = new List<Helper>();
                switch (newItem)
                {
                    case "Today":
                        windowHelper = db.window_active
                            .Where(wa => wa.to >= DateTime.Today || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > DateTime.Today ? wa.from : DateTime.Today,
                                to = wa.to ?? DateTime.Now
                            }).ToList();
                        activityHelper = db.activity_active
                            .Where(wa => wa.to >= DateTime.Today || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > DateTime.Today ? wa.from : DateTime.Today,
                                to = wa.to ?? DateTime.Now
                            }).ToList();
                        break;
                    case "Yesterday":
                        DateTime yesterday = DateTime.Today.AddDays(-1);
                        windowHelper = db.window_active
                            .Where(wa => (wa.from < DateTime.Today && wa.to >= yesterday) || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > yesterday ? wa.from : yesterday,
                                to = (wa.to ?? DateTime.Today) < DateTime.Today ? (wa.to ?? DateTime.Today) : DateTime.Today
                            }).ToList();
                        activityHelper = db.activity_active
                            .Where(wa => (wa.from < DateTime.Today && wa.to >= yesterday) || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > yesterday ? wa.from : yesterday,
                                to = (wa.to ?? DateTime.Today) < DateTime.Today ? (wa.to ?? DateTime.Today) : DateTime.Today
                            }).ToList();
                        break;
                    case "Last 3 days":
                        DateTime three_days = DateTime.Now.AddDays(-3);
                        windowHelper = db.window_active
                            .Where(wa => wa.to >= three_days || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > three_days ? wa.from : three_days,
                                to = wa.to ?? DateTime.Now
                            }).ToList();
                        activityHelper = db.activity_active
                            .Where(wa => wa.to >= three_days || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > three_days ? wa.from : three_days,
                                to = wa.to ?? DateTime.Now
                            }).ToList();
                        break;
                    case "Last week":
                        DateTime mondayOfLastWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
                        DateTime mondayOfThisWeek = mondayOfLastWeek.AddDays(7);
                        windowHelper = db.window_active
                            .Where(wa => (wa.from < mondayOfThisWeek && wa.to >= mondayOfLastWeek) || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > mondayOfLastWeek ? wa.from : mondayOfLastWeek,
                                to = (wa.to ?? DateTime.Today) < mondayOfThisWeek ? (wa.to ?? DateTime.Today) : mondayOfThisWeek
                            }).ToList();
                        activityHelper = db.activity_active
                           .Where(wa => (wa.from < mondayOfThisWeek && wa.to >= mondayOfLastWeek) || wa.to == null)
                           .Select(wa => new Helper
                           {
                               name = wa.name,
                               from = wa.from > mondayOfLastWeek ? wa.from : mondayOfLastWeek,
                               to = (wa.to ?? DateTime.Today) < mondayOfThisWeek ? (wa.to ?? DateTime.Today) : mondayOfThisWeek
                           }).ToList();
                        break;
                    case "This month":
                        DateTime start_this_month = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        windowHelper = db.window_active
                            .Where(wa => wa.to >= start_this_month || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > start_this_month ? wa.from : start_this_month,
                                to = wa.to ?? DateTime.Now
                            }).ToList();
                        activityHelper = db.activity_active
                            .Where(wa => wa.to >= start_this_month || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > start_this_month ? wa.from : start_this_month,
                                to = wa.to ?? DateTime.Now
                            }).ToList();
                        break;
                    case "Last month":
                        DateTime start_last_month = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).AddMonths(-1);
                        DateTime start_next_month = start_last_month.AddMonths(1);
                        windowHelper = db.window_active
                            .Where(wa => (wa.from < start_last_month && wa.to >= start_last_month) || wa.to == null)
                            .Select(wa => new Helper
                            {
                                name = wa.name,
                                from = wa.from > start_last_month ? wa.from : start_last_month,
                                to = (wa.to ?? DateTime.Today) < start_last_month ? (wa.to ?? DateTime.Today) : start_last_month
                            }).ToList();
                        activityHelper = db.activity_active
                           .Where(wa => (wa.from < start_last_month && wa.to >= start_last_month) || wa.to == null)
                           .Select(wa => new Helper
                           {
                               name = wa.name,
                               from = wa.from > start_last_month ? wa.from : start_last_month,
                               to = (wa.to ?? DateTime.Today) < start_last_month ? (wa.to ?? DateTime.Today) : start_last_month
                           }).ToList();
                        break;
                }

                foreach (Helper h in windowHelper)
                {
                    h.time = Math.Max(Math.Round((h.to - h.from).TotalMinutes), 0);
                }

                WindowSeriesItems = windowHelper.GroupBy(h => h.name).Where(g => g.Sum(h => h.time) >= 5).Select(g => new PieSeries
                {
                    Title = g.Key,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(g.Sum(h => h.time)) },
                    DataLabels = true,
                    LabelPoint = labelPoint
                }).ToList();

                foreach (Helper h in activityHelper)
                {
                    h.time = Math.Max(Math.Round((h.to - h.from).TotalMinutes), 0);
                }

                List<Helper> activityHelper2 = activityHelper.OrderBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).First()).GroupBy(h => h.name).Where(g => g.Sum(h => h.time) >= 5).SelectMany(h => h).ToList();

                ActivityGroupSeriesItems = activityHelper2.GroupBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).First()).Select(g => new PieSeries
                {
                    Title = g.Key,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(g.Sum(h => h.time)) },
                    DataLabels = true,
                    LabelPoint = labelPoint
                }).ToList();

                ActivitySeriesItems = activityHelper2.GroupBy(h => h.name.Split(new string[] { " - " }, StringSplitOptions.None).Last()).Select(g => new PieSeries
                {
                    Title = g.Key,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(g.Sum(h => h.time)) },
                    DataLabels = true,
                    LabelPoint = labelPoint
                }).ToList();

                WindowSeries.Clear();

                foreach (PieSeries series in WindowSeriesItems)
                {
                    WindowSeries.Add(series);
                }

                ActivityGroupSeries.Clear();

                foreach (PieSeries series in ActivityGroupSeriesItems)
                {
                    ActivityGroupSeries.Add(series);
                }

                ActivitySeries.Clear();

                foreach (PieSeries series in ActivitySeriesItems)
                {
                    ActivitySeries.Add(series);
                }
            }
        }

        private void Time_Picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData(((dynamic)e.AddedItems[0]).Content);
        }
    }
}
