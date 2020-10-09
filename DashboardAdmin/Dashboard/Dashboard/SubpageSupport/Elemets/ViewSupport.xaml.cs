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
        public ViewSupport(BsonDocument DetailSupport)
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

            foreach (var item in DetailSupport["Messages"].AsBsonArray)
            {
                Debug.WriteLine(item.ToString());
            }

            //init btns
            Root.MouseDown += (s, e) =>
            {
                if (e.Source.GetType() == typeof(Grid))
                {
                    ShowOffPanel();
                }
            };

            Loaded += (s, e) =>
            {
                ShowPanel();
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
