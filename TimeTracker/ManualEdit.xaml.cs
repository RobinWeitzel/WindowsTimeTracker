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
        private List<Helper.Activity> Activities;

        private StorageHandler StorageHandler;

        public ManualEdit(StorageHandler storageHandler)
        {
            InitializeComponent();
            StorageHandler = storageHandler;
            LoadData();
        }

        private void LoadData()
        {
            Activities = StorageHandler.GetLastActivities();
            DataGrid.ItemsSource = Activities;
        }

        /* Window events */

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StorageHandler.WriteActivities(Activities);
            LoadData();
        }
    }
}
