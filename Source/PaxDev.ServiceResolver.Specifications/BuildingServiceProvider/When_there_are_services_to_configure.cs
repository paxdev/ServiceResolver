using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
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
}