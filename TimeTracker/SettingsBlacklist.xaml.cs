using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeTracker.Properties;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für SettingsBlacklist.xaml
    /// </summary>
    public partial class SettingsBlacklist : UserControl
    {
        public class Item : INotifyPropertyChanged
        {
            private string name;
            // Declare the event
            public event PropertyChangedEventHandler PropertyChanged;

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                    // Call OnPropertyChanged whenever the property is updated
                    OnPropertyChanged("Name");
                }
            }

            public Guid id = Guid.NewGuid();

            // Create the OnPropertyChanged method to raise the event
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<Item> Items;
        public SettingsBlacklist()
        {
            InitializeComponent();
            Items = new ObservableCollection<Item>();

            foreach (string b in Settings.Default.Blacklist)
            {
                Item item = new Item
                {
                    Name = b
                };

                item.PropertyChanged += Item_PropertyChanged;
                Items.Add(item);
            }

            Items.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedMethod);

            DataGrid.ItemsSource = Items;
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Item item = sender as Item;
            Settings.Default.Blacklist[Items.IndexOf(item)] = item.Name;
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            //different kind of changes that may have occurred in collection
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(Item item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                    Settings.Default.Blacklist.Add(item.Name ?? "");
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                //your code
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Item item in e.OldItems)
                {
                    Settings.Default.Blacklist.Remove(item.Name);
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                //your code
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Items.Clear();
            Settings.Default.Blacklist.Clear();

            string[] blacklist = {
                "TimeTracker",
                "Neue Benachrichtigung",
                "Explorer",
                "Cortana",
                "Akkuinformationen",
                "Start",
                "UnlockingWindow",
                "Cortana",
                "Akkuinformationen",
                "Status",
                "Aktive Anwendungen",
                "Window Dialog",
                "Info-Center",
                "Windows-Standardsperrbildschirm",
                "Host für die Windows Shell-Oberfläche",
                "F12PopupWindow",
                "LockingWindow",
                "SurfaceDTX",
                "CTX_RX_SYSTRAY",
                "[]"
            };

            foreach(string b in blacklist)
            {
                Item item = new Item
                {
                    Name = b
                };

                item.PropertyChanged += Item_PropertyChanged;
                Items.Add(item);
            }
        }
    }
}
