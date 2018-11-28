using System;
using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices.Asynchronously
{
    public class When_the_service_throws_an_error : RunningServicesContext
    {
        Establish context = () =>
        {
            SetupToResolveTestInterface();
            The<ITestInterface>().WhenToldTo(i => i.DoAsynchronous()).Throw(new Exception("ExpectedException"));
        };

        Because of = () => 
            CaughtException = Catch.Exception (() => Subject.ResolveAndRunAsync((ITestInterface i) => i.DoAsynchronous()).Await());

        It should_catch_the_exception = () => CaughtException.Message.ShouldEqual("ExpectedException");

        It should_dispose_the_scope = () =>
            ServiceScope
                .WasToldTo(s => s.Dispose());
    }
}