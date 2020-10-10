using DashboardAdmin.Dashboard.Dashboard.SubpageBugs.Elements.ViewBug;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageBugs.Elements
{
    /// <summary>
    /// Interaction logic for ModelBugs.xaml
    /// </summary>
    public partial class ModelBugs : UserControl
    {
        public ModelBugs(BsonDocument DetailBug)
        {
            InitializeComponent();

            TextTopic.Text = (DetailBug["Topic"].ToInt32()) switch
            {
                0 => "Other",
                1 => "Authentication",
                2 => "Dashboard",
                3 => "Dashboard-Dashboard",
                4 => "Dashboard-Player",
                5 => "Dashboard-Leaderboard",
                6 => "Dashboard-Logs",
                7 => "Payment",
                8 => "Studios",
                9 => "Languages",
                _ => "NotSet",
            };

            Priority.BorderBrush = (DetailBug["Priority"].ToInt32()) switch
            {
                0 => new SolidColorBrush(Colors.LightGreen),
                1 => new SolidColorBrush(Colors.Orange),
                2 => new SolidColorBrush(Colors.Red),
                _ => new SolidColorBrush(Colors.Transparent),

            };


            TextSubject.Text = DetailBug["Subject"].AsString;
            TextDescription.Text = DetailBug["Description"].AsString;
            TextDashboard.Text = DetailBug["Dashboard"].AsString;
            TextDatabase.Text = DetailBug["Database"].AsString;

            //action btn view
            BTNView.MouseDown += (s, e) =>
            {
                MainWindow.Dashboard.Content.Children.Add(new ViewBug.ViewBug(DetailBug, null));

            };
        }

    }
}
