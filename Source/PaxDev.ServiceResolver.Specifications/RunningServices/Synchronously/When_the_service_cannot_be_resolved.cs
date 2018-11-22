using System;
using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.RunningServices.Synchronously
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

    public class When_the_service_can_be_resolved : RunningServicesContext
    {
        Establish context = SetupToResolveTestInterface;

        Because of = () => Subject.ResolveAndRun((ITestInterface i) => i.DoSynchronous());

        It should_run_the_method_on_the_service = () =>
            The<ITestInterface>()
                .WasToldTo(i => i.DoSynchronous());

        It should_dispose_the_scope = () => 
            ServiceScope
                .WasToldTo(s => s.Dispose());
    }

    public class When_the_service_throws_an_error : RunningServicesContext
    {
        Establish context = () =>
        {
            SetupToResolveTestInterface();
            The<ITestInterface>().WhenToldTo(i => i.DoSynchronous()).Throw(new Exception("ExpectedException"));
        };

        Because of = () => 
            CaughtException = Catch.Exception (() => Subject.ResolveAndRun((ITestInterface i) => i.DoSynchronous()));

        It should_catch_the_exception = () => CaughtException.Message.ShouldEqual("ExpectedException");

        It should_dispose_the_scope = () =>
            ServiceScope
                .WasToldTo(s => s.Dispose());
    }
}