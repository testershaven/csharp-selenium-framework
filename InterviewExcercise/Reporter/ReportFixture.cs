using System;

namespace InterviewExcercise.Reporter
{
    public class ReportFixture
    {
        private static readonly Lazy<ExtentReportsHelper> _lazy =
         new Lazy<ExtentReportsHelper>(() => new ExtentReportsHelper());

        public static ExtentReportsHelper Instance { get { return _lazy.Value; } }

        static ReportFixture()
        {
        }

        private ReportFixture()
        {
        }
    }
}
