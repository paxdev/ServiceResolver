using System.Collections.Generic;
using Machine.Specifications;
using Microsoft.Extensions.Configuration.Memory;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_there_is_configuration : ServiceResolverBuilderContext
    {
        protected internal const string ExpectedConfigurationKey = "expectedKey";
        protected internal const string ExpectedConfigurationValue = "expectedValue";

        static string actualConfigurationValue;

        Establish context = () =>
        {
            var configuration = new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string> {{ExpectedConfigurationKey, ExpectedConfigurationValue}}
            };

            ServiceResolverBuilder.Configure(builder => builder.Add(configuration));

            ServiceResolverBuilder.ConfigureServices
            (
                (config, services) => actualConfigurationValue = config[ExpectedConfigurationKey]
            );


        };

        Because of = () => ServiceResolverBuilder.Build();

        It should_pass_the_configuration_to_configure_services = 
            () => actualConfigurationValue.ShouldEqual(ExpectedConfigurationValue);
    }
}