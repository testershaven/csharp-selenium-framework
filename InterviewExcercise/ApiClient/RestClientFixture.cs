using RestSharp;
using RestSharp.Serialization.Json;
using Xunit.Abstractions;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class RestClientFixture
    {
        public UserEndpoint UserEndpoint { get; private set; }
        public PostEndpoint PostEndpoint { get; private set; }
        public CommentEndpoint CommentEndpoint { get; private set; }
        public ToDoEndpoint ToDoEndpoint { get; private set; }

        private RestClient restClient;

        public RestClientFixture(ITestOutputHelper outputHelper)
        {
            restClient = new RestClient("https://gorest.co.in");
            restClient.AddDefaultHeader("Authorization", "Bearer 9bf5db212a21ef7b4837e826efd6b9ca594e52044a03ee0dd98ab400619fb2b8");
            restClient.UseSerializer(() => new JsonSerializer { DateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ" });

            UserEndpoint = new UserEndpoint(restClient, outputHelper);
            PostEndpoint = new PostEndpoint(restClient, outputHelper);
            CommentEndpoint = new CommentEndpoint(restClient, outputHelper);
            ToDoEndpoint = new ToDoEndpoint(restClient, outputHelper);
        }
    }
}
