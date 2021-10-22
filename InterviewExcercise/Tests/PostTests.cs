using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using System.Linq;
using System.Net;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace InterviewExcercise
{
    public class PostTests
    {
        private readonly RestClientFixture restClient;
        private readonly ITestOutputHelper testOutputHelper;

        private static UserData postUser;

        public PostTests(ITestOutputHelper testOutputHelper)
        {
            if (restClient == null)
            {
                restClient = new RestClientFixture(testOutputHelper);
                this.testOutputHelper = testOutputHelper;
                getRandomUser();
            }

        }

        [Fact]
        public void CreatePostOnUser()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = "This is the title for a test",
                Body = "This is a test body"
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, postUser.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Title);
            postResponse.Content.Should().Contain(postUser.id.ToString());
            postResponse.Content.Should().Contain(request.Body);
        }

        [Fact]
        public void CreatePostWithoutTitle()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = null,
                Body = "This is a test body"
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, postUser.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"title\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreatePostWithoutBody()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = "This is a test title",
                Body = null
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, postUser.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"body\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreatePostWithoutUserId()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = "This is a test title",
                Body = "This is a test body"
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, -1);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private void getRandomUser()
        {
            testOutputHelper.WriteLine("Picking a random user");
            var response = restClient.UserEndpoint.GetActiveUsers();
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            postUser = users.data.Take(1).First();
        }

    }
}