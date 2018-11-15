﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaktionslogik für CustomToast.xaml
    /// </summary>
    public partial class CustomToast : Window
    {
        List<CustomComboBoxItem> Activities;
        int id;
        Stack<bool> cancelClose = new Stack<bool>();

        long timeout = 5000;

        class CustomComboBoxItem
        {
            public string Name { get; set; }
            public bool Selectable { get; set; }
            public string Visible { get; set; }
        }


        public CustomToast(string id_string, string window)
        {
            InitializeComponent();

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 15;
            this.Top = desktopWorkingArea.Bottom - this.Height - 12;

            using (mainEntities db = new mainEntities())
            {
                bool lastActivities = db.settings.Find("lastActivities") != null ? db.settings.Find("lastActivities").value == 1 : Constants.lastActivities;

                id = int.Parse(id_string);
                activity_active new_activity = db.activity_active.Find(id);
            
                Activities = db.Database.SqlQuery<string>("SELECT name FROM activity_active GROUP BY name ORDER BY max([from]) DESC").Select(a => new CustomComboBoxItem()
                {
                    Name = a,
                    Selectable = true
                }).ToList();

                for(int i = 0; i < Activities.Count(); i++)
                {
                    Activities[i].Visible = i < 5 ? "Visible" : "Collapsed"; // Make only the first 5 options visible
                }

                Activities.Insert(0, new CustomComboBoxItem()
                {
                    Name = "Activity - Subactivity",
                    Selectable = false
                });

                ComboBox.ItemsSource = Activities;

                ComboBox.SelectedItem = Activities.Where(a => a.Name.Equals(new_activity.name)).FirstOrDefault();

                TextBlock2.Text = window.Trim();

                timeout = db.settings.Find("timeout") != null ? db.settings.Find("timeout").value : Constants.defaultTimeout;
            }

            setupClose();
        }

        private async void setupClose()
        {
            await Task.Delay((int)timeout);

            if(cancelClose.Count() == 0 || cancelClose.Pop() != true)
                this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            cancelClose.Push(true);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            setupClose();
        }

        private void setNewActivity()
        {
            using (mainEntities db = new mainEntities())
            {
                activity_active new_activity = db.activity_active.Find(id);
                if(new_activity != null)
                    new_activity.name = ComboBox.Text;

                db.SaveChanges();
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            setNewActivity();
        }

        private void ComboBox_OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                setNewActivity();
            }
        }
    }
}
