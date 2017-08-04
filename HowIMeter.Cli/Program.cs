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
                LoggingSetup.Setup();

                //var application = CommandLineApplicationSetup.Setup();

                try
                {
                    var options = CommandLineOptions.Parse(args);

                    if (options?.Command == null)
                    {
                        // DefaultCommand will have printed help
                        return (int) ApplicationErrorKind.GeneralError;
                    }

                    return (int) options.Command.Run();
                }
                catch (CommandParsingException e)
                {
                    Logger.Current.Error(e.Message);
                    return (int) ApplicationErrorKind.InvalidCliArgument;
                }

            }
        }
    }
}