using System;
using System.Collections.Generic;
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

        public CustomToast(string window)
        {
            InitializeComponent();
            var currentScreen = ScreenHandler.GetCurrentScreen(this); // ToDo: fix this
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 15;
            this.Top = desktopWorkingArea.Bottom - this.Height - 12;

            toDate = DateTime.Now;

            using (mainEntities db = new mainEntities())
            {
                bool lastActivities = db.settings.Find("lastActivities") != null ? db.settings.Find("lastActivities").value == 1 : Constants.lastActivities;
                bool makeSound = db.settings.Find("makeSound") != null ? db.settings.Find("makeSound").value == 1 : Constants.makeSound;
            
                Activities = db.Database.SqlQuery<string>("SELECT name FROM activity_active GROUP BY name ORDER BY max([from]) DESC").Select(a => new CustomComboBoxItem()
                {
                    Name = a,
                    Selectable = true
                }).ToList();

                for(int i = 0; i < Activities.Count(); i++)
                {
                    Activities[i].Visible = i < 5 ? "Visible" : "Collapsed"; // Make only the first 5 options visible
                }

                Activities.Insert(0, new CustomComboBoxItem()
                {
                    Name = "Activity - Subactivity",
                    Selectable = false
                });

                ComboBox.ItemsSource = Activities;

                activity_active last_activity = db.activity_active.Where(aa => aa.to == null).FirstOrDefault();
                if (last_activity == null)
                    last_activity = db.activity_active.OrderByDescending(aa => aa.to).FirstOrDefault();
                defaultName = last_activity != null ? last_activity.name : "";
                ComboBox.SelectedItem = Activities.Where(a => a.Name.Equals(defaultName)).FirstOrDefault();

                TextBlock2.Text = window.Trim();

                timeout = db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout;

                if(makeSound)
                    SystemSounds.Hand.Play();
            }

            setupClose();
        }

        private async void setupClose()
        {
            await Task.Delay((int)timeout);

            if(cancelClose.Count() == 0 || cancelClose.Pop() != true)
            {
                setNewActivity(defaultName);
                this.Close();
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

        private void setNewActivity(string name)
        {
            using (mainEntities db = new mainEntities())
            {
                activity_active current_activity = db.activity_active.Where(aa => aa.to == null).FirstOrDefault();
                if (current_activity == null || !ComboBox.Text.Equals(current_activity.name))
                {
                    activity_active new_activity = new activity_active();

                    if (current_activity != null)
                        current_activity.to = toDate;

                    new_activity.from = toDate;
                    new_activity.name = name;

                    db.activity_active.Add(new_activity);

                    db.SaveChanges();
                }

                Constants.lastConfirmed = DateTime.Now;
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity(defaultName);
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity(ComboBox.Text);
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                setNewActivity(ComboBox.Text);
            }
        }
    }
}
