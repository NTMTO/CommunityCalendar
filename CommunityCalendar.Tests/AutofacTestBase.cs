using System;
using System.Reflection;
using Autofac;
namespace CommunityCalendar.Tests
{
    public class AutofacTestBase
    {

        public IContainer _container;

        public AutofacTestBase()
        {
            _container = Configure();
        }

        public IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SampleDataProvider>().As<ISampleDataProvider>();
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(CommunityCalendar)))
                .Where(t => t.Namespace.Contains("Utilities"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }

    }
}

