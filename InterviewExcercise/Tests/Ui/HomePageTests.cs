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
        [SetUp]
        public void Setup()
        {
            ReportFixture.Instance.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName);
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
