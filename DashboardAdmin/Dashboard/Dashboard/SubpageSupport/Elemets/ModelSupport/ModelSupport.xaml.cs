﻿using DashboardAdmin.Dashboard.Setting;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageSupport.Elemets.ModelSupport
{
    /// <summary>
    /// Interaction logic for ModelSupport.xaml
    /// </summary>
    public partial class ModelSupport : UserControl
    {
        public ModelSupport(BsonDocument DetailSupport, Action Refreshlist)
        {
            InitializeComponent();

            TextToken.Text = DetailSupport["Token"].ToString();
            TextTokenUsername.Text = DetailSupport["Creator"].ToString();
            TextHeader.Text = DetailSupport["Header"].AsString;
            switch (DetailSupport["Part"].ToInt32())
            {
                case 0:
                    TextPart.Text = "Authenticator";
                    break;
                case 1:
                    TextPart.Text = "Dashboard";
                    break;
                case 2:
                    TextPart.Text = "Payment";
                    break;
            }

            TextCreated.Text = DetailSupport["Created"].ToLocalTime().ToString();

            switch (DetailSupport["Priority"].ToInt32())
            {
                case 0:
                    Priority.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                    break;
                case 1:
                    Priority.BorderBrush = new SolidColorBrush(Colors.Orange);
                    break;
                case 2:
                    Priority.BorderBrush = new SolidColorBrush(Colors.Tomato);
                    break;
            }


            //init BTNs
            BTNViewSupport.MouseDown += (s, e) =>
            {
                MainWindow.Dashboard.Content.Children.Add(new ViewSupport(DetailSupport,Refreshlist));
            };


            BTNBlockSupport.MouseDown += (s, e) =>
            {
                SDK.PageSupport.BlockMessage(DetailSupport["Studio"].AsString, DetailSupport["Token"].AsObjectId,
                    Result =>
                    {
                        if (Result)
                        {
                            MainWindow.Notifaction("Blocked Support", Notifactions.StatusMessage.Ok);
                            Refreshlist();
                        }
                        else
                        {
                            MainWindow.Notifaction("Faild Block", Notifactions.StatusMessage.Error);
                        }
                    });

            };

        }



    }
}
