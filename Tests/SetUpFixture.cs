using NUnit.Framework;
using System;
using TestingFramework.Reporters.Allure;

namespace TestingFramework.Tests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Console.WriteLine("AFTER TESTS WORKING");
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
}
