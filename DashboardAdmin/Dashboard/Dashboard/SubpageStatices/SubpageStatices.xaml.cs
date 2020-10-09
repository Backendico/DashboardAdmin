using DashboardAdmin.Dashboard.Dashboard.SubpageBugs;
using DashboardAdmin.Dashboard.Dashboard.SubpageEmails;
using DashboardAdmin.Dashboard.Setting;
using Microsoft.VisualBasic.CompilerServices;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;

namespace DashboardAdmin.Dashboard.Dashboard.SubpageStatices
{
    /// <summary>
    /// Interaction logic for SubpageStatices.xaml
    /// </summary>
    public partial class SubpageStatices : UserControl
    {
        public SubpageStatices()
        {
            InitializeComponent();

            SDK.PageStatices.ReciveStatices(
                Result =>
                {
                    TextUserCount.Text = Result["Users"].ToString();
                    TextBugs.Text = Result["BugReport"].ToString();
                    TextSupportCount.Text = Result["Supports"].ToString();
                    TextEmail_Send.Text = Result["Emails"]["Send"].ToString();
                    TextEmailRegister_Count.Text = Result["Emails"]["Register"].ToString();
                    TextCash.Text = Result["Cash"].ToInt64().ToString("#,##0")+" T";
                }, () =>
                {


                });
        }





    }
}
