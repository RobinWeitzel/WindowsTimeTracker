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
    /// Interaktionslogik für ManualEdit.xaml
    /// </summary>
    public partial class ManualEdit : Window
    {
        List<activity_active> Activities;
        List<activity_active> Activities_Duplicate;
        public ManualEdit()
        {
            InitializeComponent();
            loadData();

        }

        private void loadData()
        {
            using (mainEntities db = new mainEntities())
            {
                Activities = db.activity_active.Where(aa => aa.to != null).OrderByDescending(aa => aa.to).Take(20).ToList();
                Activities_Duplicate = db.activity_active.Where(aa => aa.to != null).OrderByDescending(aa => aa.to).Take(20).ToList();
                DataGrid.ItemsSource = Activities;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using (mainEntities db = new mainEntities())
            {
                foreach(activity_active activity_duplicate in Activities_Duplicate)
                {
                    activity_active activity = Activities.Where(aa => aa.id == activity_duplicate.id).FirstOrDefault();
                    activity_active activity_db = db.activity_active.Find(activity_duplicate.id);
                    if (activity == null)
                    {
                        if(activity_db != null)
                            db.activity_active.Remove(db.activity_active.Find(activity_duplicate.id));
                    }
                    else
                    {
                        if(activity_db != null)
                        {
                            activity_db.name = activity.name;
                            activity_db.from = activity.from;
                            activity_db.to = activity.to;
                        }
                    }

                    db.SaveChanges();
                }

                loadData();
            }
        }
    }
}
