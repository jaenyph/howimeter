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
            switch (value?.ToLower())
            {
                case "debug":
                    Logger.Current.Logger.Repository.Threshold = Level.Debug;
                    break;
                case null:
                case "error":
                    Logger.Current.Logger.Repository.Threshold = Level.Error;
                    break;
                case "fatal":
                    Logger.Current.Logger.Repository.Threshold = Level.Fatal;
                    break;
                case "info":
                    Logger.Current.Logger.Repository.Threshold = Level.Info;
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled verbosity '{value}'");
            }
        }
    }
}
