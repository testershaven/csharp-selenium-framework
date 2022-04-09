using TestingFramework.ApiClient.Requests;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class UserEndpoint
    {
        public static async Task<RestResponse> PostUser(PostUserRequest requestBody)
        {
            var request = new RestRequest("/public/v1/users")
            {
                Method = Method.Post,
            };
            request.AddJsonBody(requestBody);

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }

        //Not Implementing Pagination as out of scope
        public static async Task<RestResponse> GetActiveUsers()
        {
            var request = new RestRequest("/public/v1/users?status=active")
            {
                Method = Method.Get,
            };

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
