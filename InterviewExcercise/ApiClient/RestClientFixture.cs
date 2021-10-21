using System;
using RestSharp;
using RestSharp.Serialization.Json;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class RestClientFixture : IDisposable
    {
        public UserEndpoint UserEndpoint { get; private set; }
        public PostEndpoint PostEndpoint { get; private set; }
        public CommentEndpoint CommentEndpoint { get; private set; }
        public ToDoEndpoint ToDoEndpoint { get; private set; }


        private RestClient restClient;

        public RestClientFixture()
        {
            restClient = new RestClient("https://gorest.co.in");
            restClient.AddDefaultHeader("Authorization", "Bearer 9bf5db212a21ef7b4837e826efd6b9ca594e52044a03ee0dd98ab400619fb2b8");
            restClient.UseSerializer(() => new JsonSerializer { DateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ" });

            UserEndpoint = new UserEndpoint(restClient);
            PostEndpoint = new PostEndpoint(restClient);
            CommentEndpoint = new CommentEndpoint(restClient);
            ToDoEndpoint = new ToDoEndpoint(restClient);
        }

        public void Dispose()
        {
            restClient = null;
        }
    }
}
