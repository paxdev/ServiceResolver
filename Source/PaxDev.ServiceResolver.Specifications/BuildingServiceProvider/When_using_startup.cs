using System;
using Machine.Specifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaxDev.ServiceResolver.Specifications.BuildingServiceProvider
{
    public class When_using_startup : ServiceResolverBuilderContext
    {
        public static IActionLogger ActionLogger { get; set; }

        public static IServiceResolver Resolver { get; set; }

        Establish context = () => Subject.UseStartup<TestStartup>();

        Because of = () => Resolver = Subject.Build();

        It should_resolve_correctly = () =>
            Resolver.ResolveAndRun(AssertResolvedClassExpected());

        It should_do = () =>
        {
            string objGetValue = null;
            LazyLoadedServiceResolver<TestStartup>.Get().ResolveAndRun((TestClass c) =>
            {
                objGetValue  = c.GetValue;   
            });

            Console.WriteLine(objGetValue);
        };

        static Action<TestClass> AssertResolvedClassExpected() =>
            c => c.GetValue.ShouldEqual(TestConfiguration.Value);

        public class TestStartup : IServiceResolverStartup
        {
            public void Configure(IConfigurationBuilder configurationBuilder)
            {
                configurationBuilder.Sources.Add(new TestConfiguration());
            }

            public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
            {
                services.AddSingleton<TestClass>(new TestClass(configuration[TestConfiguration.Key]));
            }
        }

        public class TestClass
        {
            public TestClass(string value)
            {
                GetValue = value;
            }

            public string GetValue { get; }
        }
    }
}