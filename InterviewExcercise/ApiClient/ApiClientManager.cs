using RestSharp;
using RestSharp.Serialization.Json;
using System;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class ApiClientManager
    {

        private static readonly Lazy<RestClient> _lazy =
         new(() => new RestClient(ConfigManager.AppSettings["ApiClient:ServiceUrl"]));

        public static RestClient ApiClient { get { return _lazy.Value; } }

        static ApiClientManager()
        {
            ApiClient.AddDefaultHeader("Authorization", ConfigManager.AppSettings["ApiClient:Token"]);
            ApiClient.UseSerializer(() => new JsonSerializer { DateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ" });
        }

        private ApiClientManager()
        {
        }
    }
}
