using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class PostEndpoint
    {
        private RestClient client;

        public PostEndpoint(RestClient restClient)
        {
            client = restClient;
        }

        public IRestResponse CreatePost(CreatePostRequest requestBody, int userId)
        {
            ExtentTestManager.SetStepStatusPass("Posting Post");
            var request = new RestRequest($"/public/v1/users/{userId}/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);

        }

        public IRestResponse GetPosts()
        {
            ExtentTestManager.SetStepStatusPass("Getting list of posts");
            var request = new RestRequest($"/public/v1/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }
    }
}
