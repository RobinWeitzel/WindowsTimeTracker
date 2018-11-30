using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
        List<Helper.Activity> Activities;
        List<Helper.Activity> Activities_Duplicate;
        public ManualEdit()
        {
            InitializeComponent();
            loadData();

        }

        private void loadData()
        {
            using (TextReader tr = new StreamReader(Variables.activityPath))
            {
                var csv = new CsvReader(tr);
                var records = csv.GetRecords<Helper.Activity>();

                Activities = records.OrderByDescending(aa => aa.To).Take(20).ToList();
                Activities_Duplicate = records.OrderByDescending(aa => aa.To).Take(20).ToList();
                DataGrid.ItemsSource = Activities;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            /*using (TextWriter tw = new StreamWriter(Variables.activityPath))
            {
                var csv = new CsvWriter(tw);
                foreach (Helper.Activity activity_duplicate in Activities_Duplicate)
                {
                    Helper.Activity activity = Activities.Where(aa => aa.Id == activity_duplicate.id).FirstOrDefault();
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
            }*/
        }
    }
}
