using System.Reflection;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace HowIMeter.Cli
{
    internal static class LoggingSetup
    {
        public static void Setup(Level logginLevel)
        {
            var repository = (Hierarchy)log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(Hierarchy));

            var consoleLayout = new PatternLayout
            {
                ConversionPattern = "%message%newline"
            };
            consoleLayout.ActivateOptions();

            var consoleAppender = new ConsoleAppender
            {
                Threshold = Level.All,
                Layout = consoleLayout
            };
            consoleAppender.ActivateOptions();

            repository.Root.AddAppender(consoleAppender);

            repository.Root.Level = logginLevel;
            repository.Configured = true;
        }
    }
}
