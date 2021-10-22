using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class CommentEndpoint
    {
        private RestClient client;
        private readonly ExtentReportsHelper outputHelper;

        public CommentEndpoint(RestClient restClient, ExtentReportsHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse PostComment(PostCommentRequest requestBody, int postId)
        {
            outputHelper.SetStepStatusPass("Posting Comment");
            var request = new RestRequest($"/public/v1/posts/{postId}/comments");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }
    }
}
