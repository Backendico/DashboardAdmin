using MongoDB.Bson;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace DashboardAdmin.Dashboard.Setting
{
    sealed class SDK
    {
        sealed public class PageAUT
        {
            public static Links.PageAUT Links;
            public static async void Login(string Username, string Password, Action<bool> Result)
            {
                var client = new RestClient(Links.LinkLogin);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                client.ClearHandlers();
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Username", Username);
                request.AddParameter("Password", Password);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UserData.DataAdmin = BsonDocument.Parse(response.Content);
                    Result(true);
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

            public static async void ReciveStatices(Action<BsonDocument> Result, Action ERR)
            {
                var client = new RestClient(Links.ReciveStatices);
                client.Timeout = -1;
                client.ClearHandlers();
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Token", UserData.Token);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(BsonDocument.Parse(response.Content));
                }
                else
                {
                    ERR();
                }

            }

        }

        sealed public class PageEmail
        {
            public static Links.PageEmail Links;

            public static async void ReciveEmailList(Action<BsonDocument> Result, Action ERR)
            {
                var client = new RestClient(Links.ReciveEmailList);
                client.Timeout = -1;
                client.ClearHandlers();
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Token", UserData.Token);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(BsonDocument.Parse(response.Content));
                }
                else
                {
                    Result(BsonDocument.Parse(response.Content));
                    ERR();
                }
            }

            public static async void AddEmail(BsonDocument Detail, Action<bool> Result)
            {
                var client = new RestClient(Links.AddEmail);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Detail", Detail.ToString());
                request.AddParameter("Token", UserData.Token);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(true);
                }
                else
                {
                    Result(false);
                }
            }

            public static async void RemoveEmail(string TokeEmail, Action<bool> Result)
            {
                var client = new RestClient(Links.RemoveEmail);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Token", UserData.Token);
                request.AddParameter("TokenEmail", TokeEmail);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(true);
                }
                else
                {
                    Result(false);
                }
            }

        }

        sealed public class PageUsers
        {
            public static Links.PageUsers Links;

            public static async void ReciveUsers(Action<BsonDocument> Result)
            {
                var client = new RestClient(Links.ReciveUser);
                client.ClearHandlers();
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Token", UserData.Token);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(BsonDocument.Parse(response.Content));
                }
                else
                {
                    Result(new BsonDocument());
                }
            }
        }

        sealed public class PageSupport
        {
            public static Links.PageSupport Links;

            public static async void ReciveSupport(Action<BsonDocument> Result, Action ERR)
            {
                var client = new RestClient(Links.ReciveSupports);
                client.Timeout = -1;
                client.ClearHandlers();
                var request = new RestRequest(Method.POST);
                request.AddParameter("Token", UserData.Token);
                var Responce = await client.ExecuteAsync(request);

                if (Responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(BsonDocument.Parse(Responce.Content));

                }
                else
                {
                    Result(new BsonDocument());
                    ERR();
                }
            }

        }
        sealed public class PageBugs
        {
            public static Links.PageBugs Links;

            public static async void ReciveBugs(Action<BsonDocument> Result, Action ERR)
            {
                var client = new RestClient(Links.ReciveBugs);
                client.Timeout = -1;
                client.ClearHandlers();
                var request = new RestRequest(Method.POST);
                request.AddParameter("Token", UserData.Token);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(BsonDocument.Parse(response.Content));
                }
                else
                {
                    Result(new BsonDocument());
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

        public struct PageEmail
        {
            public string ReciveEmailList => "https://localhost:44346/SubpageEmail/ReciveEmailList";
            public string AddEmail => "https://localhost:44346/SubpageEmail/AddEmail";
            public string RemoveEmail => "https://localhost:44346/SubpageEmail/RemoveEmail";
        }

        public struct PageUsers
        {
            public string ReciveUser => "https://localhost:44346/SubpageUsers/ReciveUsers";
        }

        public struct PageSupport
        {
            public string ReciveSupports => "https://localhost:44346/SubpageSupport/ReciveSupports";
        }

        public struct PageBugs
        {
            public string ReciveBugs => "https://localhost:44346/SubpageBugs/ReciveBugs";
        }
    }
}
