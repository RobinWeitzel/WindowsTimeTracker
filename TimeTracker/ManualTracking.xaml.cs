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
using TimeTracker.Helper;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für ManualTracking.xaml
    /// </summary>
    public partial class ManualTracking : System.Windows.Window
    {
        private List<CustomComboBoxItem> Activities;
        private DateTime FromDate;
        private DateTime ToDate;
        private string DefaultName;

        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;

        public ManualTracking(StorageHandler storageHandler, AppStateTracker appStateTracker, DateTime lastLocked)
        {
            InitializeComponent();

            FromDate = lastLocked;
            ToDate = DateTime.Now;

            StorageHandler = storageHandler;
            AppStateTracker = appStateTracker;

            Label.Content = "What were you doing since " + FromDate.ToShortTimeString() + "?";
            TimeElapsed.Content = (ToDate - FromDate).ToString().Substring(0, 8);

            Activities = StorageHandler.GetLastActivitiesGrouped().Select(rg => new CustomComboBoxItem()
            {
                Name = rg.Key,
                Selectable = true
            }).ToList();

            DefaultName = AppStateTracker.CurrentActivity?.Name ?? Activities.FirstOrDefault()?.Name ?? "";

            if (AppStateTracker.CurrentActivity != null && !Activities.Any(a => a.Name.Equals(DefaultName)))
                Activities.Insert(0, new CustomComboBoxItem()
                {
                    Name = DefaultName,
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
            ComboBox.SelectedItem = Activities.Where(a => a.Name.Equals(DefaultName)).FirstOrDefault();
        }

        private void SetNewActivity(string name)
        {
            
            AppStateTracker.CreateCurrentActivity(name, FromDate);
            AppStateTracker.SaveCurrentActivity(ToDate);
            Close();
        }

        /* Window events */

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            SetNewActivity(ComboBox.Text);
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetNewActivity(ComboBox.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
