using InterviewExcercise.Reporter;
using InterviewExcercise.UiClient.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace InterviewExcercise.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class HomePageTests
    {

        [SetUp]
        public void Setup()
        {
            ExtentTestManager.CreateMethod(TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
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
