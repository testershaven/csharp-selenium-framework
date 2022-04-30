using TestingFramework.ApiClient.Requests;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class PostEndpoint
    {
        public static async Task<RestResponse> CreatePost(CreatePostRequest requestBody, int userId)
        {
            var request = new RestRequest($"/public/v1/users/{userId}/posts");
            request.AddJsonBody(requestBody);
            request.Method = Method.Post;

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }

        public static async Task<RestResponse> GetPosts()
        {
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
