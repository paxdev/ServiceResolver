using System.Threading.Tasks;

namespace PaxDev.ServiceResolver.Specifications.ResolvingServices.RunningServices
{
    public interface ITestInterface
    {
        void DoSynchronous();
        Task DoAsynchronous();
    }
}