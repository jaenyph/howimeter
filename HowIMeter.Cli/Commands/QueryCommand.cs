using System;
using HowIMeter.Cli.Web.Http;

namespace HowIMeter.Cli.Commands
{
    internal class QueryCommand : Command
    {
        private readonly int _count;

        private readonly Uri _uri;

        public QueryCommand(CommandLineOptions options, Uri uri, int count) :base(options)
        {
            _uri = uri;
            _count = count;
        }

        protected override ApplicationErrorKind CoreRun()
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