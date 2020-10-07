using DashboardAdmin.Dashboard.Dashboard.SubpageEmails.Elements;
using DashboardAdmin.Dashboard.Setting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageEmails
{
    /// <summary>
    /// Interaction logic for SubpageEmails.xaml
    /// </summary>
    public partial class SubpageEmails : UserControl
    {
        public SubpageEmails()
        {
            InitializeComponent();

            BTNAddEmail.MouseDown += (s, e) =>
            {
                ShowOpenPanelAddEmail();
            };

            PanelAddPlayer.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanelAddEmail();
                }
            };

            BTNActionAdd.MouseDown += (s, e) =>
            {
                try
                {
                    _ = new MailAddress(TextBoxEmail.Text) ;

                    var Detail = new BsonDocument
                {
                    {"Email",TextBoxEmail.Text },
                    {"Sender",UserData.Username }
                };
                    SDK.PageEmail.AddEmail(Detail,
                        result =>
                        {
                            if (result)
                            {
                                TextBoxEmail.Text = "";
                                MainWindow.Notifaction("Email Send", Notifactions.StatusMessage.Ok);
                                InitilizeEmail();
                            }
                            else
                            {
                                MainWindow.Notifaction("Faild", Notifactions.StatusMessage.Error);
                            }
                        });
                }
                catch (Exception ex )
                {
                    TextBoxEmail.Text = null;
                    MainWindow.Notifaction(ex.Message, Notifactions.StatusMessage.Error);
                }
            };

            InitilizeEmail();
        }


        internal void InitilizeEmail()
        {
            PlaceEmails.Children.Clear();

            SDK.PageEmail.ReciveEmailList(
                result =>
                {
                    if (result["ListEmails"].AsBsonArray.Count >= 1)
                    {
                        foreach (var item in result["ListEmails"].AsBsonArray)
                        {
                            PlaceEmails.Children.Add(new ModelEmail(item.AsBsonDocument, InitilizeEmail));
                        }
                    }
                    else
                    {
                        ShowOpenPanelAddEmail();
                    }
                },
                () =>
                {


                });
        }



        void ShowOpenPanelAddEmail()
        {
            PanelAddPlayer.Visibility = Visibility.Visible;

            DoubleAnimation Anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }

        void ShowOffPanelAddEmail()
        {
            DoubleAnimation Anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Anim.Completed += (s, e) =>
            {
                PanelAddPlayer.Visibility = Visibility.Collapsed;

                TextBoxEmail.Text = "";
            };

            Storyboard.SetTargetName(Anim, PanelAddPlayer.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("Opacity"));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Begin(this);
        }


    }
}
