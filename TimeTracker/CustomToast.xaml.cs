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

using TimeTracker.Helper;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für CustomToast.xaml
    /// </summary>
    public partial class CustomToast : System.Windows.Window
    {
        private List<CustomComboBoxItem> Activities;
        private Stack<bool> CancelClose = new Stack<bool>();
        private DateTime ToDate;
        private string DefaultName;
        private long Timeout;

        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;

        public CustomToast(StorageHandler storageHandler, AppStateTracker appStateTracker, bool focusToast)
        {
            InitializeComponent();

            StorageHandler = storageHandler;
            AppStateTracker = appStateTracker;
            ToDate = DateTime.Now;

            // Move toast to the bottom right
            Rect DesktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = DesktopWorkingArea.Right - this.Width - 15;
            this.Top = DesktopWorkingArea.Bottom - this.Height - 12;

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
            TextBlock2.Text = AppStateTracker.CurrentWindow?.Name.Trim() ?? "No window active";

            Timeout = Settings.Default.Timeout * 1000; // Convert to ms

            if (Settings.Default.PlayNotificationSound)
                SystemSounds.Hand.Play();

            if (focusToast)
                ComboBox.Focus();

            SetupClose();
        }

        private async void SetupClose()
        {
            await Task.Delay((int)Timeout);

            if (CancelClose.Count() == 0 || CancelClose.Pop() != true)
            {
                SetNewActivity(ComboBox.Text);
            }
        }

        private void SetNewActivity(string name = null, bool confirmClicked = false)
        {
            if (AppStateTracker.CurrentActivity == null || !ComboBox.Text.Equals(AppStateTracker.CurrentActivity.Name))
            {
                AppStateTracker.SaveCurrentActivity();
                AppStateTracker.CreateCurrentActivity(name ?? DefaultName, ToDate);

                if (confirmClicked)
                    AppStateTracker.LastConfirmed = DateTime.Now;
            }
            if (this.IsLoaded) // Can only close if the window still exists
                this.Close();
        }

        /* Window events */

        private void Window_Activated(object sender, EventArgs e)
        {
            CancelClose.Push(true);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            SetupClose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetNewActivity();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SetNewActivity();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            SetNewActivity(ComboBox.Text, true);
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetNewActivity(ComboBox.Text, true);
            }
            else if (e.Key == Key.Escape)
            {
                SetNewActivity();
            }
        }
    }
}
