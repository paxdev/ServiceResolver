using Machine.Specifications;

namespace PaxDev.ServiceResolver.Specifications
{
    public class When_it_is_a_test 
    {
        It should_run_the_test = () => true.ShouldBeTrue();
    }
}