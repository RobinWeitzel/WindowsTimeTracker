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
    /// Interaktionslogik für ActivityDialog.xaml
    /// </summary>
    public partial class ActivityDialog : System.Windows.Window
    {
        /* Variables */
        private List<CustomComboBoxItem> Activities;
        private Stack<bool> CancelClose = new Stack<bool>();
        private DateTime ToDate;
        private string DefaultName;
        private long Timeout;
        private StorageHandler StorageHandler;
        private AppStateTracker AppStateTracker;

        /// <summary>
        /// The activity dialog shown in the bottom right corner asking the users if he is still working on the same acitivity.
        /// </summary>
        /// <param name="storageHandler">Handles reading and writing from/to the csv files</param>
        /// <param name="appStateTracker">Tracks the state of the app</param>
        /// <param name="focusToast">True, if the dialog should be focused, otherwise False</param>
        public ActivityDialog(StorageHandler storageHandler, AppStateTracker appStateTracker, bool focusToast)
        {
            InitializeComponent();

            StorageHandler = storageHandler;
            AppStateTracker = appStateTracker;
            ToDate = DateTime.Now;

            // Move toast to the bottom right
            Rect DesktopWorkingArea = SystemParameters.WorkArea;
            Left = DesktopWorkingArea.Right - Width - 15;
            Top = DesktopWorkingArea.Bottom - Height - 12;

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
            TextBlock2.Text = AppStateTracker.CurrentWindow?.Name.Trim() ?? "No window active";

            Timeout = Settings.Default.TimeNotificationVisible * 1000; // Convert to ms

            if (Settings.Default.PlayNotificationSound)
                SystemSounds.Hand.Play();

            if (focusToast)
                ComboBox.Focus();

            SetupClose();
        }

        /// <summary>
        /// Makes sure the dialog is closed after the appropriate time, if it has not been selected by the user.
        /// As long as it is selected, the dialog will never close (so the user can take as much time as he needs to input th activity).
        /// </summary>
        private async void SetupClose()
        {
            await Task.Delay((int)Timeout);

            // Checks if the dialog should truely be closed.
            if (CancelClose.Count() == 0 || CancelClose.Pop() != true) 
            {
                SetNewActivity(ComboBox.Text);
            }
        }

        /// <summary>
        ///  Changes the current activity and saves the old one.
        ///  Closes the dialog afterwards.
        /// </summary>
        /// <param name="name">The name if the new activity. Leave it empty if the name of the last activity should be used.</param>
        /// <param name="confirmClicked">True, if the confirm button was clicked.</param>
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
