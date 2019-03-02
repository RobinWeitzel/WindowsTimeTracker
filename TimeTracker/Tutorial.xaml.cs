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
    /// Interaktionslogik für Tutorial.xaml
    /// </summary>
    public partial class Tutorial : Window
    {
        public int Position = 0;
        public string[] Images = {"Bild1.png", "Bild2.png" , "Bild3.png" , "Bild4.png" };
        public string[] Text = { "Welcome to TimeTracker - your personal time keeper!", "Any time you switch to a program you have not used for a while TimeTracker will ask if you are still working on the same activity.", "You can manually change the current activity, access settings and view the recorded data by right-clicking on the icon in the System Tray.", "", "" };
        public Tutorial()
        {
            InitializeComponent();
            DrawImage();
        }

        private void DrawImage()
        {
            Uri uriSource = new Uri("Resources/" + Images[Position], UriKind.Relative);

            Image.Source = new BitmapImage(uriSource);
            TextBlock.Text = Text[Position];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Position = Math.Max(Position - 1, 0);

            Next.Content = "Next";
            if (Position == 0)
                Previous.IsEnabled = false;
                
            DrawImage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Position == Images.Length - 1)
            {
                this.Close();
                return;
            }

            Position = Math.Min(Position + 1, Images.Length - 1);

            Previous.IsEnabled = true;
            if (Position == Images.Length - 1)
                Next.Content = "Close";

            DrawImage();
        }
    }
}
