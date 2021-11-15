using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;

namespace InterviewExcercise.Reporter
{
    public class ExtentTestManager
    {

        private static Dictionary<string, ExtentTest> _parentTestMap = new Dictionary<string, ExtentTest>();
        private static ThreadLocal<ExtentTest> _parentTest = new ThreadLocal<ExtentTest>();
        private static ThreadLocal<ExtentTest> _childTest = new ThreadLocal<ExtentTest>();

        private static readonly object _synclock = new object();

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
                Status logstatus;

                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        break;
                    case TestStatus.Inconclusive:
                        logstatus = Status.Warning;
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        break;
                    default:
                        logstatus = Status.Pass;
                        break;
                }
                GetMethod().Log(logstatus, "Test ended with " + logstatus + stacktrace);
            }
        }
    }
}
