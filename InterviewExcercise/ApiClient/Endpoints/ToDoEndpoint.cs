using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        private RestClient client;
        private readonly ExtentReportsHelper outputHelper;


        public ToDoEndpoint(RestClient restClient, ExtentReportsHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse PostToDo(PostToDoRequest requestBody, int userId)
        {
            outputHelper.SetStepStatusPass("Posting To Do");
            var request = new RestRequest($"/public/v1/users/{userId}/todos");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }
    }
}
