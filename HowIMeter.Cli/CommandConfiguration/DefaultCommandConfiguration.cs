using HowIMeter.Cli.Commands;
using Microsoft.Extensions.CommandLineUtils;

namespace HowIMeter.Cli.CommandConfiguration
{
    internal static class DefaultCommandConfiguration
    {
        public static void Configure(CommandLineApplication app, CommandLineOptions options)
        {
            app.Command("query", c => QueryCommandConfiguration.Configure(c, options));

            app.OnExecute(() =>
            {
                options.Command = new DefaultCommand(options);
                return (int) ApplicationErrorKind.NoError;
            });
        }
    }
}