using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceResolver
{
    public interface IServiceResolverInitialiser
    {
        IConfiguration BuildConfiguration();
        IServiceCollection ConfigureServices(IConfiguration configuration);
    }
}
