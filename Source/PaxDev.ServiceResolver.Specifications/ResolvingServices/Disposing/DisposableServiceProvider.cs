using System;
using Machine.Fakes;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.Disposing
{
    public class DisposableServiceProvider : WithFakes, IServiceProvider, IDisposable
    {
        readonly IActionLogger actionLogger;

        public DisposableServiceProvider(IActionLogger actionLogger)
        {
            this.actionLogger = actionLogger;
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            actionLogger.Log(nameof(Dispose));    
        }
    }
}