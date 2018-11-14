using System;
using System.Threading.Tasks;

namespace ServiceResolver
{
    public interface IServiceResolver
    {
        void ResolveAndRun<TService>(Action<TService> action);
        Task ResolveAndRunAsync<TService>(Func<TService, Task> action);
    }
}
