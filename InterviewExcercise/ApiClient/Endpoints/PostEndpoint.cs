using InterviewExcercise.ApiClient.Requests;
using RestSharp;
using Xunit.Abstractions;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class PostEndpoint
    {
        private RestClient client;
        private readonly ITestOutputHelper outputHelper;

        public PostEndpoint(RestClient restClient, ITestOutputHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse CreatePost(CreatePostRequest requestBody, int userId)
        {
            outputHelper.WriteLine("Posting Post");
            var request = new RestRequest($"/public/v1/users/{userId}/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);

        }

        public IRestResponse GetPosts()
        {
            outputHelper.WriteLine("Getting list of posts");
            var request = new RestRequest($"/public/v1/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }
    }
}
