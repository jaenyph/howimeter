using HowIMeter.Ioc.Configuration;
using SimpleInjector;

namespace HowIMeter.Ioc
{
    public static class IocContainer
    {
        private static readonly GenericServiceProvider ServiceProvider;

        public static IGenericServiceProvider Current => ServiceProvider;

        static IocContainer()
        {
            ServiceProvider = new GenericServiceProvider(new Container());
            new IocConfiguration().Setup(ServiceProvider.Container);
        }
    }
}
