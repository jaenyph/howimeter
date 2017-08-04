using System;
namespace HowIMeter.Ioc
{
    public interface IGenericServiceProvider : IServiceProvider, IDisposable
    {
        TService GetService<TService>() where TService : class;
    }
}
