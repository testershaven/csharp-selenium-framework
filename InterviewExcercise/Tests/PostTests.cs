using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using System.Net;
using Xunit;

namespace InterviewExcercise
{
    [Collection("Api Tests")]
    public class PostTests : IClassFixture<RestClientFixture> 
    {
        private readonly RestClientFixture restClient;

        private static PostUserResponse userResponse;

        public PostTests(RestClientFixture restClientFixture)
        {
            restClient = restClientFixture;

            userResponse = userResponse == null ? restClient.UserEndpoint.GenerateRandomUser() : userResponse;
        }

        [Fact]
        public void CreatePostOnUser()
        {
            var request = new CreatePostRequest() {
                User = userResponse.data.name,
                Title = "This is the title for a test",
                Body = "This is a test body"
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, userResponse.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Title);
            postResponse.Content.Should().Contain(userResponse.data.id.ToString());
            postResponse.Content.Should().Contain(request.Body);
        }

        [Fact]
        public void CreatePostWithoutTitle()
        {
            var request = new CreatePostRequest()
            {
                User = userResponse.data.name,
                Title = null,
                Body = "This is a test body"
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, userResponse.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"title\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreatePostWithoutBody()
        {
            var request = new CreatePostRequest()
            {
                User = userResponse.data.name,
                Title = "This is a test title",
                Body = null
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, userResponse.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"body\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreatePostWithoutUserId()
        {
            var request = new CreatePostRequest()
            {
                User = userResponse.data.name,
                Title = "This is a test title",
                Body = "This is a test body"
            };

            var postResponse = restClient.PostEndpoint.CreatePost(request, -1);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }
    }
}