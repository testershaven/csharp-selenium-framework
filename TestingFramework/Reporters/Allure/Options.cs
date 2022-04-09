using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFramework.Reporters.Allure
{
    public class Options
    {
        public string Host { get; set; }
        public bool SecurityEnabled { get; set; }
        public bool CleanupFilesAfterUpload { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Project { get; set; }
        public string ResultsFolder { get; set; }
    }
}
