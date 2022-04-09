using TestingFramework.ApiClient.Requests;
using TestingFramework.Reporter;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        public static async Task<RestResponse> PostToDo(PostToDoRequest requestBody, int userId)
        {
            ReportManager.SetStepStatusPass("Posting To Do");
            var request = new RestRequest($"/public/v1/users/{userId}/todos");
            request.AddJsonBody(requestBody);
            request.Method = Method.Post;

            Task<RestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
