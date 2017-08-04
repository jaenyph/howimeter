using SimpleInjector;

namespace HowIMeter.Ioc.Configuration
{
    public class IocConfiguration
    {
        public void Setup(Container container)
        {
            container.Verify();
        }
    }
}
