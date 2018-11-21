using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public interface IServiceResolverStartup
    {
        void Configure(IConfigurationBuilder configurationBuilder);
        void ConfigureServices(IConfiguration configuration, IServiceCollection services);
    }
}