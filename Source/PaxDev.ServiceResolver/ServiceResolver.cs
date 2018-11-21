using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public class ServiceResolver : IServiceResolver
    {
        readonly IServiceProvider _serviceProvider;

        public ServiceResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public void ResolveAndRun<TService>(Action<TService> action)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TService>();
                action(service);
            }
        }

        public async Task ResolveAndRunAsync<TService>(Func<TService, Task> action)
        {
            IServiceScope scope = null;
            try
            {
                scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<TService>();
                await action(service);
            }
            finally
            {
                scope?.Dispose();
            }
        }

        public void Dispose()
        {
            if (_serviceProvider is IDisposable d)
            {
                d.Dispose();
            }
        }
    }
}
