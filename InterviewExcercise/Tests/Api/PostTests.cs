using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using InterviewExcercise.Reporter;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace InterviewExcercise
{
    [Parallelizable(scope: ParallelScope.All)]
    public class PostTests
    {
        private static UserData postUser;

        [OneTimeTearDown]
        public void CloseAll()
        {
            ExtentManager.Instance.Flush();
        }

        [TearDown]
        public void AfterTest()
        {
            ExtentTestManager.EndTest();
        }

        [SetUp]
        public void Setup()
        {
            ExtentTestManager.CreateMethod(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
            if (postUser == null) GetRandomUser();
        }

        [Test]
        public void CreatePostOnUser()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = "This is the title for a test",
                Body = "This is a test body"
            };

            var postResponse = PostEndpoint.CreatePost(request, postUser.id).Result;

            ExtentTestManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ExtentTestManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Title);
            postResponse.Content.Should().Contain(postUser.id.ToString());
            postResponse.Content.Should().Contain(request.Body);
        }

        [Test]
        public void CreatePostWithoutTitle()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = null,
                Body = "This is a test body"
            };

            var postResponse = PostEndpoint.CreatePost(request, postUser.id).Result;

            ExtentTestManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ExtentTestManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"title\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void CreatePostWithoutBody()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = "This is a test title",
                Body = null
            };

            var postResponse = PostEndpoint.CreatePost(request, postUser.id).Result;

            ExtentTestManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ExtentTestManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"body\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void CreatePostWithoutUserId()
        {
            var request = new CreatePostRequest()
            {
                User = postUser.name,
                Title = "This is a test title",
                Body = "This is a test body"
            };

            var postResponse = PostEndpoint.CreatePost(request, -1).Result;

            ExtentTestManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ExtentTestManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private static void GetRandomUser()
        {
            ExtentTestManager.SetStepStatusPass("Picking a random user");
            var response = UserEndpoint.GetActiveUsers().Result;
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            postUser = users.data.Take(1).First();
        }

    }
}