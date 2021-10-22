using InterviewExcercise.ApiClient.Requests;
using RestSharp;
using Xunit.Abstractions;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        private RestClient client;
        private readonly ITestOutputHelper outputHelper;


        public ToDoEndpoint(RestClient restClient, ITestOutputHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse PostToDo(PostToDoRequest requestBody, int userId)
        {
            outputHelper.WriteLine("Posting To Do");
            var request = new RestRequest($"/public/v1/users/{userId}/todos");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }
    }
}
