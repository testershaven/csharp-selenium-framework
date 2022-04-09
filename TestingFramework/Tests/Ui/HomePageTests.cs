using TestingFramework.UiClient;
using TestingFramework.UiClient.Pages;
using NUnit.Framework;

namespace TestingFramework.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class HomePageTests
    {

        [Test]
        public void VerifyElementsInLandingPage()
        {
            using (var driver = DriverManager.StartDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
            }
        }
        [Test]
        public void VerifyElementsInLandingPage2()
        {
            using (var driver = DriverManager.StartDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
            }
        }

        [Test]
        public void VerifyElementsInLandingPage3()
        {
            using (var driver = DriverManager.StartDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
            }
        }
    }
}
