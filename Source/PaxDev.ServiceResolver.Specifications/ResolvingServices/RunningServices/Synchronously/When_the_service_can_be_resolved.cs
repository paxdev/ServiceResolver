using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices.Synchronously
{
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
}