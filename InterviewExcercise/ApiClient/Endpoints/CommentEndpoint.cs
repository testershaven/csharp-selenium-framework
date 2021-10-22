using InterviewExcercise.ApiClient.Requests;
using RestSharp;
using Xunit.Abstractions;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class CommentEndpoint
    {
        private RestClient client;
        private readonly ITestOutputHelper outputHelper;

        public CommentEndpoint(RestClient restClient, ITestOutputHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse PostComment(PostCommentRequest requestBody, int postId)
        {
            outputHelper.WriteLine("Posting Comment");
            var request = new RestRequest($"/public/v1/posts/{postId}/comments");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }
    }
}
