using System;
using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.Initialising
{
    public class When_it_has_already_been_initialised: WithFakes
    {
        static Exception CaughtException;

        Establish context = ServiceResolver.Initialise<ServiceResolverTester>;

        Because of = () => CaughtException = Catch.Exception(ServiceResolver.Initialise<ServiceResolverTester>);

        It should_throw = () => CaughtException.ShouldBeOfExactType<ResolverAlreadyInstantatedException>();
    }

    public class When_it_has_not_been_initialised
    {

    }

    public class ServiceResolverTester: WithFakes, IServiceProviderBuilder
    {
        public IServiceProvider Build()
        {
            return An<IServiceProvider>();
        }
    }
}