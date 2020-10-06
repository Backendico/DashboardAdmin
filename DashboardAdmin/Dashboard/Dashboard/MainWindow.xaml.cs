using DashboardAdmin.Dashboard.Dashboard.SubpageBugs;
using DashboardAdmin.Dashboard.Dashboard.SubpageEmails;
using DashboardAdmin.Dashboard.Dashboard.SubpageStatices;
using DashboardAdmin.Dashboard.Dashboard.SubpageSupport;
using DashboardAdmin.Dashboard.Dashboard.SubpageUsers;
using DashboardAdmin.Dashboard.PageAut;
using DashboardAdmin.Dashboard.Setting;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace DashboardAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Dashboard;

        UserControl CurentPage;
        TextBlock CurentTab;
        BlurEffect Blur = new BlurEffect();

        public MainWindow()
        {
            InitializeComponent();
            Dashboard = this;

            CurentTab = BTNStatices;
            Root.Children.Add(new Login());


        }


        internal void InitDashboard()
        {
            TextUsername.Text = UserData.Username;
            Content.Children.Add(new SubpageStatices());
        }
        internal async void Blure(bool OnOff)
        {
            if (OnOff)
            {
                PageDashboard.Effect = Blur;
                while (Blur.Radius < 10)
                {
                    Blur.Radius += 0.5d;
                    await Task.Delay(4);

                    if (Blur.Radius >= 10)
                        break;
                }
            }
            else
            {
                while (Blur.Radius > 0)
                {
                    Blur.Radius -= 0.5d;
                    await Task.Delay(4);

                    if (Blur.Radius <= 0)
                    {
                        PageDashboard.Effect = null;
                        break;

                    }
                }

            }
        }
        private void Window_LayoutUpdated(object sender, EventArgs e)
        {

                if (NameList.Width >= 100)
                {
                    BTNOpenPane.Foreground = new SolidColorBrush(Colors.Orange);
                    BTNOpenPane.Text = "\xEA49";
                }
                else
                {
                    BTNOpenPane.Text = "\xEA5B";
                }

                CurentTab.Foreground = new SolidColorBrush(Colors.Orange);

          
        }
        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            Close();
        }


        public void ChangeColor_Active(object sender, MouseEventArgs e)
        {
            var TextBlock = sender as TextBlock;
            ColorAnimation Anim = new ColorAnimation(fromValue: Colors.Gray, toValue: Colors.Orange, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

            DoubleAnimation Anim1 = new DoubleAnimation(18, 20, TimeSpan.FromSeconds(0.1));
            Storyboard.SetTargetName(Anim1, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("FontSize"));


            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);

            storyboard.Begin(this);

        }
        public void ChangeColor_DeActive(object sender, MouseEventArgs e)
        {
            var TextBlock = sender as TextBlock;

            ColorAnimation Anim = new ColorAnimation(fromValue: Colors.Orange, toValue: Colors.Gray, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTargetName(Anim, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

            DoubleAnimation Anim1 = new DoubleAnimation(20, 18, TimeSpan.FromSeconds(0.1));
            Storyboard.SetTargetName(Anim1, TextBlock.Name);
            Storyboard.SetTargetProperty(Anim1, new PropertyPath("FontSize"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(Anim);
            storyboard.Children.Add(Anim1);
            storyboard.Begin(this);

        }
        private void ChangePage(object sender, MouseButtonEventArgs e)
        {
            Content.Children.Clear();

            CurentTab.Foreground = new SolidColorBrush(Colors.Gray);

            switch ((sender as TextBlock).Name)
            {
                case "BTNStatices":
                    CurentPage = new SubpageStatices();
                    CurentTab = BTNStatices;
                    break;
                case "BTNEmails":
                    CurentPage = new SubpageEmails();
                    CurentTab = BTNEmails;
                    break;
                case "BTNSupport":
                    CurentPage = new SubpageSupport();
                    CurentTab = BTNSupport;
                    break;
                case "BTNUsers":
                    CurentPage = new SubpageUsers();
                    CurentTab = BTNUsers;
                    break;
                case "BTNBugs":
                    CurentPage = new SubpageBugs();
                    CurentTab = BTNBugs;
                    break;
                default:
                    Debug.WriteLine("Not set");
                    break;
            }

            Content.Children.Add(CurentPage);
            CurentTab.Foreground = new SolidColorBrush(Colors.Orange);
        }
        public void ControlPane(object sender, MouseButtonEventArgs e)
        {
            Storyboard storyboard = new Storyboard();

            if (NameList.Width >= 100)
            {
                DoubleAnimation Anim = new DoubleAnimation(100, 0, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim, NameList.Name);
                Storyboard.SetTargetProperty(Anim, new PropertyPath("Width"));
                storyboard.Children.Add(Anim);
            }
            else
            {
                DoubleAnimation Anim = new DoubleAnimation(0, 100, TimeSpan.FromSeconds(0.3));
                Storyboard.SetTargetName(Anim, NameList.Name);
                Storyboard.SetTargetProperty(Anim, new PropertyPath("Width"));
                storyboard.Children.Add(Anim);
            }

            storyboard.Begin(this);
        }

    }
}
