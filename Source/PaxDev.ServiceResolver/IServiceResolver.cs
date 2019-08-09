using System;
using System.Threading.Tasks;

namespace PaxDev.ServiceResolver
{
    public interface IServiceResolver : IDisposable
    {
        void ResolveAndRun<TService>(Action<TService> action);

        TReturn ResolveAndRun<TService, TReturn>(Func<TService, TReturn> func);

        Task ResolveAndRunAsync<TService>(Func<TService, Task> action);

        Task<TReturn> ResolveAndRunAsync<TService, TReturn>(Func<TService, Task<TReturn>> func);

    }
}
