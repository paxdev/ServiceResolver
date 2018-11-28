using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_there_are_configuration_actions : ServiceResolverBuilderContext
    {
        static int ConfigurationActionCalledTimes;

        Establish context = () =>
        {
            ConfigurationActionCalledTimes = 0;
            Subject.Configure(builder => ConfigurationActionCalledTimes++);
            Subject.Configure(builder => ConfigurationActionCalledTimes++);
        };

        Because of = () => Subject.Build();

        It should_run_all_the_actions = () => ConfigurationActionCalledTimes.ShouldEqual(2);
    }
}