using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using RestSharp;
using System;
using System.Text.Json;

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
            var request = new RestRequest("/public/v1/users");
            request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();
            request.AddJsonBody(requestBody);
            return client.Post(request);
        }

        public PostUserResponse GenerateRandomUser()
        {
            var randomGenerator = new Random();
            PostUserResponse postUserResponse = null;

            var request = new PostUserRequest()
            {
                Name = "ThisIsATestName" + randomGenerator.Next(1, 999),
                Email = randomGenerator.Next(1, 999) + "pepito@yoloGroup.com",
                Gender = "male",
                Status = "active"
            };

            RestResponse response = (RestResponse)PostUser(request);

            return JsonSerializer.Deserialize<PostUserResponse>(response.Content);
        }
    }
}
