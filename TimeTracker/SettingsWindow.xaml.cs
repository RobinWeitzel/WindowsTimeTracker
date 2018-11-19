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
                // Redraw inputs
                TimeOut.Text = ((db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout) / 1000).ToString();
                TimeNotUsed.Text = ((db.settings.Find("timeNotUsed") != null ? db.settings.Find("timeNotUsed").value : Constants.defaultTimeNotUsed) / (1000 * 60)).ToString();
                TimeOut2.Text = (db.settings.Find("timeout2") != null ? db.settings.Find("timeout2").value : Constants.defaultTimeout2).ToString();
                FuzzyMatching.IsChecked = db.settings.Find("fuzzyMatching") != null ? db.settings.Find("fuzzyMatching").value == 1 : Constants.fuzzyMatching;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
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

                /* Save TimeOut2 */
                settings timeOut2 = db.settings.Find("timeOut2");

                if (timeOut2 == null)
                {
                    timeOut2 = new settings();
                    timeOut2.key = "timeOut2";
                    db.settings.Add(timeOut2);
                }

                int timeout2Result;
                if (int.TryParse(TimeOut2.Text, out timeout2Result))
                {
                    timeOut2.value = timeout2Result;
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

                /* Save MakeSound */
                settings makeSound = db.settings.Find("makeSound");

                if (makeSound == null)
                {
                    makeSound = new settings();
                    makeSound.key = "makeSound";
                    db.settings.Add(makeSound);
                }

                makeSound.value = MakeSound.IsChecked == true ? 1 : 0;

                db.SaveChanges();
            }
        }
    }
}
