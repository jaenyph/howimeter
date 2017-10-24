using System;
using HowIMeter.Cli.Commands;
using Microsoft.Extensions.CommandLineUtils;

namespace HowIMeter.Cli.CommandConfiguration
{
    internal static class QueryCommandConfiguration
    {
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.ExtendedHelpText = "This command perform a query on the given target uri.";
            command.Description = "Perform a query on a given target";
            command.HelpOption(CommandLineOptions.DefaultHelpOptionTemplate);

            var uriArgument = command.Argument(
                "uri",
                "The uri to target when querying");

            var countOption = command.Option(
                "-c|--count <count>",
                "Number of queries to run",
                CommandOptionType.SingleValue);

            
            command.OnExecute(() =>
            {
                var count = 1;
                if (countOption.HasValue())
                {
                    if (!int.TryParse(countOption.Value(), out count))
                    {
                        throw new CommandParsingException(command, $"Invalid number for '{nameof(count)}'");
                    }
                }

                if (!Uri.TryCreate(uriArgument.Value, UriKind.RelativeOrAbsolute, out Uri uri) || !uri.IsWellFormedOriginalString())
                {
                    return (int) ApplicationErrorKind.InvalidUri;
                }

                options.Command = new QueryCommand(options, uri, count);

                return 0;
            });
        }
    }
}