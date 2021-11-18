using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace InterviewExcercise.UiClient
{
    class DriverManager
    {
        private static ThreadLocal<IWebDriver> browser = new();

        public static IWebDriver StartDriver()
        {
            string browserType = ConfigManager.AppSettings["UiClient:Browser"];

            browser.Value = browserType switch
            {
                "Chrome" => new ChromeDriver(),
                "Firefox" => new FirefoxDriver(),
                "RemoteWebDriver" => new RemoteWebDriver(new Uri(ConfigManager.AppSettings["UiClient:RemoteUri"]), new ChromeOptions()),
                _ => throw new System.NotImplementedException(),
            };

            return browser.Value;
        }
    }
}
