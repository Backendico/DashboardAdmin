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

namespace DashboardAdmin.Dashboard.Dashboard.SubpageUsers.Elements
{
    public partial class ModelUser : UserControl
    {
        public ModelUser(BsonDocument DetailUser)
        {
            InitializeComponent();

            TextToken.Text = DetailUser["Token"].ToString();
            TextUsername.Text = DetailUser["Username"].AsString;
            TextPlayers.Text = DetailUser["Players"].ToString();
            TextLeaderboards.Text = DetailUser["Leaderboards"].ToString();
            TextStudios.Text = DetailUser["GameStudio"].ToString();
            TextCash.Text = DetailUser["Cash"].ToString();
            TextEmail.Text = DetailUser["Email"].AsString;
            TextPhone.Text = DetailUser["Phone"].AsString;
        }
    }
}
