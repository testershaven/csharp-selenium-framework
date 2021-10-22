using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using System;
using System.Net;
using Xunit;

namespace InterviewExcercise
{
    public class UserTests : IClassFixture<RestClientFixture>
    {
        private readonly RestClientFixture restClient;

        public UserTests(RestClientFixture restClientFixture)
        {
            restClient = restClientFixture;
        }

        [Fact]
        public void CreateUserSuccesfully()
        {
            var postUserRequest = GeneratePostUserRequest();

            var userCreationResponse = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

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

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        public PostUserRequest GeneratePostUserRequest()
        {
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