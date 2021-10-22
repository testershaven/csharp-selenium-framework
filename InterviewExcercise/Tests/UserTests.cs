using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using System;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace InterviewExcercise
{
    public class UserTests
    {
        private readonly RestClientFixture restClient;
        private readonly ITestOutputHelper testOutputHelper;

        public UserTests(ITestOutputHelper testOutputHelper)
        {
            if (restClient == null)
            {
                restClient = new RestClientFixture(testOutputHelper);
                this.testOutputHelper = testOutputHelper;
            }
        }

        [Fact]
        public void CreateUserSuccesfully()
        {
            var postUserRequest = GeneratePostUserRequest();

            var userCreationResponse = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + userCreationResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + userCreationResponse.Content);
            userCreationResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public void CreateUserWithExistingEmail()
        {
            var firstUser = GeneratePostUserRequest();

            restClient.UserEndpoint.PostUser(firstUser);

            var secondUser = GeneratePostUserRequest();
            secondUser.Email = firstUser.Email;

            var secondUserResponse = restClient.UserEndpoint
                                        .PostUser(secondUser);

            testOutputHelper.WriteLine("Response Code is: " + secondUserResponse.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + secondUserResponse.Content);

            secondUserResponse.StatusCode
                .Should().Be(HttpStatusCode.UnprocessableEntity);
            secondUserResponse.Content
                .Should().Contain("{\"field\":\"email\",\"message\":\"has already been taken\"}");
        }

        [Fact]
        public void PostUserWithoutName()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Name = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"name\",\"message\":\"can't be blank\"}");

        }

        [Fact]
        public void PostUserWithoutEmail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void PostUserWithoutGender()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Gender = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"gender\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void PostUserWithoutStatus()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Status = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"status\",\"message\":\"can't be blank\"}");
        }

        [Fact]
        public void PostUserWithoutUsernameInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "@yoloGroup.com";

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        [Fact]
        public void PostUserWithoutDomainInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "pepito@.com";

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        [Fact]
        public void PostUserWithoutAtInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "pepitoyologroup.com";

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            testOutputHelper.WriteLine("Response Code is: " + response.StatusCode);
            testOutputHelper.WriteLine("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        public PostUserRequest GeneratePostUserRequest()
        {
            testOutputHelper.WriteLine("Generating Post User Request");
            var randomGenerator = new Random();

            return new PostUserRequest()
            {
                Name = "ThisIsATestName" + randomGenerator.Next(1, 999),
                Email = randomGenerator.Next(1, 999) + "pepito@yoloGroup.com",
                Gender = "male",
                Status = "active"
            };
        }
    }
}