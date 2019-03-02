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
    /// Interaktionslogik für AwayFromPCDialog.xaml
    /// </summary>
    public partial class AwayFromPCDialog : System.Windows.Window
    {
        /* Variables */
        private List<CustomComboBoxItem> Activities;
        private DateTime FromDate;
        private DateTime ToDate;
        private string DefaultName;
        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;

        /// <summary>
        /// Inializes the AwayFromPCDialog.
        /// </summary>
        /// <param name="storageHandler">Handles reading and writing from/to the csv files</param>
        /// <param name="appStateTracker">Tracks the state of the app</param>
        /// <param name="lastLocked">The time when the machine was locked</param>
        public AwayFromPCDialog(StorageHandler storageHandler, AppStateTracker appStateTracker, DateTime lastLocked)
        {
            InitializeComponent();

            FromDate = lastLocked;
            ToDate = DateTime.Now;

            StorageHandler = storageHandler;
            AppStateTracker = appStateTracker;

            Label.Content = "What were you doing since " + FromDate.ToShortTimeString() + "?";
            TimeElapsed.Content = (ToDate - FromDate).ToString().Substring(0, 8);

            // Load the last acitvities so that they can be displayed in the dropdown menu
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

            // Make only the first 5 options visible. The other should still be loaded so that autocomplete works
            for (int i = 0; i < Activities.Count(); i++)
            {
                Activities[i].Visible = i < 5 ? "Visible" : "Collapsed";
            }

            // Inserst a unselectable template at the top of the list.
            // This should serve as an example on how to input activities.
            Activities.Insert(0, new CustomComboBoxItem()
            {
                Name = "Activity - Subactivity",
                Selectable = false
            });

            ComboBox.ItemsSource = Activities;
            ComboBox.SelectedItem = Activities.Where(a => a.Name.Equals(DefaultName)).FirstOrDefault();
        }

        /// <summary>
        /// Sets the activity for the time while the users was away.
        /// Closes the dialog afterwards.
        /// </summary>
        /// <param name="name">The name of the activity</param>
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
