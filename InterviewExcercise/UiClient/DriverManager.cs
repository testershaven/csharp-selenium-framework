using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace InterviewExcercise.UiClient
{
    class DriverManager
    {
        private static ThreadLocal<IWebDriver> browser = new();

        public static IWebDriver StartDriver()
        {
            string browserType = ConfigFixture.Instance["UiClient:Browser"];

            browser.Value = browserType switch
            {
                "Chrome" => new ChromeDriver(),
                "Firefox" => throw new System.NotImplementedException(),
                "SeleniumGrid" => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException(),
            };

            return browser.Value;
        }
    }
}
