namespace PaxDev.ServiceResolver
{
    public static class LazyLoadedServiceResolver<TStartup> 
        where TStartup : IServiceResolverStartup, new()
    {
        static IServiceResolver serviceResolver;

        public static IServiceResolver Get()
        {
            if (serviceResolver != null)
            {
                return serviceResolver;
            }

            // todo make thread-safe
            var builder = new ServiceResolverBuilder();
            builder.UseStartup<TStartup>();

            serviceResolver = builder.Build();

            return serviceResolver;
        }
    }
}