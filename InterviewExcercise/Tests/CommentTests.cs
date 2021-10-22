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
    public class CommentTests
    {
        private readonly RestClientFixture restClient;
        private readonly ITestOutputHelper testOutputHelper;
        private UserData commentUser;
        private PostData post;

        public CommentTests(ITestOutputHelper testOutputHelper)
        {
            if (restClient == null)
            {
                restClient = new RestClientFixture(testOutputHelper);
                this.testOutputHelper = testOutputHelper;
                getRandomPost();
                getRandomUser();
            }

        }

        [Fact]
        public void CreateCommentOnPost()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = commentUser.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Name);
            postResponse.Content.Should().Contain(request.Email);
            postResponse.Content.Should().Contain(request.Body);
        }

        [Fact]
        public void CreateCommentWithoutName()
        {
            var request = new PostCommentRequest()
            {
                Name = null,
                Email = commentUser.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);


            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"name\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreateCommentWithoutEmail()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = null,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"email\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreateCommentWithoutBody()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = commentUser.email,
                Body = null
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"body\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreateCommentWithoutPostId()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = commentUser.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, -1);

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"post\",\"message\":\"must exist\"}");
        }

        private void getRandomUser()
        {
            testOutputHelper.WriteLine("Picking a random user");
            var response = restClient.UserEndpoint.GetActiveUsers();
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            commentUser = users.data.Take(1).First();
        }

        private void getRandomPost()
        {
            testOutputHelper.WriteLine("Picking a random post");
            var response = restClient.PostEndpoint.GetPosts();
            var users = JsonSerializer.Deserialize<GetPostsResponse>(response.Content);
            post = users.data.Take(1).First();
        }
    }
}
