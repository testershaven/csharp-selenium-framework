using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class UserEndpoint
    {
        private RestClient client;

        public UserEndpoint(RestClient restClient)
        {
            client = restClient;
        }

        public IRestResponse PostUser(PostUserRequest requestBody)
        {
            ExtentTestManager.SetStepStatusPass("Posting User");
            var request = new RestRequest("/public/v1/users");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }

        //Not Implementing Pagination as out of scope
        public IRestResponse GetActiveUsers()
        {
            ExtentTestManager.SetStepStatusPass("Getting active users");
            var request = new RestRequest("/public/v1/users?status=active");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }
    }
}
