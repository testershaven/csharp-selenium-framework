using InterviewExcercise.ApiClient.Requests;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class ToDoEndpoint
    {
        private RestClient client;

        public ToDoEndpoint(RestClient restClient)
        {
            client = restClient;
        }

        public IRestResponse PostToDo(PostToDoRequest requestBody, int userId)
        {
            var request = new RestRequest($"/public/v1/users/{userId}/todos");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }
    }
}
