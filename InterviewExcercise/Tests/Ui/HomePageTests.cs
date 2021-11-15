using InterviewExcercise.Reporter;
using InterviewExcercise.UiClient.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace InterviewExcercise.Tests
{
    public class HomePageTests
    {

        [OneTimeSetUp]
        public void SetUpReporter()
        {
            ExtentTestManager.CreateParentTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void Setup()
        {
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName);
        }

        [OneTimeTearDown]
        public void CloseAll()
        {
            ExtentManager.Instance.Flush();
        }

        [TearDown]
        public void AfterTest()
        {
            ExtentTestManager.EndTest();
        }

        [Test]
        public void VerifyElementsInLandingPage()
        {
            using (var driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
                ExtentTestManager.SetStepStatusPass("Logo is correctly displayed");
            }
        }
    }
}
