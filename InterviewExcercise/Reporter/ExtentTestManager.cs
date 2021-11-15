using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Threading;

namespace InterviewExcercise.Reporter
{
    public class ExtentTestManager
    {

        private static ThreadLocal<ExtentTest> _childTest = new();

        private static readonly object _synclock = new();

        public static ExtentTest CreateMethod(string parentName, string testName, string description = null)
        {
            lock (_synclock)
            {
                _childTest.Value = ExtentManager.Instance.CreateTest(testName, description);
                _childTest.Value.AssignCategory(parentName);

                return _childTest.Value;
            }
        }
        public static ExtentTest GetMethod()
        {
            lock (_synclock)
            {
                return _childTest.Value;
            }
        }

        public static void SetStepStatusPass(string stepDescription)
        {
            lock (_synclock)
            {
                Console.WriteLine(stepDescription);
                _childTest.Value.Log(Status.Pass, stepDescription);
            }
        }

        public static void EndTest()
        {
            lock (_synclock)
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                        ? ""
                        : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);

                var logstatus = status switch
                {
                    TestStatus.Failed => Status.Fail,
                    TestStatus.Inconclusive => Status.Warning,
                    TestStatus.Skipped => Status.Skip,
                    _ => Status.Pass,
                };

                GetMethod().Log(logstatus, "Test ended with " + logstatus + stacktrace);
            }
        }
    }
}
