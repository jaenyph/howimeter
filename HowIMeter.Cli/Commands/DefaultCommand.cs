using Microsoft.Extensions.CommandLineUtils;

namespace HowIMeter.Cli.Commands
{
    internal class DefaultCommand : ICommand
    {
        private readonly CommandLineApplication _app;

        public DefaultCommand(CommandLineApplication app)
        {
            _app = app;
        }

        public ApplicationErrorKind Run()
        {
            _app.ShowHelp();

            return ApplicationErrorKind.GeneralError;
        }
    }
}