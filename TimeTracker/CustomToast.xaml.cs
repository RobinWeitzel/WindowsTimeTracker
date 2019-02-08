using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für CustomToast.xaml
    /// </summary>
    public partial class CustomToast : Window
    {
        List<CustomComboBoxItem> Activities;
        Stack<bool> cancelClose = new Stack<bool>();
        DateTime toDate;
        string defaultName;

        long timeout = 5000;

        class CustomComboBoxItem
        {
            public string Name { get; set; }
            public bool Selectable { get; set; }
            public string Visible { get; set; }
        }

        public static class ScreenHandler
        {
            public static Screen GetCurrentScreen(Window window)
            {
                var parentArea = new System.Drawing.Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
                return Screen.FromRectangle(parentArea);
            }

            public static Screen GetScreen(int requestedScreen)
            {
                var screens = Screen.AllScreens;
                var mainScreen = 0;
                if (screens.Length > 1 && mainScreen < screens.Length)
                {
                    return screens[requestedScreen];
                }
                return screens[mainScreen];
            }
        }

        public CustomToast(bool focusToast)
        {
            InitializeComponent();
            var currentScreen = ScreenHandler.GetCurrentScreen(this); // ToDo: fix this
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 15;
            this.Top = desktopWorkingArea.Bottom - this.Height - 12;

            toDate = DateTime.Now;

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

                TextBlock2.Text = Variables.currentWindow != null ? Variables.currentWindow.Name.Trim() : "No window active";

                timeout = Settings.Default.Timeout * 1000; // Convert to ms

                if(Settings.Default.PlayNotificationSound)
                    SystemSounds.Hand.Play();
            }

            if (focusToast)
                ComboBox.Focus();

            setupClose();
        }

        private async void setupClose()
        {
            await Task.Delay((int)timeout);

            if(cancelClose.Count() == 0 || cancelClose.Pop() != true)
            {
                setNewActivity(ComboBox.Text);
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            cancelClose.Push(true);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            setupClose();
        }

        private void setNewActivity(string name, bool confirmClicked = false)
        {
            if (Variables.currentActivity == null || !ComboBox.Text.Equals(Variables.currentActivity.Name))
            {
                if (Variables.currentActivity != null)
                    Variables.currentActivity.save(toDate);

                Variables.currentActivity = new Helper.Activity();

                Variables.currentActivity.Name = name;
                Variables.currentActivity.From = toDate;

                if(confirmClicked)
                    Variables.lastConfirmed = DateTime.Now;
            }
            if(this.IsLoaded) // Can only close if the window still exists
                this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity(defaultName);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity(ComboBox.Text, true);
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                setNewActivity(ComboBox.Text, true);
            } else if(e.Key == Key.Escape)
            {
                setNewActivity(defaultName);
            }
        }
    }
}
