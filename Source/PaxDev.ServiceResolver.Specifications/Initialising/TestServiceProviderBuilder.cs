using System;
using Machine.Fakes;

namespace PaxDev.ServiceResolver.Specifications.Initialising
{
    public class TestServiceProviderBuilder: WithFakes, IServiceProviderBuilder
    {
        public IServiceProvider Build()
        {
            return An<IServiceProvider>();
        }
    }
}