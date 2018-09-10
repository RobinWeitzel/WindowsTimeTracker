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
using System.Windows.Interop;
using LiveCharts;
using LiveCharts.Wpf;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection {
                new StackedColumnSeries {
                    Values = new ChartValues<double> { 3, 5, 7, 4 },
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries {
                    Values = new ChartValues<decimal> { 5, 6, 2, 7 },
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };

            SeriesCollection.Add(new StackedColumnSeries
            {
                Values = new ChartValues<double> { 2, 4, 9, 4 },
                StackMode = StackMode.Values,
                DataLabels = true
            });

            Labels = new[] { "Chrome", "Firefox", "IE" };
            Formatter = value => value + "Mill";

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set;}
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }

  
}