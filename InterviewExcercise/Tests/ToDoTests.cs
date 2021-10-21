using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.ApiClient.Responses;
using System;
using System.Net;
using Xunit;

namespace InterviewExcercise
{
    [Collection("Api Tests")]
    public class ToDoTests : IClassFixture<RestClientFixture>
    {
        private readonly RestClientFixture restClient;

        private static PostUserResponse userResponse;

        public ToDoTests(RestClientFixture restClientFixture)
        {
            restClient = restClientFixture;

            userResponse = userResponse == null ? restClient.UserEndpoint.GenerateRandomUser() : userResponse;
        }

        [Fact]
        public void CreateToDoOnUser()
        {
            var request = new PostToDoRequest()
            {
                Title = "This is a test ToDo",
                User = userResponse.data.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = "pending"
            };

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, userResponse.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            postResponse.Content.Should().Contain(request.Title);
            postResponse.Content.Should().Contain(userResponse.data.id.ToString());
            postResponse.Content.Should().Contain(request.Status);
            postResponse.Content.Should().Contain(request.due_on);
        }

        [Fact]
        public void PostToDoWithoudTitle()
        {
            var request = new PostToDoRequest()
            {
                Title = null,
                User = userResponse.data.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = "pending"
            };

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, userResponse.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"title\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void PostToDoWithoutStatus()
        {
            var request = new PostToDoRequest()
            {
                Title = "This is a test title",
                User = userResponse.data.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = null
            };

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, userResponse.data.id);

            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"status\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void PostToDoWithoutUserId()
        {
            var request = new PostToDoRequest()
            {
                Title = "This is a test ToDo",
                User = userResponse.data.name,
                due_on = "2021-10-22T00:22:43.000+05:30",
                Status = "pending"
            };

            var postResponse = restClient.ToDoEndpoint.PostToDo(request, -1);
            postResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            postResponse.Content.Should().Contain("{\"field\":\"user\",\"message\":\"must exist\"}");
        }
    }
}
