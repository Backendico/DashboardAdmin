using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DashboardAdmin.Dashboard.Setting
{
    class UserData
    {
        public static BsonDocument DataAdmin;

        public static ObjectId Token => DataAdmin["Account"]["Token"].AsObjectId;
        public static string Username => DataAdmin["Account"]["Username"].AsString;

    }
}
