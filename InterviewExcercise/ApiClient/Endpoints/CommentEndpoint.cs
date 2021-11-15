using InterviewExcercise.ApiClient.Requests;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class CommentEndpoint
    {
        private RestClient client;

        public CommentEndpoint(RestClient restClient)
        {
            client = restClient;
        }

        public IRestResponse PostComment(PostCommentRequest requestBody, int postId)
        {

            var request = new RestRequest($"/public/v1/posts/{postId}/comments");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }
    }
}
