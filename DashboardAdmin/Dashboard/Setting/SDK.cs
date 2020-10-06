using MongoDB.Bson;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAdmin.Dashboard.Setting
{
    sealed class SDK
    {
        sealed public class PageAUT
        {
            public static Links.PageAUT Links;
            public static async void Login(string Username,string Password,Action<bool> Result)
            {
                var client = new RestClient(Links.LinkLogin);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                client.ClearHandlers();
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Username", Username);
                request.AddParameter("Password", Password);
                var response =await client.ExecuteAsync(request);

                if (response.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    UserData.DataAdmin = BsonDocument.Parse(response.Content);
                    Result(true) ;
                }
                else
                {
                    Result(false);
                }
            }
          
         

        }

        sealed public class PageStatices
        {
            public static Links.PageStatices Links;

            public static async void ReciveStatices(Action<BsonDocument> Result,Action ERR)
            {
                var client = new RestClient(Links.ReciveStatices);
                client.Timeout = -1;
                client.ClearHandlers();
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Token", UserData.Token);
                var response =await client.ExecuteAsync(request);

                if (response.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    Result(BsonDocument.Parse(response.Content));
                }
                else
                {
                    ERR();
                }

            }

        }

    }

    public struct Links
    {
        public struct PageAUT
        {
            public string LinkLogin => "https://localhost:44346/PageAUT_Admin/Login";
        }
        public struct PageStatices
        {
            public string ReciveStatices => "https://localhost:44346/PageStatices/ReciveStatices";
        }
    }
}
