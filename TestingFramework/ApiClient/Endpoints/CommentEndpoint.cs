using InterviewExcercise.ApiClient.Requests;
using RestSharp;
using System.Threading.Tasks;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class CommentEndpoint
    {
        public static async Task<IRestResponse> PostComment(PostCommentRequest requestBody, int postId)
        {
            var request = new RestRequest($"/public/v1/posts/{postId}/comments")
            {
                Method = Method.POST,
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer(),
            };
            request.AddJsonBody(requestBody);

            Task<IRestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
