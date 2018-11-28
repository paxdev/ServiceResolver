using Machine.Fakes;
using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.Disposing
{
    [Subject(typeof(ServiceResolver))]
    public class When_the_service_provider_is_disposable : WithFakes
    {
        static ServiceResolver Subject;
        static IActionLogger ActionLogger;
        
        Establish context = () =>
        {
            ActionLogger = An<IActionLogger>();
            Subject = new ServiceResolver(new DisposableServiceProvider(ActionLogger));
        };

        Because of = () => Subject.Dispose();

        It should_dispose_of_the_service_provider = () => 
            ActionLogger
                .WasToldTo(d => d.Log(nameof(DisposableServiceProvider.Dispose)));
    }
}