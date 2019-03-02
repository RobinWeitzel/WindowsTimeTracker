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

        /// <summary>
        /// Allows the user to manually change the activies and their time slots
        /// </summary>
        /// <param name="storageHandler">Used to read and write from/to the csv files</param>
        public ManualEdit(StorageHandler storageHandler)
        {
            InitializeComponent();
            StorageHandler = storageHandler;
            LoadData();
        }

        /// <summary>
        /// Loads all activites from the csv file and show them in the table
        /// </summary>
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
