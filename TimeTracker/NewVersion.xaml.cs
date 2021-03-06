﻿using System;
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

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für NewVersion.xaml
    /// </summary>
    public partial class NewVersion : Window
    {
        public NewVersion()
        {
            InitializeComponent();
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
    }
}
