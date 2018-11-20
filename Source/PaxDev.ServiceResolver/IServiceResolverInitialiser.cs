using System;

namespace PaxDev.ServiceResolver
{
    public interface IServiceProviderBuilder
    {
        IServiceProvider Build();
    }
}
