using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using RestSharp;
using System.Text.Json;

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
            var request = new RestRequest($"/public/v1/users/{userId}/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);

        }

        public IRestResponse GetPosts()
        {
            var request = new RestRequest($"/public/v1/posts");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }

        public CreatePostResponse GeneratePost(int userId)
        {
            var request = new CreatePostRequest()
            {
                User = "This is a test name",
                Title = "This is the title for a test",
                Body = "This is a test body"
            };

            var postResponse = CreatePost(request, userId).Content;

            return JsonSerializer.Deserialize<CreatePostResponse>(postResponse);
        }
    }
}
