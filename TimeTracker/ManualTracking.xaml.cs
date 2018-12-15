using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für ManualTracking.xaml
    /// </summary>
    public partial class ManualTracking : Window
    {
        List<CustomComboBoxItem> Activities;
        DateTime fromDate;
        DateTime toDate;
        string defaultName;

        class CustomComboBoxItem
        {
            public string Name { get; set; }
            public bool Selectable { get; set; }
            public string Visible { get; set; }
        }

        public ManualTracking()
        {
            InitializeComponent();

            toDate = DateTime.Now;

            using (TextReader tr = new StreamReader(Variables.activityPath))
            {
                var csv = new CsvReader(tr);
                var records = csv.GetRecords<Helper.Activity>();

                Helper.Activity latest = records.OrderByDescending(r => r.To).FirstOrDefault();

                if (latest != null)
                    fromDate = (DateTime)latest.To;
                else
                    fromDate = DateTime.Now;
            }

            Label.Content = "What were you doing since " + fromDate.ToShortTimeString() + "?";
            TimeElapsed.Content = (toDate - fromDate).ToString().Substring(0, 8);

            using (TextReader tr = new StreamReader(Variables.activityPath))
            {
                var csv = new CsvReader(tr);
                var records = csv.GetRecords<Helper.Activity>();

                Activities = records.GroupBy(r => r.Name).OrderByDescending(rg => rg.Max(r => r.From)).Select(rg => new CustomComboBoxItem()
                {
                    Name = rg.Key,
                    Selectable = true
                }).ToList();

                defaultName = Variables.currentActivity != null ? Variables.currentActivity.Name : (Activities.Count() > 0 ? Activities.First().Name : "");

                if (Variables.currentActivity != null && !Activities.Any(a => a.Name.Equals(defaultName)))
                    Activities.Insert(0, new CustomComboBoxItem()
                    {
                        Name = defaultName,
                        Selectable = true
                    });

                for (int i = 0; i < Activities.Count(); i++)
                {
                    Activities[i].Visible = i < 5 ? "Visible" : "Collapsed"; // Make only the first 5 options visible
                }

                Activities.Insert(0, new CustomComboBoxItem()
                {
                    Name = "Activity - Subactivity",
                    Selectable = false
                });

                ComboBox.ItemsSource = Activities;

                ComboBox.SelectedItem = Activities.Where(a => a.Name.Equals(defaultName)).FirstOrDefault();

                if (Settings.Default.PlayNotificationSound)
                    SystemSounds.Hand.Play();
            }
        }

        private void setNewActivity(string name)
        {
            Helper.Activity activity = new Helper.Activity();
            activity.Name = name;
            activity.From = fromDate;
            activity.To = toDate;

            activity.save();

            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity(ComboBox.Text);
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                setNewActivity(ComboBox.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
