using System;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_it_has_already_been_built : ServiceResolverBuilderContext
    {
        static Exception caughtException;

        Establish context = () => ServiceResolverBuilder.Build();

        Because of = () => caughtException = Catch.Exception(() => ServiceResolverBuilder.Build());

        It should_be_invalid = () => caughtException.ShouldBeOfExactType<InvalidOperationException>();
    }
}