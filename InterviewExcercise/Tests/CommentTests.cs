using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using System.Net;
using Xunit;

namespace InterviewExcercise
{
    [Collection("Api Tests")]
    public class CommentTests : IClassFixture<RestClientFixture>
    {
        private readonly RestClientFixture restClient;

        private static PostUserResponse postUser;
        private static PostUserResponse commentUser;
        private static CreatePostResponse postInfo;

        public CommentTests(RestClientFixture restClientFixture)
        {
            restClient = restClientFixture;

            postUser = postUser == null ? restClient.UserEndpoint.GenerateRandomUser() : postUser;
            postInfo = postInfo == null ? restClient.PostEndpoint.GeneratePost(postUser.data.id) : postInfo;
            commentUser = commentUser == null ? restClient.UserEndpoint.GenerateRandomUser() : commentUser;
        }

        [Fact]
        public void CreateCommentOnPost()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.data.name,
                Email = commentUser.data.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, postInfo.data.id);

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
                Email = commentUser.data.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, postInfo.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"name\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreateCommentWithoutEmail()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.data.name,
                Email = null,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, postInfo.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"email\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreateCommentWithoutBody()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.data.name,
                Email = commentUser.data.email,
                Body = null
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, postInfo.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"body\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void CreateCommentWithoutPostId()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.data.name,
                Email = commentUser.data.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, -1);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"post\",\"message\":\"must exist\"}");
        }
    }
}
