using System;
using System.Collections.Generic;
using Machine.Specifications;
using Microsoft.Extensions.Configuration.Memory;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    [Subject(typeof(ServiceResolverBuilder))]
    public class ServiceResolverBuilderContext
    {
        protected static ServiceResolverBuilder ServiceResolverBuilder;

        Establish context = () => ServiceResolverBuilder = new ServiceResolverBuilder();
    }

    public class When_it_has_already_been_built : ServiceResolverBuilderContext
    {
        static Exception caughtException;

        Establish context = () => ServiceResolverBuilder.Build();

        Because of = () => caughtException = Catch.Exception(() => ServiceResolverBuilder.Build());

        It should_be_invalid = () => caughtException.ShouldBeOfExactType<InvalidOperationException>();
    }

    public class When_there_are_configuration_actions : ServiceResolverBuilderContext
    {
        static int configurationActionCalledTimes;

        Establish context = () =>
        {
            configurationActionCalledTimes = 0;
            ServiceResolverBuilder.Configure(builder => configurationActionCalledTimes++);
            ServiceResolverBuilder.Configure(builder => configurationActionCalledTimes++);
        };

        Because of = () => ServiceResolverBuilder.Build();

        It should_run_all_the_actions = () => configurationActionCalledTimes.ShouldEqual(2);
    }

    public class When_there_are_services_to_configure : ServiceResolverBuilderContext
    {
        static int serviceConfigurationCalledTimes;

        Establish context = () =>
        {
            serviceConfigurationCalledTimes = 0;
            ServiceResolverBuilder.ConfigureServices((config, services) => serviceConfigurationCalledTimes++);
            ServiceResolverBuilder.ConfigureServices((config, services) => serviceConfigurationCalledTimes++);
        };

        Because of = () => ServiceResolverBuilder.Build();

        It should_run_all_the_actions = () => serviceConfigurationCalledTimes.ShouldEqual(2);
    }

    public class When_poo : ServiceResolverBuilderContext
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

        It should_pass_the_configuration_to_the_service_configurations = 
            () => actualConfigurationValue.ShouldEqual(ExpectedConfigurationValue);
    }
}