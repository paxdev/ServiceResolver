using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver
{
    public interface IServiceResolverStartup
    {
        void ConfigureServices(IConfigurationBuilder configurationBuilder);
        void ConfigureServices(IConfiguration configuration, IServiceCollection services);
    }
}