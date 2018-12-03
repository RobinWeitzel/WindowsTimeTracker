using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für ManualEdit.xaml
    /// </summary>
    public partial class ManualEdit : Window
    {
        List<Helper.Activity> Activities;
        public ManualEdit()
        {
            InitializeComponent();
            loadData();

        }

        private void loadData()
        {
            using (TextReader tr = new StreamReader(Variables.activityPath))
            {
                var csv = new CsvReader(tr);
                var records = csv.GetRecords<Helper.Activity>();

                Activities = records.OrderByDescending(aa => aa.To).ToList();
                DataGrid.ItemsSource = Activities;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (TextWriter tw = new StreamWriter(Variables.activityPath))
            {
                var csv = new CsvWriter(tw);
                csv.WriteRecords(Activities);
            }
            loadData();
        }
    }
}
