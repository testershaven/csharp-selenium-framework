using System;

namespace TestingFramework.Reporters.Allure
{
    public static class Worker
    {
        public static void UploadResults(Options options)
        {
            CheckOptions(options);

            var request = GenerateRequest(options.ResultsFolder);

            var allureClient = new Client(options.Host);

            if (options.SecurityEnabled)
            {
                allureClient.Login(options.Username, options.Password);
            }

            allureClient.CleanResults(options.Project);
            allureClient.SendResults(request, options.Project);
            var report = allureClient.GenerateReport(options.Project);

            Console.WriteLine(report.meta_data.message);
            Console.WriteLine(report.data.report_url);

            if (options.CleanupFilesAfterUpload)
            {
                CleanAllureFolder(options.ResultsFolder);
            }
        }

        private static void CheckOptions(Options options)
        {

        }

        private static string GenerateRequest(string resultsFolder)
        {
            return "";
        }

        private static void CleanAllureFolder(string resultsFolder)
        {

        }
    }
}
