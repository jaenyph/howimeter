using Microsoft.Extensions.CommandLineUtils;

namespace HowIMeter.Cli.Commands
{
    internal class DefaultCommand : Command
    {
        private readonly CommandLineApplication _app;

        public DefaultCommand(CommandLineOptions options) : base(options)
        {
            _app = CurrentCommandLineOptions.Application;
        }

        protected override ApplicationErrorKind CoreRun()
        {
            _app.ShowHelp();

            return ApplicationErrorKind.GeneralError;
        }
    }
}