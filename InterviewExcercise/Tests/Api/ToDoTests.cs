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
    public class ToDoTests
    {
        private RestClientFixture restClient;

        private static UserData toDoUser;

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
            if (toDoUser == null) getRandomUser();
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

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, toDoUser.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, toDoUser.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, toDoUser.id);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

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

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, -1);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + postResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private void getRandomUser()
        {
            ReportFixture.Instance.SetStepStatusPass("Picking a random User");
            var response = restClient.UserEndpoint.GetActiveUsers();
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            toDoUser = users.data.Take(1).First();
        }
    }
}
