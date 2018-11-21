using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
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
}