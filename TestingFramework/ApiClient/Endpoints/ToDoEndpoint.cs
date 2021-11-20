using TestingFramework.ApiClient.Requests;
using TestingFramework.Reporter;
using RestSharp;
using System.Threading.Tasks;

namespace TestingFramework.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        public static async Task<IRestResponse> PostToDo(PostToDoRequest requestBody, int userId)
        {
            ReportManager.SetStepStatusPass("Posting To Do");
            var request = new RestRequest($"/public/v1/users/{userId}/todos")
            {
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer()
            };
            request.AddJsonBody(requestBody);
            request.Method = Method.POST;

            Task<IRestResponse> t = ApiClientManager.ApiClient.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
