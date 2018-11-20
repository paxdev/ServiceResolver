using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public abstract class ServiceProviderBuilder : IServiceProviderBuilder
    {
        public IServiceProvider Build()
        {
            var configuration = BuildConfiguration();
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            return services.BuildServiceProvider(validateScopes: true);
        }

        public abstract IConfiguration BuildConfiguration();
        public abstract void ConfigureServices(ServiceCollection services, IConfiguration configuration);
    }
}