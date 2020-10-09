using DashboardAdmin.Dashboard.Dashboard.SubpageBugs.Elements;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageBugs
{
    /// <summary>
    /// Interaction logic for SubpageBugs.xaml
    /// </summary>
    public partial class SubpageBugs : UserControl
    {
        public SubpageBugs()
        {
            InitializeComponent();
            ReciveLisBugs();
        }

        void ReciveLisBugs()
        {
            SDK.PageBugs.ReciveBugs(
                result =>
                {

                    foreach (var item in result["ListBugs"].AsBsonArray)
                    {
                        PlaceBugs.Children.Add(new ModelBugs(item.AsBsonDocument));
                    }

                },
                () =>
                {
                });
        }
    }
}
