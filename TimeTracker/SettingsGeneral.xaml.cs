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
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für SettingsGeneral.xaml
    /// </summary>
    public partial class SettingsGeneral : UserControl
    {
        public SettingsGeneral()
        {
            InitializeComponent();

            TimeOut.Text = Settings.Default.Timeout.ToString();
            TimeNotUsed.Text = Settings.Default.TimeSinceAppLastUsed.ToString();
            TimeOut2.Text = Settings.Default.Timeout2.ToString();
            MakeSound.IsChecked = Settings.Default.PlayNotificationSound;
        }

        private void TimeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            int timeoutResult;
            if (int.TryParse(TimeOut.Text, out timeoutResult))
            {
                Settings.Default.Timeout = timeoutResult;
            }
        }

        private void TimeNotUsed_TextChanged(object sender, TextChangedEventArgs e)
        {
            int timeNotUsedResult;
            if (int.TryParse(TimeNotUsed.Text, out timeNotUsedResult))
            {
                Settings.Default.TimeSinceAppLastUsed = timeNotUsedResult;
            }
        }

        private void TimeOut2_TextChanged(object sender, TextChangedEventArgs e)
        {

            int timeout2Result;
            if (int.TryParse(TimeOut2.Text, out timeout2Result))
            {
                Settings.Default.Timeout2 = timeout2Result;
            }
        }

        private void MakeSound_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.PlayNotificationSound = true;
        }

        private void MakeSound_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.PlayNotificationSound = false;
        }
    }
}
