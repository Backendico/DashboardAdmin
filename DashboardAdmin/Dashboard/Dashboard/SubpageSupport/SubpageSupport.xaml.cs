using DashboardAdmin.Dashboard.Dashboard.SubpageSupport.Elemets;
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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageSupport
{
    /// <summary>
    /// Interaction logic for SubpageSupport.xaml
    /// </summary>
    public partial class SubpageSupport : UserControl
    {
        public SubpageSupport()
        {
            InitializeComponent();
            ReciveSupports();
        }

        public void ReciveSupports()
        {
            PlaceContentSupports.Children.Clear();

            SDK.PageSupport.ReciveSupport(
                result =>
                {
                    foreach (var Support in result["ListSupports"].AsBsonArray)
                    {
                        PlaceContentSupports.Children.Add(new ModelSupport(Support.AsBsonDocument));

                    }
                },
                () =>
                {
                    MainWindow.Notifaction("Support Not Found", Notifactions.StatusMessage.Error);
                });
        }
    }

}
