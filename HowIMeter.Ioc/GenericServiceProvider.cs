using System;
using SimpleInjector;

namespace HowIMeter.Ioc
{
    public class GenericServiceProvider : IGenericServiceProvider
    {
        private readonly Container _container;

        internal Container Container
        {
            get => _container;
        }

        public GenericServiceProvider(Container container)
        {
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public TService GetService<TService>()
            where TService:class 
        {
            return _container.GetInstance<TService>();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}
