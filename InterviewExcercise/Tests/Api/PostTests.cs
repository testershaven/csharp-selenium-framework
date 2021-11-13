using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using InterviewExcercise.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace InterviewExcercise
{
    public class PostTests
    {
        private RestClientFixture restClient;

        private static UserData postUser;

        [OneTimeSetUp]
        public void SetUpReporter()
        {
            restClient = new RestClientFixture();
        }
        [OneTimeTearDown]
        public void CloseAll()
        {
            ReportFixture.Instance.Close();
        }

        [TearDown]
        public void AfterTest()
        {
            ReportFixture.Instance.EndTest(TestContext.CurrentContext);
        }

        [SetUp]
        public void Setup()
        {
            ReportFixture.Instance.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName);
            if (postUser == null) getRandomUser();

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

            var postResponse = restClient.PostEndpoint.CreatePost(request, postUser.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            var postResponse = restClient.PostEndpoint.CreatePost(request, postUser.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            var postResponse = restClient.PostEndpoint.CreatePost(request, postUser.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            var postResponse = restClient.PostEndpoint.CreatePost(request, -1);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private void getRandomUser()
        {
            ReportFixture.Instance.SetStepStatusPass("Picking a random user");
            var response = restClient.UserEndpoint.GetActiveUsers();
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            postUser = users.data.Take(1).First();
        }

    }
}