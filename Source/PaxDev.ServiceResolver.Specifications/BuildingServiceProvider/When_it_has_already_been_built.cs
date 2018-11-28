using System;
using System.Threading.Tasks;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Extensions.Configuration.Memory;
using PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_it_has_already_been_built : ServiceResolverBuilderContext
    {
        static Exception CaughtException;

        Establish context = () => Subject.Build();

        Because of = () => CaughtException = Catch.Exception(() => Subject.Build());

        It should_be_invalid = () => CaughtException.ShouldBeOfExactType<InvalidOperationException>();
    }
}