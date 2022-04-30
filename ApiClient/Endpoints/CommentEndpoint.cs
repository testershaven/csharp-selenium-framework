using TestingFramework.ApiClient.Requests;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class CommentEndpoint
    {
        public static async Task<RestResponse> PostComment(PostCommentRequest requestBody, int postId)
        {
            var request = new RestRequest($"/public/v1/posts/{postId}/comments")
            {
                Method = Method.Post,
            };
            request.AddJsonBody(requestBody);

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
