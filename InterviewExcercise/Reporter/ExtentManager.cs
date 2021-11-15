using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;

namespace InterviewExcercise.Reporter
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> _lazy =
         new(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            var htmlReporter = new ExtentV3HtmlReporter(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "ExtentReports.html"));
            htmlReporter.Config.DocumentTitle = "Automation Testing Report";
            htmlReporter.Config.ReportName = "Regression Testing";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            Instance.AttachReporter(htmlReporter);
            Instance.AddSystemInfo("Application Under Test", "nop Commerce Demo");
            Instance.AddSystemInfo("Environment", "QA");
            Instance.AddSystemInfo("Machine", Environment.MachineName);
            Instance.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        private ExtentManager()
        {
        }
    }
}
