using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_there_is_configuration : ServiceResolverBuilderContext
    {
        static string ActualConfigurationValue;

        Establish context = () =>
        {
            var configuration = new TestConfiguration();

            Subject.Configure(builder => builder.Add(configuration));

            Subject.ConfigureServices
            (
                (config, services) => ActualConfigurationValue = config[TestConfiguration.Key]
            );
        };
        
        Because of = () => Subject.Build();

        It should_pass_the_configuration_to_configure_services = 
            () => ActualConfigurationValue.ShouldEqual(TestConfiguration.Value);
    }
}