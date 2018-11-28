using System;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices.Synchronously
{
    public class When_the_service_cannot_be_resolved : RunningServicesContext
    {
        Because of = () => CaughtException = 
            Catch.Exception(() => Subject.ResolveAndRun((ITestInterface i) => i.DoSynchronous()));

        It should_throw = () => CaughtException.ShouldBeOfExactType<InvalidOperationException>();

        It should_say_the_type_is_unregistered = () =>
            CaughtException.Message.ShouldEqual(
                $"No service for type '{typeof(ITestInterface)}' has been registered.");
    }
}