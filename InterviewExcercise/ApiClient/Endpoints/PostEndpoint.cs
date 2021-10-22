using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class PostEndpoint
    {
        private RestClient client;
        private readonly ExtentReportsHelper outputHelper;

        public PostEndpoint(RestClient restClient, ExtentReportsHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse CreatePost(CreatePostRequest requestBody, int userId)
        {
            outputHelper.SetStepStatusPass("Posting Post");
            var request = new RestRequest($"/public/v1/users/{userId}/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);

        }

        public IRestResponse GetPosts()
        {
            outputHelper.SetStepStatusPass("Getting list of posts");
            var request = new RestRequest($"/public/v1/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }
    }
}
