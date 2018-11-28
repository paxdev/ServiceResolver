using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_there_are_services_to_configure : ServiceResolverBuilderContext
    {
        static int ServiceConfigurationCalledTimes;

        Establish context = () =>
        {
            ServiceConfigurationCalledTimes = 0;
            Subject.ConfigureServices((config, services) => ServiceConfigurationCalledTimes++);
            Subject.ConfigureServices((config, services) => ServiceConfigurationCalledTimes++);
        };

        Because of = () => Subject.Build();

        It should_run_all_the_actions = () => ServiceConfigurationCalledTimes.ShouldEqual(2);
    }
}