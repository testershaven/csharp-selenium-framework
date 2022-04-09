using RestSharp;
using System;

namespace TestingFramework.ApiClient.Endpoints
{
    public class ApiClientManager
    {
        private static readonly Lazy<RestClient> _lazy =
         new(() => new RestClient(ConfigManager.AppSettings["ApiClient:ServiceUrl"]));

        public static RestClient ApiClient { get { return _lazy.Value; } }

        static ApiClientManager()
        {
            ApiClient.AddDefaultHeader("Authorization", ConfigManager.AppSettings["ApiClient:Token"]);
        }

        private ApiClientManager()
        {
        }
    }
}
