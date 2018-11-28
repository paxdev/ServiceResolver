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

        public void Dispose()
        {
            if (serviceProvider is IDisposable d)
            {
                d.Dispose();
            }
        }
    }
}
