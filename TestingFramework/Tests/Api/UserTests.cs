using FluentAssertions;
using TestingFramework.ApiClient.Endpoints;
using TestingFramework.ApiClient.Requests;
using TestingFramework.Reporter;
using NUnit.Framework;
using System;
using System.Net;

namespace TestingFramework
{
    [Parallelizable(scope: ParallelScope.All)]
    public class UserTests
    {
        [OneTimeTearDown]
        public void CloseAll()
        {
            ExtentManager.Reporter.Flush();
        }

        [SetUp]
        public void Setup()
        {
            ReportManager.CreateTest(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            ReportManager.EndTest();
        }

        [Test]
        public void CreateUserSuccesfully()
        {
            var postUserRequest = GeneratePostUserRequest();

            var userCreationResponse = UserEndpoint
                                            .PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + userCreationResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + userCreationResponse.Content);
            userCreationResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void CreateUserWithExistingEmail()
        {
            var firstUser = GeneratePostUserRequest();

            UserEndpoint.PostUser(firstUser).Wait();

            var secondUser = GeneratePostUserRequest();
            secondUser.Email = firstUser.Email;

            var secondUserResponse = UserEndpoint
                                        .PostUser(secondUser).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + secondUserResponse.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + secondUserResponse.Content);

            secondUserResponse.StatusCode
                .Should().Be(HttpStatusCode.UnprocessableEntity);
            secondUserResponse.Content
                .Should().Contain("{\"field\":\"email\",\"message\":\"has already been taken\"}");
        }

        [Test]
        public void PostUserWithoutName()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Name = null;

            var response = UserEndpoint
                                            .PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"name\",\"message\":\"can't be blank\"}");

        }

        [Test]
        public void PostUserWithoutEmail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = null;

            var response = UserEndpoint
                                            .PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostUserWithoutGender()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Gender = null;

            var response = UserEndpoint.PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"gender\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostUserWithoutStatus()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Status = null;

            var response = UserEndpoint.PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"status\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostUserWithoutUsernameInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "@yoloGroup.com";

            var response = UserEndpoint.PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        [Test]
        public void PostUserWithoutDomainInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "pepito@.com";

            var response = UserEndpoint.PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        [Test]
        public void PostUserWithoutAtInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "pepitoyologroup.com";

            var response = UserEndpoint.PostUser(postUserRequest).Result;

            ReportManager.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportManager.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        public static PostUserRequest GeneratePostUserRequest()
        {
            ReportManager.SetStepStatusPass("Generating Post User Request");
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