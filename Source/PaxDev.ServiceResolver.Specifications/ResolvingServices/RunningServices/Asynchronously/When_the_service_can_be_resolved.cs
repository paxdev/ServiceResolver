using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices.Asynchronously
{
    public class When_the_service_can_be_resolved : RunningServicesContext
    {
        Establish context = SetupToResolveTestInterface;

        Because of = async () => await Subject.ResolveAndRunAsync(async (ITestInterface i) => await i.DoAsynchronous());

        It should_run_the_method_on_the_service = () =>
            The<ITestInterface>()
                .WasToldTo(i => i.DoAsynchronous());

        It should_dispose_the_scope = () => 
            ServiceScope
                .WasToldTo(s => s.Dispose());
    }
}