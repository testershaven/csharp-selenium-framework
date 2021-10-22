using InterviewExcercise.ApiClient.Requests;
using RestSharp;
using Xunit.Abstractions;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class UserEndpoint
    {
        private RestClient client;
        private readonly ITestOutputHelper outputHelper;

        public UserEndpoint(RestClient restClient, ITestOutputHelper outputHelper)
        {
            client = restClient;
            this.outputHelper = outputHelper;
        }

        public IRestResponse PostUser(PostUserRequest requestBody)
        {
            outputHelper.WriteLine("Posting User");
            var request = new RestRequest("/public/v1/users");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }

        //Not Implementing Pagination as out of scope
        public IRestResponse GetActiveUsers()
        {
            outputHelper.WriteLine("Getting active users");
            var request = new RestRequest("/public/v1/users?status=active");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            return client.Get(request);
        }
    }
}
