using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public class ServiceResolver : IServiceResolver
    {
        readonly IServiceProvider serviceProvider;

        public ServiceResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public void ResolveAndRun<TService>(Action<TService> action)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var service = scope
                                .ServiceProvider
                                .GetRequiredService<TService>();
                action(service);
            }
        }

        public TReturn ResolveAndRun<TService, TReturn>(Func<TService, TReturn> func)
        {
            TReturn returnValue = default;

            ResolveAndRun<TService>(s => returnValue = func(s));

            return returnValue;
        }

        public async Task ResolveAndRunAsync<TService>(Func<TService, Task> action)
        {
            IServiceScope scope = null;
            try
            {
                scope = serviceProvider.CreateScope();
                var service = scope
                                .ServiceProvider
                                .GetRequiredService<TService>();
                await action(service);
            }
            finally
            {
                scope?.Dispose();
            }
        }

        public async Task<TReturn> ResolveAndRunAsync<TService, TReturn>(Func<TService, Task<TReturn>> func)
        {
             TReturn returnValue = default;

             await ResolveAndRunAsync<TService>(async service => await func(service));

             return returnValue;
        }

        public void Dispose()
        {
            if (serviceProvider is IDisposable d)
            {
                d.Dispose();
            }
        }
    }
}
