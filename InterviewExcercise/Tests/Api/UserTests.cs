using FluentAssertions;
using InterviewExcercise.ApiClient.Endpoints;
using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Net;

namespace InterviewExcercise
{
    public class UserTests
    {
        private RestClientFixture restClient;

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

        [SetUp]
        public void Setup()
        {
            ReportFixture.Instance.CreateTest(TestContext.CurrentContext.Test.Name);
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

        [Test]
        public void CreateUserSuccesfully()
        {
            var postUserRequest = GeneratePostUserRequest();

            var userCreationResponse = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + userCreationResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + userCreationResponse.Content);
            userCreationResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void CreateUserWithExistingEmail()
        {
            var firstUser = GeneratePostUserRequest();

            restClient.UserEndpoint.PostUser(firstUser);

            var secondUser = GeneratePostUserRequest();
            secondUser.Email = firstUser.Email;

            var secondUserResponse = restClient.UserEndpoint
                                        .PostUser(secondUser);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + secondUserResponse.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + secondUserResponse.Content);

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

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"name\",\"message\":\"can't be blank\"}");

        }

        [Test]
        public void PostUserWithoutEmail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostUserWithoutGender()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Gender = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"gender\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostUserWithoutStatus()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Status = null;

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"status\",\"message\":\"can't be blank\"}");
        }

        [Test]
        public void PostUserWithoutUsernameInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "@yoloGroup.com";

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        [Test]
        public void PostUserWithoutDomainInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "pepito@.com";

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        [Test]
        public void PostUserWithoutAtInMail()
        {
            var postUserRequest = GeneratePostUserRequest();
            postUserRequest.Email = "pepitoyologroup.com";

            var response = restClient.UserEndpoint
                                            .PostUser(postUserRequest);

            ReportFixture.Instance.SetStepStatusPass("Response Code is: " + response.StatusCode);
            ReportFixture.Instance.SetStepStatusPass("Response Content is: " + response.Content);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            response.Content
                    .Should().Contain("{\"field\":\"email\",\"message\":\"is invalid\"}");
        }

        public PostUserRequest GeneratePostUserRequest()
        {
            ReportFixture.Instance.SetStepStatusPass("Generating Post User Request");
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