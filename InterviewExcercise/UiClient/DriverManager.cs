using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
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

            if (bool.Parse(ConfigManager.AppSettings["UiClient:IsRemote"]))
            {
                browser.Value = browserType switch
                {
                    "Chrome" => new RemoteWebDriver(new Uri(ConfigManager.AppSettings["UiClient:RemoteUri"]), new ChromeOptions()),
                    "Firefox" => new RemoteWebDriver(new Uri(ConfigManager.AppSettings["UiClient:RemoteUri"]), new FirefoxOptions()),
                    "Edge" => new RemoteWebDriver(new Uri(ConfigManager.AppSettings["UiClient:RemoteUri"]), new EdgeOptions()),
                    _ => throw new System.NotImplementedException(),
                };
            } else
            {
                browser.Value = browserType switch
                {
                    "Chrome" => new ChromeDriver(),
                    "Firefox" => new FirefoxDriver(),
                    "Edge" => new EdgeDriver(),
                    _ => throw new System.NotImplementedException(),
                };
            }

            return browser.Value;
        }
    }
}
