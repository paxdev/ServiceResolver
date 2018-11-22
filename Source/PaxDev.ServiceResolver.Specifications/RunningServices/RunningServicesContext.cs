using System;
using Machine.Fakes;
using Machine.Specifications;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver.Specifications.RunningServices
{
    [Subject(typeof(ServiceResolver))]
    public class RunningServicesContext : WithSubject<ServiceResolver>
    {
        protected static IServiceProvider ScopeServiceProvider;
        protected static IServiceScope ServiceScope;

        protected static Exception CaughtException;

        Establish context = () =>
        {
            ScopeServiceProvider = An<IServiceProvider>();

            Subject = new ServiceResolver(ScopeServiceProvider);

            var serviceScopeFactory = An<IServiceScopeFactory>();

            ScopeServiceProvider
                .WhenToldTo(p => p.GetService(typeof(IServiceScopeFactory)))
                .Return(serviceScopeFactory);

            ServiceScope = An<IServiceScope>();

            serviceScopeFactory
                .WhenToldTo(f => f.CreateScope())
                .Return(ServiceScope);

            ServiceScope
                .WhenToldTo(s => s.ServiceProvider)
                .Return(ScopeServiceProvider);
        };

        protected static void SetupToResolveTestInterface() =>
            ScopeServiceProvider
                .WhenToldTo(p => p.GetService(typeof(ITestInterface)))
                .Return(The<ITestInterface>());
    }
}