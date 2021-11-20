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
    public class ToDoTests
    {
        private static UserData toDoUser;

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
            if (toDoUser == null) GetRandomUser();
        }


        [Test]
        public void CreateToDoOnUser()
        {
            var request = new PostToDoRequest()
            {
                Title = "This is a test ToDo",
                User = toDoUser.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = "pending"
            };

            var postResponse = ToDoEndpoint.PostToDo(request, toDoUser.id).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Title);
            postResponse.Content.Should().Contain(toDoUser.id.ToString());
            postResponse.Content.Should().Contain(request.Status);
            postResponse.Content.Should().Contain(request.due_on);
        }

        [Test]
        public void PostToDoWithoudTitle()
        {
            var request = new PostToDoRequest()
            {
                Title = null,
                User = toDoUser.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = "pending"
            };

            var postResponse = ToDoEndpoint.PostToDo(request, toDoUser.id).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"title\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostToDoWithoutStatus()
        {
            var request = new PostToDoRequest()
            {
                Title = "This is a test title",
                User = toDoUser.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = null
            };

            var postResponse = ToDoEndpoint.PostToDo(request, toDoUser.id).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"status\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostToDoWithoutUserId()
        {
            var request = new PostToDoRequest()
            {
                Title = "This is a test ToDo",
                User = toDoUser.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = "pending"
            };

            var postResponse = ToDoEndpoint.PostToDo(request, -1).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private static void GetRandomUser()
        {
            ReportManager.SetStepStatusPass("Picking a random User");
            var response = UserEndpoint.GetActiveUsers().Result;
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            toDoUser = users.data.Take(1).First();
        }
    }
}
