using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace InterviewExcercise
{
    public class ConfigFixture
    {
        private static readonly Lazy<IConfigurationRoot> _lazy =
         new Lazy<IConfigurationRoot>(() => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build());

        public static IConfigurationRoot Instance { get { return _lazy.Value; } }

        static ConfigFixture()
        {
        }

        private ConfigFixture()
        {
        }
    }
}
