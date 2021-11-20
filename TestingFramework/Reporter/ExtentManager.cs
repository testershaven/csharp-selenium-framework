using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace TestingFramework.Reporter
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy =
         new(() => new ExtentReports());

        public static ExtentReports Reporter { get { return _lazy.Value; } }

        static ExtentManager()
        {
            var htmlReporter = new ExtentV3HtmlReporter(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "ExtentReports.html"));
            htmlReporter.Config.DocumentTitle = "Automation Testing Report";
            htmlReporter.Config.ReportName = "Regression Testing";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            Reporter.AttachReporter(htmlReporter);
            Reporter.AddSystemInfo("Application Under Test", "nop Commerce Demo");
            Reporter.AddSystemInfo("Environment", "QA");
            Reporter.AddSystemInfo("Machine", Environment.MachineName);
            Reporter.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        private ExtentManager()
        {
        }
    }
}
