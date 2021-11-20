using FluentAssertions;
using TestingFramework.ApiClient.Endpoints;
using TestingFramework.ApiClient.Requests;
using TestingFramework.ApiClient.Responses;
using TestingFramework.Reporter;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace TestingFramework
{
    [Parallelizable(scope: ParallelScope.All)]
    public class PostTests
    {
        private static UserData postUser;

        [OneTimeTearDown]
        public void CloseAll()
        {
            ExtentManager.Reporter.Flush();
        }

        [TearDown]
        public void AfterTest()
        {
            ReportManager.EndTest();
        }

        [SetUp]
        public void Setup()
        {
            ReportManager.CreateTest(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
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

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private static void GetRandomUser()
        {
            ReportManager.SetStepStatusPass("Picking a random user");
            var response = UserEndpoint.GetActiveUsers().Result;
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            postUser = users.data.Take(1).First();
        }

    }
}