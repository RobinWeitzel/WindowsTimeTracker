using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    /// Interaktionslogik für Data.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        UserControl currentControll;
        public DataWindow()
        {
            InitializeComponent();

            Switcher.pageSwitcher = this;
            Switcher.Switch(new Overview());  //initial page   
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            if (e == null || e.OriginalSource == null)
                return;
            Button btn = e.OriginalSource as Button;

            if (btn == null || btn.Content == null)
                return;

            switch(btn.Content)
            {
                case "Details":
                    Navigate(new Details());
                    break;
                case "Overview":
                    Navigate(new Overview());
                    break;
            }
        }
    }
}
