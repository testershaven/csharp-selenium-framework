using RestSharp;
using RestSharp.Serialization.Json;
using System;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class RestClientFixture
    {

        private static readonly Lazy<RestClient> _lazy =
         new(() => new RestClient(ConfigFixture.Instance["ApiClient:ServiceUrl"]));

        public static RestClient Instance { get { return _lazy.Value; } }

        static RestClientFixture()
        {
            Instance.AddDefaultHeader("Authorization", ConfigFixture.Instance["ApiClient:Token"]);
            Instance.UseSerializer(() => new JsonSerializer { DateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ" });
        }

        private RestClientFixture()
        {
        }
    }
}
