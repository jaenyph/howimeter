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

        private CommandLineOptions()
        {
            CurrentApplicationErrorStatus = ApplicationErrorKind.NoError;
        }

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

            options.LogginVerbosityOption = app.Option(
                "-l|--logLevel <logLevel>",
                "Cli verbosity",
                CommandOptionType.SingleValue);

            options.Application = app;

            //AdaptLoggingVerbosity(verbosityOption.Value());

            DefaultCommandConfiguration.Configure(app, options);

            options.CurrentApplicationErrorStatus = (ApplicationErrorKind) app.Execute(args);
            
            return options;
        }

        public CommandOption LogginVerbosityOption { get; private set; }

        public CommandLineApplication Application { get; private set; }

        public ApplicationErrorKind CurrentApplicationErrorStatus { get; private set; }
        public ICommand Command { get; set; }

    }
}
