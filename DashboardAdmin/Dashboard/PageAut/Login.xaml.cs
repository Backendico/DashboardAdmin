using DashboardAdmin.Dashboard.Dashboard.Notifactions;
using DashboardAdmin.Dashboard.Dashboard.SubpageStatices;
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

namespace DashboardAdmin.Dashboard.PageAut
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            MainWindow.Dashboard.Blure(true);
        }

        public void LoginAction(object sender, MouseButtonEventArgs e)
        {
            SDK.PageAUT.Login(TextUsername.Text, TextPassword.Password, result =>
            {
                if (result)
                {
                    Debug.WriteLine(UserData.Token);

                    Notifaction("Logined", StatusMessage.Ok);

                    MainWindow.Dashboard.Root.Children.Remove(this);
                    MainWindow.Dashboard.Blure(false);
                    MainWindow.Dashboard.InitDashboard();
                }
                else
                {
                    Notifaction("LoginFaild", StatusMessage.Error);
                }

            });
        }

        public static void Notifaction(string Message, StatusMessage Status) => new Notifaction(Message, Status);



    }
}
