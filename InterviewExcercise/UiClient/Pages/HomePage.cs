using InterviewExcercise.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace InterviewExcercise.UiClient.Pages
{
    class HomePage
    {
        private IWebDriver Driver;

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        IWebElement logo => Driver.FindElement(By.CssSelector("a[class='logo'] img[alt='Coingaming']:not([class='logo-for-mobile'])"));

        public void Load()
        {
            Driver.Navigate().GoToUrl(ConfigFixture.Instance["UiClient:HomePage"]);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            wait.Until(condition => logo.Displayed);
            ExtentTestManager.SetStepStatusPass("Navigated to Yolo Group home page: https://yolo.group/");
        }

        public bool IsLogoDisplayed() => logo.Displayed;
    }
}