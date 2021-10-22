using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class UserEndpoint
    {
        private RestClient client;
        private readonly ExtentReportsHelper extent;

        public UserEndpoint(RestClient restClient, ExtentReportsHelper outputHelper)
        {
            client = restClient;
            this.extent = outputHelper;
        }

        public IRestResponse PostUser(PostUserRequest requestBody)
        {
            extent.SetStepStatusPass("Posting User");
            var request = new RestRequest("/public/v1/users");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }

        //Not Implementing Pagination as out of scope
        public IRestResponse GetActiveUsers()
        {
            extent.SetStepStatusPass("Getting active users");
            var request = new RestRequest("/public/v1/users?status=active");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }
    }
}
