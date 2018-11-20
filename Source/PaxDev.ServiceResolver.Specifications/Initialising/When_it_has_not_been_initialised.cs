using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.Initialising
{
    [Subject(typeof(ServiceResolver))]
    public class When_it_has_not_been_initialised
    {
        Because of = ServiceResolver.Initialise<TestServiceProviderBuilder>;

        It should_build_the_instance = () => ServiceResolver.GetInstance().ShouldNotBeNull();
    }
}