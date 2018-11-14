
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceResolver
{
    public class ServiceResolver : IServiceResolver
    {
        static ServiceResolver _instance;
        static readonly object Locker = new object();

        readonly ServiceProvider _serviceProvider;

        ServiceResolver(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public static IServiceResolver GetInstance()
        {
            lock (Locker)
            {
                if (_instance == null)
                {
                    throw new ResolverNotInstantiatedException();
                }
            }

            return _instance;
        }
        
        public static void Initialise<TInitialiser>() where TInitialiser : IServiceResolverInitialiser, new()
        {
            lock (Locker)
            {
                if (_instance != null)
                {
                    throw new ResolverAlreadyInstantatedException();
                }
            }
            var initialiser = new TInitialiser();
            var configuration = initialiser.BuildConfiguration();
            var services = initialiser.ConfigureServices(configuration);
            var serviceProvider = services.BuildServiceProvider();
            lock (Locker)
            {
                if (_instance != null)
                {
                    _instance = new ServiceResolver(serviceProvider);
                }
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
    }
}
