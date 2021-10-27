using InterviewExcercise.Reporter;
using InterviewExcercise.UiClient.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace InterviewExcercise.Tests
{
    public class HomePageTests
    {
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
        }

        [Test]
        public void VerifyElementsInLandingPage()
        {
            using (var driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
                ReportFixture.Instance.SetStepStatusPass("Logo is correctly displayed");
            }
        }
    }
}
