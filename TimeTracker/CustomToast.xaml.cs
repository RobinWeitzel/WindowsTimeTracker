using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        List<string> Activities;
        int id;
        public CustomToast(string id_string, string window)
        {
            InitializeComponent();

            using (mainEntities db = new mainEntities())
            {
                bool lastActivities = db.settings.Find("lastActivities") != null ? db.settings.Find("lastActivities").value == 1 : Constants.lastActivities;

                id = int.Parse(id_string);
                activity_active new_activity = db.activity_active.Find(id);

                if (lastActivities)
                {
                    Activities = db.Database.SqlQuery<string>("SELECT name FROM activity_active GROUP BY name ORDER BY max([from]) DESC LIMIT 5").ToList();
                }
                else
                {
                    Activities = db.activities.Select(a => a.name).ToList();
                    if (!Activities.Contains(new_activity.name)) // If a custom activity was entered add this as an option
                        Activities.Add(new_activity.name);
                }

                ComboBox.ItemsSource = Activities;
                ComboBox.SelectedItem = new_activity.name;

                TextBlock.Text = window + " is being used for: ";
            }
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

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                setNewActivity();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using (mainEntities db = new mainEntities())
                {
                    setNewActivity();
                }
            }
        }
    }
}
