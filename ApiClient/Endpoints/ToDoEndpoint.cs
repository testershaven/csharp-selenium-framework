using TestingFramework.ApiClient.Requests;
using RestSharp;
using System.Threading.Tasks;
using Allure.Commons;
using NUnit.Allure.Core;

namespace TestingFramework.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        public static async Task<RestResponse> PostToDo(PostToDoRequest requestBody, int userId)
        {
            Task<RestResponse> t = null;

            AllureLifecycle.Instance.WrapInStep(() => {
                var request = new RestRequest($"/public/v1/users/{userId}/todos");
                request.AddJsonBody(requestBody);
                request.Method = Method.Post;

                t = ApiClientManager.ApiClient.ExecuteAsync(request);
                t.Wait();
            }, "Posting to do for user id" + userId);

            return await t;
        }
    }
}