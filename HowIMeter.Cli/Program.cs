using HowIMeter.Ioc;
using Microsoft.Extensions.CommandLineUtils;

namespace HowIMeter.Cli
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            using (IocContainer.Current)
            {
                try
                {
                    var options = CommandLineOptions.Parse(args);

                    if (options.CurrentApplicationErrorStatus != ApplicationErrorKind.NoError)
                    {
                        return ExitWith(options.CurrentApplicationErrorStatus);
                    }
                    if(options.Command == null)
                    {
                        // DefaultCommand will have printed help 
                        return ExitWith(ApplicationErrorKind.GeneralError);
                    }

                    return ExitWith(options.Command.Run());
                }
                catch (CommandParsingException e)
                {
                    Logger.Current.Error(e.Message);
                    return ExitWith(ApplicationErrorKind.InvalidCliArgument);
                }

            }
        }

        private static int ExitWith(ApplicationErrorKind status)
        {
            if (Logger.Current.IsInfoEnabled)
            {
                Logger.Current.Info($"Exiting with status {(int)status}");
            }
            return (int) status;
        }
    }
}