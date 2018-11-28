using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Memory;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class TestConfiguration : MemoryConfigurationSource
    {
        public const string Key = "expectedKey";
        public const string Value = "expectedValue";

        public TestConfiguration()
        {
            InitialData = new Dictionary<string, string> {{Key, Value}};
        }
    }
}