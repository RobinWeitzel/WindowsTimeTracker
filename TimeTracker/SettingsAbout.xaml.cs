using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TimeTracker.Helper;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für SettingsAbout.xaml
    /// </summary>
    public partial class SettingsAbout : UserControl
    {

        /// <summary>
        /// Inializes the settings-About window.
        /// </summary>
        public SettingsAbout()
        {
            InitializeComponent();
            Version.Content = "Version: " + AppStateTracker.Version;

            List<Library> libraries = new List<Library>
            {
                new Library
                {
                    Name = "CsvHelper",
                    Author = "JoshClose",
                    License = "Apache-2.0"
                },
                new Library
                {
                    Name = "CefSharp",
                    Author = "CefSharp Community",
                    License = "BSD"
                }
            };

            Libraries.ItemsSource = libraries;
        }

        /// <summary>
        /// Opens a browser window poiting to the provided link
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event (contains the url to which the browser should point)</param>
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        /// <summary>
        /// Triggered when the users clicks on a library listed in the table.
        /// Opens up a browser window pointing to the corresponding license.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event</param>
        private void Libraries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Libraries.SelectedItem != null)
            {
                Library library = Libraries.SelectedItem as Library;

                switch(library.License)
                {
                    case "Apache-2.0":
                        Process.Start("https://opensource.org/licenses/Apache-2.0");
                        break;
                    case "BSD":
                        Process.Start("https://opensource.org/licenses/BSD-3-Clause");
                        break;
                }

                Libraries.SelectedIndex = -1;
            }
        }
    }
}
