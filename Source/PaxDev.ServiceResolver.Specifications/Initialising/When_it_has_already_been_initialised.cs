using System;
using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.Initialising
{
    [Subject(typeof(ServiceResolver))]
    public class When_it_has_already_been_initialised: WithFakes
    {
        static Exception CaughtException;

        Establish context = ServiceResolver.Initialise<TestServiceProviderBuilder>;

        Because of = () => CaughtException = Catch.Exception(ServiceResolver.Initialise<TestServiceProviderBuilder>);

        It should_throw = () => CaughtException.ShouldBeOfExactType<ResolverAlreadyInstantatedException>();
    }
}