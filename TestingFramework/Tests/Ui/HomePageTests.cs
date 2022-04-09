using TestingFramework.UiClient;
using TestingFramework.UiClient.Pages;
using NUnit.Framework;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.Commons;

namespace TestingFramework.Tests
{
    [Parallelizable(scope: ParallelScope.All)]
    [AllureNUnit]
    [AllureSuite("Home Page Tests")]
    [AllureDisplayIgnored]
    public class HomePageTests
    {

        [Test(Description = "VerifyElementsInLandingPage")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Saetabis")]
        public void VerifyElementsInLandingPage()
        {
            using (var driver = DriverManager.StartDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
            }
        }

        [Test(Description = "VerifyElementsInLandingPage2")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Saetabis")]
        public void VerifyElementsInLandingPage2()
        {
            using (var driver = DriverManager.StartDriver())
            {
                var homePage = new HomePage(driver);
                homePage.Load();

                Assert.True(homePage.IsLogoDisplayed());
            }
        }

        [Test(Description = "VerifyElementsInLandingPage3")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureOwner("Saetabis")]
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
