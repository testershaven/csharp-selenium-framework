using TestingFramework.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestingFramework.UiClient.Pages
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
            Driver.Navigate().GoToUrl(ConfigManager.AppSettings["UiClient:Pages:HomePage"]);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            wait.Until(condition => logo.Displayed);
            ReportManager.SetStepStatusPass("Navigated to Yolo Group home page: https://yolo.group/");
        }

        public bool IsLogoDisplayed() => logo.Displayed;
    }
}