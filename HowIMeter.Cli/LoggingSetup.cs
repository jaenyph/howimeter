using System.Reflection;
using System.Xml;

namespace HowIMeter.Cli
{
    class LoggingSetup
    {
        public static void Setup()
        {
            var configDocument = new XmlDocument();
            //configDocument.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo);
        }
    }
}
