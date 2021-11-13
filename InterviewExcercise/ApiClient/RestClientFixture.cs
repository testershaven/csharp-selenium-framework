using InterviewExcercise.Reporter;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serialization.Json;
using System.IO;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class RestClientFixture
    {
        public UserEndpoint UserEndpoint { get; private set; }
        public PostEndpoint PostEndpoint { get; private set; }
        public CommentEndpoint CommentEndpoint { get; private set; }
        public ToDoEndpoint ToDoEndpoint { get; private set; }

        private RestClient restClient;

        public RestClientFixture()
        {
            restClient = new RestClient(ConfigFixture.Instance["ApiClient:ServiceUrl"]);
            restClient.AddDefaultHeader("Authorization", ConfigFixture.Instance["ApiClient:Token"]);
            restClient.UseSerializer(() => new JsonSerializer { DateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ" });

            UserEndpoint = new UserEndpoint(restClient, ReportFixture.Instance);
            PostEndpoint = new PostEndpoint(restClient, ReportFixture.Instance);
            CommentEndpoint = new CommentEndpoint(restClient, ReportFixture.Instance);
            ToDoEndpoint = new ToDoEndpoint(restClient, ReportFixture.Instance);
        }
    }
}
