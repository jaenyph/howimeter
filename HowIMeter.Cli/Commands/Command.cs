using System;
using log4net.Core;

namespace HowIMeter.Cli.Commands
{
    internal abstract class Command : ICommand
    {
        protected CommandLineOptions CurrentCommandLineOptions { get; }

        protected Command(CommandLineOptions currentCommandLineOptions)
        {
            CurrentCommandLineOptions = currentCommandLineOptions;
        }

        public ApplicationErrorKind Run()
        {
            AdaptLoggingVerbosity();
            return CoreRun();
        }

        private void AdaptLoggingVerbosity()
        {
            var value = CurrentCommandLineOptions.LogginVerbosityOption.Value();
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

        protected abstract ApplicationErrorKind CoreRun();
    }
}
