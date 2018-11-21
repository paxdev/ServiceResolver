using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public class ServiceResolverBuilder
    {
        readonly List<Action<IConfigurationBuilder>> configurationActions 
            = new List<Action<IConfigurationBuilder>>();

        readonly List<Action<IConfiguration, IServiceCollection>> configureServicesActions 
            = new List<Action<IConfiguration, IServiceCollection>>();

        bool resolverBuilt = false;
        
        public ServiceResolverBuilder ConfigureServices(Action<IConfiguration, IServiceCollection> configureServiceAction)
        {
            configureServicesActions.Add(configureServiceAction);
            return this;
        }

        public ServiceResolverBuilder Configure(Action<IConfigurationBuilder> configurationAction)
        {
            configurationActions.Add(configurationAction);
            return this;
        }

        public ServiceResolverBuilder UseStartup<TStartup>() where TStartup : IServiceResolverStartup, new()
        {
            var startup = new TStartup();
            configurationActions.Add(config => startup.Configure(config));
            configureServicesActions.Add((config, services) => startup.ConfigureServices(config, services));
            return this;
        }

        public IServiceResolver Build()
        {
            if (resolverBuilt)
            {
                throw new InvalidOperationException("Build can only be called once.");
            }

            resolverBuilt = true;

            var configuration = BuildConfiguration();

            var services = RegisterServices(configuration);

            services.AddSingleton<IServiceResolver, ServiceResolver>();

            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IServiceResolver>();
        }

        IConfigurationRoot BuildConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            foreach (var configurationAction in configurationActions)
            {
                configurationAction(configurationBuilder);
            }

            var configuration = configurationBuilder.Build();
            return configuration;
        }

        ServiceCollection RegisterServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            foreach (var configureServicesAction in configureServicesActions)
            {
                configureServicesAction(configuration, services);
            }

            return services;
        }
    }
}