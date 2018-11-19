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
        public int position = 0;
        public string[] images = {"Bild1.png", "Bild2.png" , "Bild3.png" , "Bild4.png" };
        public Tutorial()
        {
            InitializeComponent();
            drawImage();
        }

        private void drawImage()
        {
            Uri uriSource = new Uri("Resources/" + images[position], UriKind.Relative);

            Image.Source = new BitmapImage(uriSource);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            position = Math.Max(position - 1, 0);
            drawImage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            position = Math.Min(position + 1, images.Length - 1);
            drawImage();
        }
    }
}
