using System;
using System.Reflection;
using HowIMeter.Cli.CommandConfiguration;
using HowIMeter.Cli.Commands;
using log4net.Core;
using Microsoft.Extensions.CommandLineUtils;

namespace HowIMeter.Cli
{
    internal class CommandLineOptions
    {
        public const string DefaultHelpOptionTemplate = "-?|-h|--help";

        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            var app = new CommandLineApplication
            {
                Name = "howimeter",
                FullName = "HowIMeter performance tool"
            };

            app.HelpOption(DefaultHelpOptionTemplate);
            app.VersionOption("-v|--version",
                () => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    .InformationalVersion);

            var verbosityOption = app.Option(
                "-l|--logLevel <logLevel>",
                "Cli verbosity",
                CommandOptionType.SingleValue);

            
            DefaultCommandConfiguration.Configure(app, options);

            var result = app.Execute(args);

            if (result != 0)
            {
                return null;
            }

            AdaptLoggingVerbosity(verbosityOption.Value());


            return options;
        }

        public ICommand Command { get; set; }


        private static void AdaptLoggingVerbosity(string value)
        {
            Level currentLogginLevel;
            switch (value?.ToLower())
            {
                case "d":
                case "debug":
                    currentLogginLevel = Level.Debug;
                    break;

                case "e":
                case "error":
                case null:
                    currentLogginLevel = Level.Error;
                    break;

                case "f":
                case "fatal":
                    currentLogginLevel = Level.Fatal;
                    break;

                case "i":
                case "info":
                    currentLogginLevel = Level.Info;
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled verbosity '{value}'");
            }
            LoggingSetup.Setup(currentLogginLevel);
        }
    }
}
