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
        }

        public void redrawList()
        {
            using (mainEntities db = new mainEntities())
            {
                ActivityList.Items.Clear();
                foreach(activities item in db.activities.ToList())
                {
                    ActivityList.Items.Add(new { Key = item.id, Value = item.name });
                }
            }
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                List<activities> activities = db.activities.ToList();

                if(activities.Count() >= 5)
                {
                    // Todo handle error
                } else
                {
                    activities new_activity = new activities();
                    new_activity.name = NewActivity.Text;
                    db.activities.Add(new_activity);
                    db.SaveChanges();

                    NewActivity.Clear();
                    redrawList();
                }
            }
        }

        private void RemoveActivity_Click(object sender, RoutedEventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                foreach (dynamic item in ActivityList.SelectedItems)
                {
                    activities activity = db.activities.Find(item.Key);
                    db.activities.Remove(activity);
                }

                db.SaveChanges();
                redrawList();
            }
        }

            private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
