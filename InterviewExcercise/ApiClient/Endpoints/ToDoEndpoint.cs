using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;
using System.Threading.Tasks;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        public static async Task<IRestResponse> PostToDo(PostToDoRequest requestBody, int userId)
        {
            ExtentTestManager.SetStepStatusPass("Posting To Do");
            var request = new RestRequest($"/public/v1/users/{userId}/todos")
            {
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer()
            };
            request.AddJsonBody(requestBody);
            request.Method = Method.POST;

            Task<IRestResponse> t = RestClientFixture.Instance.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
