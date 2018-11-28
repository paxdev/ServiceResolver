using System;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices.Asynchronously
{
    public class When_the_service_cannot_be_resolved : RunningServicesContext
    {
        Because of = () => CaughtException = 
            Catch.Exception(() => Subject.ResolveAndRunAsync(async (ITestInterface i) => await i.DoAsynchronous()).Await());

        It should_throw = () => CaughtException.ShouldBeOfExactType<InvalidOperationException>();

        It should_say_the_type_is_unregistered = () =>
            CaughtException.Message.ShouldEqual(
                $"No service for type '{typeof(ITestInterface)}' has been registered.");
    }
}