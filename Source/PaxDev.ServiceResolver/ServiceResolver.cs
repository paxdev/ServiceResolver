using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public class ServiceResolver : IServiceResolver
    {
        static ServiceResolver Instance;
        static readonly object Locker = new object();

        readonly IServiceProvider _serviceProvider;

        public ServiceResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public static IServiceResolver GetInstance()
        {
            lock (Locker)
            {
                if (Instance == null)
                {
                    throw new ResolverNotInstantiatedException();
                }
            }

            return Instance;
        }
        
        public static void Initialise<TServiceProviderBuilder>() where TServiceProviderBuilder : IServiceProviderBuilder, new()
        {
            lock (Locker)
            {
                if (Instance != null)
                {
                    throw new ResolverAlreadyInstantatedException();
                }
                var serviceProviderBuilder = new TServiceProviderBuilder();
                var serviceProvider = serviceProviderBuilder.Build();
                Instance = new ServiceResolver(serviceProvider);
            }

        }

        public void ResolveAndRun<TService>(Action<TService> action)
        {
            IServiceScope scope = null;
            try
            {
                scope = _serviceProvider.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<TService>();
                action(service);
            }
            finally
            {
                scope?.Dispose();
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
            throw new NotImplementedException();
        }
    }
}
