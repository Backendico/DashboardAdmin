using DashboardAdmin.Dashboard.Dashboard.SubpageSupport.Elemets.ModelSupport.Elements;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageSupport.Elemets
{
    /// <summary>
    /// Interaction logic for ViewSupport.xaml
    /// </summary>
    public partial class ViewSupport : UserControl
    {
        public ViewSupport(BsonDocument DetailSupport,Action Refreshlist)
        {
            InitializeComponent();


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

            TextToken.Text = DetailSupport["Token"].ToString();

            TextTokenUser.Text = DetailSupport["Creator"].ToString();

            TextCreated.Text = DetailSupport["Created"].ToLocalTime().ToString();

            TextStudio.Text = DetailSupport["Studio"].ToString();

            //init message
            foreach (var item in DetailSupport["Messages"].AsBsonArray)
            {
                PlaceMessages.Children.Add(new ModelMessage(item.AsBsonDocument));
            }


            //init close
            Root.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanel();
                }
            };

            //event BTN Send
            BTNSend.MouseDown += (s, e) =>
            {
                if (TextboxMessage.Text.Length >= 1)
                {
                    var Message = new BsonDocument
                {
                    {"Message",TextboxMessage.Text },
                    {"Sender",1 },
                };

                    SDK.PageSupport.AddMessageToSupport(DetailSupport["Token"].AsObjectId, DetailSupport["Studio"].AsString, Message,
                        result =>
                        {
                            if (result)
                            {
                                TextboxMessage.Text = null;

                                //send notifaction
                                MainWindow.Notifaction("Support Send", Notifactions.StatusMessage.Ok);

                                //add fake message
                                Message.Add("Creator", 1);
                                Message.Add("Created", DateTime.Now);
                                PlaceMessages.Children.Add(new ModelMessage(Message));
                            }
                            else
                            {
                                MainWindow.Notifaction("Faild Send", Notifactions.StatusMessage.Error);
                            }

                        });
                }
                else
                {
                    MainWindow.Notifaction("Message Short", Notifactions.StatusMessage.Warrning);
                }
            };

            //event BTN Block
            BTNBlock.MouseDown += (s, e) =>
            {
                SDK.PageSupport.BlockMessage(DetailSupport["Studio"].AsString, DetailSupport["Token"].AsObjectId,
                    result =>
                    {
                        if (result)
                        {
                            MainWindow.Notifaction("Blocked", Notifactions.StatusMessage.Ok);
                            BTNBlock.Visibility = Visibility.Collapsed;
                            PlaceSendMessage.Visibility = Visibility.Collapsed;
                            Refreshlist();
                        }
                        else
                        {
                            MainWindow.Notifaction("Faild Block", Notifactions.StatusMessage.Error);
                        }
                    });

            };

            Loaded += (s, e) =>
            {
                ShowPanel();
            };

        }


        void Bloced()
        {

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
