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
                    MainWindow.Notifaction("Players Not Found", Notifactions.StatusMessage.Warrning);
                }
            });
        }
    }
}
