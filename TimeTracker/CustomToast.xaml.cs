using System;
using System.Collections.Generic;
using System.Linq;
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
        int id;
        bool cancelClose = false;

        long timeout = 5000;

        class CustomComboBoxItem
        {
            public string Name { get; set; }
            public bool Selectable { get; set; }
        }


        public CustomToast(string id_string, string window)
        {
            InitializeComponent();

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 15;
            this.Top = desktopWorkingArea.Bottom - this.Height - 12;

            using (mainEntities db = new mainEntities())
            {
                bool lastActivities = db.settings.Find("lastActivities") != null ? db.settings.Find("lastActivities").value == 1 : Constants.lastActivities;

                id = int.Parse(id_string);
                activity_active new_activity = db.activity_active.Find(id);

                if (lastActivities)
                {
                    Activities = db.Database.SqlQuery<string>("SELECT name FROM activity_active GROUP BY name ORDER BY max([from]) DESC LIMIT 5").Select(a => new CustomComboBoxItem()
                    {
                        Name = a,
                        Selectable = true
                    }).ToList();
                }
                else
                {
                    Activities = db.activities.Select(a => a.name).Select(a => new CustomComboBoxItem()
                    {
                        Name = a,
                        Selectable = true
                    }).ToList();
                    if (!(Activities.Where(a => a.Name.Equals(new_activity.name)).Count() > 0)) // If a custom activity was entered add this as an option
                        Activities.Add(new CustomComboBoxItem()
                        {
                            Name = new_activity.name,
                            Selectable = true
                        });
                }

                Activities.Insert(0, new CustomComboBoxItem()
                {
                    Name = "Activity - Subactivity",
                    Selectable = false
                });

                ComboBox.ItemsSource = Activities;

                ComboBox.SelectedItem = Activities.Where(a => a.Name.Equals(new_activity.name)).FirstOrDefault();

                TextBlock2.Text = window.Trim() + "?";

                timeout = db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout;
            }

            setupClose();
        }

        private async void setupClose()
        {
            await Task.Delay((int)timeout);

            if(!cancelClose)
                this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            cancelClose = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            cancelClose = false;
            setupClose();
        }

        private void setNewActivity()
        {
            using (mainEntities db = new mainEntities())
            {
                activity_active new_activity = db.activity_active.Find(id);

                new_activity.name = ComboBox.Text;

                db.SaveChanges();
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity();
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                setNewActivity();
            }
        }
    }
}
