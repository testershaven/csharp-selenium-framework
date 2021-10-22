using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using InterviewExcercise.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace InterviewExcercise
{
    public class CommentTests
    {
        private RestClientFixture restClient;
        private UserData commentUser;
        private PostData post;

        [OneTimeSetUp]
        public void SetUpReporter()
        {
            restClient = new RestClientFixture(ReportFixture.Instance);
        }
        [OneTimeTearDown]
        public void CloseAll()
        {
            ReportFixture.Instance.Close();
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;
            var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
            switch (status)
            {
                case TestStatus.Failed:
                    ReportFixture.Instance.SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>");
                    break;
                case TestStatus.Skipped:
                    ReportFixture.Instance.SetTestStatusSkipped();
                    break;
                default:
                    ReportFixture.Instance.SetTestStatusPass();
                    break;
            }
        }

        [SetUp]
        public void Setup()
        {
            ReportFixture.Instance.CreateTest(TestContext.CurrentContext.Test.Name);
            if (commentUser == null) getRandomUser();
            if (post == null) getRandomPost();
        }

        [Test]
        public void CreateCommentOnPost()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = commentUser.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Name);
            postResponse.Content.Should().Contain(request.Email);
            postResponse.Content.Should().Contain(request.Body);
        }

        [Test]
        public void CreateCommentWithoutName()
        {
            var request = new PostCommentRequest()
            {
                Name = null,
                Email = commentUser.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);


            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"name\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void CreateCommentWithoutEmail()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = null,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"email\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void CreateCommentWithoutBody()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = commentUser.email,
                Body = null
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, post.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"body\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void CreateCommentWithoutPostId()
        {
            var request = new PostCommentRequest()
            {
                Name = commentUser.name,
                Email = commentUser.email,
                Body = "This is a test body"
            };

            var postResponse = restClient.CommentEndpoint.PostComment(request, -1);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"post\",\"message\":\"must exist\"}");
        }

        private void getRandomUser()
        {
            ReportFixture.Instance.SetStepStatusPass("Picking a random user");
            var response = restClient.UserEndpoint.GetActiveUsers();
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            commentUser = users.data.Take(1).First();
        }

        private void getRandomPost()
        {
            ReportFixture.Instance.SetStepStatusPass("Picking a random post");
            var response = restClient.PostEndpoint.GetPosts();
            var users = JsonSerializer.Deserialize<GetPostsResponse>(response.Content);
            post = users.data.Take(1).First();
        }
    }
}
