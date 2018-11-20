using System;
using System.Threading.Tasks;

namespace PaxDev.ServiceResolver
{
    public interface IServiceResolver : IDisposable
    {
        void ResolveAndRun<TService>(Action<TService> action);
        Task ResolveAndRunAsync<TService>(Func<TService, Task> action);
    }
}
