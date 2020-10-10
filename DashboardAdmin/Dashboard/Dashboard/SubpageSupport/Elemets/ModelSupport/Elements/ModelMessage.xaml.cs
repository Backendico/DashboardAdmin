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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageSupport.Elemets.ModelSupport.Elements
{
    /// <summary>
    /// Interaction logic for ModelMessage.xaml
    /// </summary>
    public partial class ModelMessage : UserControl
    {
        public ModelMessage(BsonDocument DetailMessage)
        {
            InitializeComponent();
            TextTime.Text = DetailMessage["Created"].ToLocalTime().ToString();

            TextSender.Text = (DetailMessage["Sender"].ToInt32()) switch
            {
                0 => TextSender.Text = "User",
                1 => TextSender.Text = "Supporter",
                _=> TextSender.Text = "Not Set"
            };


           HorizontalAlignment= (DetailMessage["Sender"].ToInt32()) switch
            {
                0 => HorizontalAlignment.Left,
                1=>HorizontalAlignment.Right,
                _=>HorizontalAlignment.Stretch,
            };

            TextMessage.Text = DetailMessage["Message"].AsString;

        }
    }
}
