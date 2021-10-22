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
    public class ToDoTests
    {
        private readonly RestClientFixture restClient;
        private readonly ITestOutputHelper testOutputHelper;

        private static UserData toDoUser;

        public ToDoTests(ITestOutputHelper testOutputHelper)
        {
            if (restClient == null)
            {
                restClient = new RestClientFixture(testOutputHelper);
                this.testOutputHelper = testOutputHelper;
                getRandomUser();
            }

        }

        [Fact]
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

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Title);
            postResponse.Content.Should().Contain(toDoUser.id.ToString());
            postResponse.Content.Should().Contain(request.Status);
            postResponse.Content.Should().Contain(request.due_on);
        }

        [Fact]
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

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"title\",\"message\":\"can't be blank\"}");
        }

        [Fact]
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

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"status\",\"message\":\"can't be blank\"}");
        }

        [Fact]
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

            testOutputHelper.WriteLine("Response Code is: " + postResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + postResponse.Content);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }

        private void getRandomUser()
        {
            testOutputHelper.WriteLine("Picking a random User");
            var response = restClient.UserEndpoint.GetActiveUsers();
            var users = JsonSerializer.Deserialize<GetUsersResponse>(response.Content);
            toDoUser = users.data.Take(1).First();
        }
    }
}
