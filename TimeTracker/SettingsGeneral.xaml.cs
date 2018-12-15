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
        private HashSet<Key> hotkeys = new HashSet<Key>();

        public SettingsGeneral()
        {
            InitializeComponent();

            TimeOut.Text = Settings.Default.Timeout.ToString();
            TimeNotUsed.Text = Settings.Default.TimeSinceAppLastUsed.ToString();
            TimeOut2.Text = Settings.Default.Timeout2.ToString();
            MakeSound.IsChecked = Settings.Default.PlayNotificationSound;
            OfflineTracking.IsChecked = Settings.Default.OfflineTracking;
            HotkeyDisabled.IsChecked = Settings.Default.HotkeyDisabled;

            Hotkey.IsEnabled = !Settings.Default.HotkeyDisabled;
            Hotkey.Text = Settings.Default.Hotkeys != null ? String.Join(" + ", Settings.Default.Hotkeys) : "";
        }

        private void TimeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            int timeoutResult;
            if (int.TryParse(TimeOut.Text, out timeoutResult))
            {
                Settings.Default.Timeout = timeoutResult;
                Settings.Default.Save();
            }
        }

        private void TimeNotUsed_TextChanged(object sender, TextChangedEventArgs e)
        {
            int timeNotUsedResult;
            if (int.TryParse(TimeNotUsed.Text, out timeNotUsedResult))
            {
                Settings.Default.TimeSinceAppLastUsed = timeNotUsedResult;
                Settings.Default.Save();
            }
        }

        private void TimeOut2_TextChanged(object sender, TextChangedEventArgs e)
        {

            int timeout2Result;
            if (int.TryParse(TimeOut2.Text, out timeout2Result))
            {
                Settings.Default.Timeout2 = timeout2Result;
                Settings.Default.Save();
            }
        }

        private void MakeSound_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.PlayNotificationSound = true;
            Settings.Default.Save();
        }

        private void MakeSound_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.PlayNotificationSound = false;
            Settings.Default.Save();
        }

        private void OfflineTracking_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.OfflineTracking = true;
            Settings.Default.Save();
        }

        private void OfflineTracking_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.OfflineTracking = false;
            Settings.Default.Save();
        }

        private void Hotkey_KeyDown(object sender, KeyEventArgs e)
        {
            hotkeys.Add(e.Key);

            Hotkey.Text = String.Join(" + ", hotkeys);
            Hotkey.CaretIndex = Hotkey.Text.Length;
            e.Handled = true;
        }

        private void Hotkey_KeyUp(object sender, KeyEventArgs e)
        {
            if (hotkeys.Count() > 0)
            {
                Settings.Default.Hotkeys = hotkeys.ToList();
                hotkeys.Clear();

                Settings.Default.Save();
            }

            e.Handled = true;
        }

        private void Hotkey_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (hotkeys.Count() > 0)
            {
                Hotkey.Text = String.Join(" + ", hotkeys);
                Hotkey.CaretIndex = Hotkey.Text.Length;
            }
        }

        private void HotkeyDisabled_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.HotkeyDisabled = true;
            Hotkey.IsEnabled = false;
            Settings.Default.Save();
        }

        private void HotkeyDisabled_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Default.HotkeyDisabled = false;
            Hotkey.IsEnabled = true;
            Settings.Default.Save();
        }

    }
}
