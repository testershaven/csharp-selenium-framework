using TestingFramework.ApiClient.Requests;
using TestingFramework.Reporter;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class PostEndpoint
    {
        public static async Task<RestResponse> CreatePost(CreatePostRequest requestBody, int userId)
        {
            ReportManager.SetStepStatusPass("Posting Post");
            var request = new RestRequest($"/public/v1/users/{userId}/posts");
            request.AddJsonBody(requestBody);
            request.Method = Method.Post;

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }

        public static async Task<RestResponse> GetPosts()
        {
            ReportManager.SetStepStatusPass("Getting list of posts");
            var request = new RestRequest($"/public/v1/posts")
            {
                Method = Method.Get
            };

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
