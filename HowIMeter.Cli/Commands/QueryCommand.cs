using HowIMeter.Cli.Web.Http;

namespace HowIMeter.Cli.Commands
{
    internal class QueryCommand : ICommand
    {
        private readonly int _count;

        private readonly string _uri;

        public QueryCommand(string uri, int count)
        {
            _uri = uri;
            _count = count;
        }

        public ApplicationErrorKind Run()
        {
            if (Logger.Current.IsInfoEnabled)
                Logger.Current.Info(
                    $"Launching {_count} parallel queries on '{_uri}' ...");

            var result = new HttpParallelWorkersLauncher(_count, _uri, ApplicationContext.Current.DefaultBinaryProfiler).Launch();

            if (Logger.Current.IsInfoEnabled)
                Logger.Current.Info($"All {_count} queries run with result '{result}'");


            return result;
        }
    }
}