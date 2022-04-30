using NUnit.Framework;
using System;
using TestingFramework;
using TestingFramework.Reporters.Allure;

[SetUpFixture]
public class TestSuiteInitializer
{
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Console.WriteLine("Running One time setup in TestSuiteInitializer.");
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        Console.WriteLine("Running One time tear down in TestSuiteInitializer.");
        if(Convert.ToBoolean(ConfigManager.AppSettings["Reports:Allure"]))
        {
            var options = new Options()
            {
                CleanupFilesAfterUpload = true
            };

            Worker.UploadResults(options);
        }
    }
}
