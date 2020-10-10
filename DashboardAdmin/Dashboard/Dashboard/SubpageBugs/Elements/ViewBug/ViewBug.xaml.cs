using DashboardAdmin.Dashboard.Setting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashboardAdmin.Dashboard.Dashboard.SubpageBugs.Elements.ViewBug
{
    /// <summary>
    /// Interaction logic for ViewBug.xaml
    /// </summary>
    public partial class ViewBug : UserControl
    {

        public ViewBug(BsonDocument DetailBug, Action Refreshlist)
        {
            InitializeComponent();

            //init frist change
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


            TextPariority.Text = (DetailBug["Priority"].ToInt32()) switch
            {
                0 => "Low",
                1 => "Normal",
                2 => "High",
                _ => "Not Set",
            };

            TextPariority.Foreground = (DetailBug["Priority"].ToInt32()) switch
            {
                0 => new SolidColorBrush(Colors.Green),
                1 => new SolidColorBrush(Colors.Orange),
                2 => new SolidColorBrush(Colors.Tomato),
                _ => new SolidColorBrush(Colors.Transparent),
            };

            TextDashboard.Text = DetailBug["Dashboard"].AsString;
            TextDatabase.Text = DetailBug["Database"].AsString;

            TextSubject.Text = DetailBug["Subject"].AsString;
            TextMessage.Text = DetailBug["Description"].AsString;

            TextFollow.Text = (DetailBug["Follow"].AsBoolean) switch
            {
                true => "\xE73A",
                false => "\xE739",
            };

            Loaded += (s, e) =>
            {
                ShowPanel();
            };

            Root.MouseDown += (s, e) =>
            {
                ShowOffPanel();
            };


            Debug.WriteLine(DetailBug.ToString());

            //btn send prize
            BTNSendPrize.MouseDown += (s, e) =>
            {
                SDK.Globals.SendNotifaction(DetailBug["Token"].AsObjectId, DetailBug["Database"].AsString, "Gift", "Due to the bug report, the amount of \"1000\" Tomans was transferred to your account.", new BsonDocument { },
                    result =>
                    {
                        if (result)
                        {
                            MainWindow.Notifaction("Prize notifaction send", Notifactions.StatusMessage.Ok);
                        }
                        else
                        {
                            MainWindow.Notifaction("Faild Send", Notifactions.StatusMessage.Error);
                        }
                    });
            };

            //action btn BugRemove
            BTNRemove.MouseDown += (s, e) =>
            {
                SDK.PageBugs.RemoveBug(DetailBug["_id"].AsObjectId,
                    result =>
                    {
                        if (result)
                        {
                            MainWindow.Notifaction("Removed", Notifactions.StatusMessage.Ok);
                        }
                        else
                        {
                            MainWindow.Notifaction("Faild Remove", Notifactions.StatusMessage.Error);
                        }
                    });
            };

        }


        void ShowOffPanel()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));

            Anim.Completed += (s, e) =>
            {
                MainWindow.Dashboard.Content.Children.Remove(this);
            };

            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, Root.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);

            storyboard.Begin(this);
        }

        void ShowPanel()
        {

            DoubleAnimation Anim = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));

            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, Root.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);

            storyboard.Begin(this);
        }
    }
}
