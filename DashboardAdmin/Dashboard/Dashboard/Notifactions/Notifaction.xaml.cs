using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashboardAdmin.Dashboard.Dashboard.Notifactions
{
    /// <summary>
    /// Interaction logic for Notifaction.xaml
    /// </summary>
    public partial class Notifaction : UserControl
    {
        public Notifaction(string _Message, StatusMessage Status)
        {
            InitializeComponent();

            TextMessage.Text = _Message;

            switch (Status)
            {
                case StatusMessage.Error:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    break;
                case StatusMessage.Ok:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                    break;
                case StatusMessage.Warrning:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    break;
                default:
                    BorderColor.BorderBrush = new SolidColorBrush(Colors.Black);
                    break;
            }
            MainWindow.Dashboard.Root.Children.Add(this);

        }

        public void Close(object sender, EventArgs e)
        {
            MainWindow.Dashboard.Root.Children.Remove(this);
        }



    }
    public enum StatusMessage
    {

        Error, Ok, Warrning
    }
}
