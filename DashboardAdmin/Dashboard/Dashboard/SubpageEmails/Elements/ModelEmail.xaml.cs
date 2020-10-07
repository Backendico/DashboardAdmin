using DashboardAdmin.Dashboard.Setting;
using MongoDB.Bson;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageEmails.Elements
{
    public partial class ModelEmail : UserControl
    {
        public ModelEmail(BsonDocument DetailEmail, Action RefreshListEmail)
        {
            InitializeComponent();

            TextToken.Text = DetailEmail["Token"].ToString();
            TextEmail.Text = DetailEmail["Email"].AsString;
            TextCreated.Text = DetailEmail["Created"].ToLocalTime().ToString();
            TextLastSend.Text = DetailEmail["LastSend"].ToLocalTime().ToString();
            TextSender.Text = DetailEmail["Sender"].AsString;


            BTNDeleteEmail.MouseDown += (s, e) =>
            {
                SDK.PageEmail.RemoveEmail(DetailEmail["Token"].ToString(), result =>
                {
                    if (result)
                    {
                        RefreshListEmail();
                        MainWindow.Notifaction("Deleted", Notifactions.StatusMessage.Ok);
                    }
                    else
                    {
                        MainWindow.Notifaction("Faild Delete", Notifactions.StatusMessage.Error);
                    }

                });
            };



        }
    }
}
