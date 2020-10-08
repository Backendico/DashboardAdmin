using DashboardAdmin.Dashboard.Dashboard.SubpageUsers.Elements;
using DashboardAdmin.Dashboard.Setting;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageUsers
{
    /// <summary>
    /// Interaction logic for SubpageUsers.xaml
    /// </summary>
    public partial class SubpageUsers : UserControl
    {
        public SubpageUsers()
        {
            InitializeComponent();
            InitizePageUsers();
        }

        public void InitizePageUsers()
        {
            PlaceContentUsers.Children.Clear();

            SDK.PageUsers.ReciveUsers(result =>
            {
                if (result["ListUsers"].AsBsonArray.Count >= 1)
                {
                    foreach (var item in result["ListUsers"].AsBsonArray)
                    {
                        PlaceContentUsers.Children.Add(new ModelUser(item.AsBsonDocument));
                    }
                }
                else
                {
                    ShowOffPanelpanelAdd();
                    MainWindow.Notifaction("Players Not Found", Notifactions.StatusMessage.Warrning);
                }
            });

            BTNAdd.MouseDown += (s, e) =>
            {
                ShowPanelpanelAdd();
            };

            PanelAddPlayer.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelpanelAdd();
                }

            };
        }

        void ShowPanelpanelAdd()
        {
            PanelAddPlayer.Visibility = Visibility.Visible;
            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);

        }

        void ShowOffPanelpanelAdd()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddPlayer.Visibility = Visibility.Collapsed;
            };
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

    }
}
