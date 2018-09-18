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
    /// Interaktionslogik für Gantt2.xaml
    /// </summary>
    public partial class Gantt2 : UserControl
    {
        public ChartValues<double> Values { get; set; }
        public StepLineSeries Series { get; set; }
        public Func<double, string> Formatter { get; set; }
        public string[] Labels { get; set; }

        public Gantt2()
        {
            InitializeComponent();

            Values = new ChartValues<double> { 1, 4, 3, 3, 1, 4, 2, 1, 2, 3, 5 };

            Formatter = value => new string[] { "test", "hi", "ho", "app 1", "app2" }[(int)value - 1];

            DataContext = this;

            Labels = new string[] { "test", "hi", "ho", "app 1", "app2", "app3"};

            Series = new StepLineSeries
            {
                Values = new ChartValues<double> { 1, 4, 3, 3, 1, 4, 2, 1, 2, 3, 5 },
                AlternativeStroke = Brushes.Transparent,
                StrokeThickness = 3,
                PointGeometry = null
            };
        }
    }
}
