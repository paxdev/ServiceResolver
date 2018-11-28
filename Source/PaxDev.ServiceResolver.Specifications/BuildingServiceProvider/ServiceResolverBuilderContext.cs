using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    [Subject(typeof(ServiceResolverBuilder))]
    public class ServiceResolverBuilderContext : WithSubject<ServiceResolverBuilder>
    {
    }
}