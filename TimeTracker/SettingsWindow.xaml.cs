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
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void redrawSettings()
        {
            using (mainEntities db = new mainEntities())
            {
                // Redraw list
                ActivityList.Items.Clear();
                foreach(activities item in db.activities.ToList())
                {
                    ActivityList.Items.Add(new { Key = item.id, Value = item.name });
                }

                // Redraw inputs
                TimeOut.Text = ((db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout) / 1000).ToString();
                TimeNotUsed.Text = ((db.settings.Find("timeNotUsed") != null ? db.settings.Find("timeNotUsed").value : Constants.defaultTimeNotUsed) / (1000 * 60)).ToString();
                TimeRecordsKept.Text = (db.settings.Find("timeRecordsKept") != null ? db.settings.Find("timeRecordsKept").value : Constants.defaultTimeRecordsKept).ToString();
                FuzzyMatching.IsChecked = db.settings.Find("fuzzyMatching") != null ? db.settings.Find("fuzzyMatching").value == 1 : Constants.fuzzyMatching;
            }
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {

            if(ActivityList.Items.Count >= 4)
            {
                // Todo handle error
            } else
            {
                // Key is irrelevant as it is auto generated when the new item is saved in the db
                ActivityList.Items.Add(new { Key = -1, Value = NewActivity.Text });
                NewActivity.Clear();
            }  
        }

        private void RemoveActivity_Click(object sender, RoutedEventArgs e)
        {
            List<object> helper = new List<object>();
            foreach (object item in ActivityList.SelectedItems)
            {
                helper.Add(item);
            }

            foreach (object item in helper)
            {
                ActivityList.Items.Remove(item);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                /* Save activities list */
                List<long> existingKeys = new List<long>();

                // Add new activities
                foreach(dynamic item in ActivityList.Items)
                {
                    existingKeys.Add(item.Key);
                    if (db.activities.Find(item.Key) == null) // activity does not exist in db
                    {
                        activities activitiy = new activities();
                        activitiy.name = item.Value;
                        db.activities.Add(activitiy);
                    }
                }

                // Delete removed activities
                db.activities.RemoveRange(db.activities.Where(a => !existingKeys.Contains(a.id)));

                /* Save Timeout */
                settings timeout = db.settings.Find("timeout");

                if(timeout == null)
                {
                    timeout = new settings();
                    timeout.key = "timeout";
                    db.settings.Add(timeout);
                }

                int timeoutResult;
                if(int.TryParse(TimeOut.Text, out timeoutResult))
                {
                    timeout.value = timeoutResult * 1000; // Convert to ms 
                }

                /* Save TimeNotUsed */
                settings timeNotUsed = db.settings.Find("timeNotUsed");

                if (timeNotUsed == null)
                {
                    timeNotUsed = new settings();
                    timeNotUsed.key = "timeNotUsed";
                    db.settings.Add(timeNotUsed);
                }

                int timeNotUsedResult;
                if (int.TryParse(TimeNotUsed.Text, out timeNotUsedResult))
                {
                    timeNotUsed.value = timeNotUsedResult * 60 * 1000; // Convert to ms 
                }

                /* Save TimeRecordsKept */
                settings timeRecordsKept = db.settings.Find("timeRecordsKept");

                if (timeRecordsKept == null)
                {
                    timeRecordsKept = new settings();
                    timeRecordsKept.key = "timeRecordsKept";
                    db.settings.Add(timeRecordsKept);
                }

                int timeRecordsKeptResult;
                if (int.TryParse(TimeRecordsKept.Text, out timeRecordsKeptResult))
                {
                    timeRecordsKept.value = timeRecordsKeptResult; // Keep as days
                }

                /* Save FuzzyMatching */
                settings fuzzyMatching = db.settings.Find("fuzzyMatching");

                if (fuzzyMatching == null)
                {
                    fuzzyMatching = new settings();
                    fuzzyMatching.key = "fuzzyMatching";
                    db.settings.Add(fuzzyMatching);
                }

                fuzzyMatching.value = FuzzyMatching.IsChecked == true ? 1 : 0;

                db.SaveChanges();

                Hide();
            }
        }
    }
}
