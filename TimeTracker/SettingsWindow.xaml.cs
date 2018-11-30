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
using TimeTracker.Properties;

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

            Version.Content = "v" + Variables.version;
        }

        public void redrawSettings()
        {
            // Redraw inputs
            TimeOut.Text = Settings.Default.Timeout.ToString();
            TimeNotUsed.Text = Settings.Default.TimeSinceAppLastUsed.ToString();
            TimeOut2.Text = Settings.Default.Timeout2.ToString();
            MakeSound.IsChecked = Settings.Default.PlayNotificationSound;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
                int timeoutResult;
                if(int.TryParse(TimeOut.Text, out timeoutResult))
                {
                    Settings.Default.Timeout = timeoutResult * 1000; // Convert to ms 
                }

                int timeNotUsedResult;
                if (int.TryParse(TimeNotUsed.Text, out timeNotUsedResult))
                {
                    Settings.Default.TimeSinceAppLastUsed = timeNotUsedResult * 60 * 1000; // Convert to ms 
                }

                int timeout2Result;
                if (int.TryParse(TimeOut2.Text, out timeout2Result))
                {
                    Settings.Default.Timeout2 = timeout2Result;
                }

                Settings.Default.PlayNotificationSound = MakeSound.IsChecked == true;
        }
    }
}
