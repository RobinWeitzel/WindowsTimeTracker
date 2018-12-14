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

            ContentArea.Content = new SettingsGeneral();
            ListBox.SelectedIndex = 0;
        }

        private void Navigate(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox.SelectedItem == null) // Do not allow deselecting items
            {
                ListBox.SelectedItem = e.RemovedItems[0];
                return;
            }

            switch((ListBox.SelectedItem as ListBoxItem).Content.ToString())
            {
                case "General":
                    ContentArea.Content = new SettingsGeneral();
                    break;
                case "Blacklist Apps":
                    ContentArea.Content = new SettingsBlacklist();
                    break;
                case "About":
                    ContentArea.Content = new SettingsAbout();
                    break;
            }
        }
    }
}
