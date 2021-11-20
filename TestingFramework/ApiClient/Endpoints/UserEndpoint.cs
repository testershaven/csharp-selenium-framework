using TestingFramework.ApiClient.Requests;
using TestingFramework.Reporter;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class UserEndpoint
    {
        public static async Task<IRestResponse> PostUser(PostUserRequest requestBody)
        {
            ReportManager.SetStepStatusPass("Posting User");
            var request = new RestRequest("/public/v1/users")
            {
                Method = Method.POST,
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer()
            };
            request.AddJsonBody(requestBody);

            Task<IRestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }

        //Not Implementing Pagination as out of scope
        public static async Task<IRestResponse> GetActiveUsers()
        {
            ReportManager.SetStepStatusPass("Getting active users");
            var request = new RestRequest("/public/v1/users?status=active")
            {
                Method = Method.GET,
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer()
            };

            Task<IRestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
