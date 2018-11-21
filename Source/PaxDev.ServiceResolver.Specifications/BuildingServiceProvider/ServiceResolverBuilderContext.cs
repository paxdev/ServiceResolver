using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    [Subject(typeof(ServiceResolverBuilder))]
    public class ServiceResolverBuilderContext
    {
        protected static ServiceResolverBuilder ServiceResolverBuilder;

        Establish context = () => ServiceResolverBuilder = new ServiceResolverBuilder();
    }
}