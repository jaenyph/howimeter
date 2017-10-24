using System;
using SimpleInjector;

namespace HowIMeter.Ioc
{
    public class GenericServiceProvider : IGenericServiceProvider
    {
        internal Container Container { get; }

        public GenericServiceProvider(Container container)
        {
            Container = container;
        }
        public object GetService(Type serviceType)
        {
            return Container.GetInstance(serviceType);
        }

        public TService GetService<TService>()
            where TService:class 
        {
            return Container.GetInstance<TService>();
        }

        public void Dispose()
        {
            Container?.Dispose();
        }
    }
}
