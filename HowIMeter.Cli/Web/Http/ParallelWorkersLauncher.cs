using System;
using System.Linq;
using System.Threading.Tasks;
using HowIMeter.Engine.Profilers;
using HowIMeter.Engine.Web.Http;
using HowIMeter.Engine.Workers;

namespace HowIMeter.Cli.Web.Http
{
    internal class HttpParallelWorkersLauncher : ILauncher
    {
        private readonly int _count;
        private readonly Uri _uri;
        private readonly IBinaryProfiler _profiler;

        public HttpParallelWorkersLauncher(int count, Uri uri, IBinaryProfiler profiler)
        {
            _count = count;
            _uri = uri;
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
        }

        public ApplicationErrorKind Launch()
        {
            var workers = Enumerable.Range(0, _count).Select(e => new Request(_uri, _profiler));
            var result = new ParallelWorkersRunner(workers).Run();
            var status = ApplicationErrorKind.NoError;
            Task.WhenAll(result).Wait();

            if (result.Any(t => t.IsFaulted))
            {
                status = ApplicationErrorKind.GeneralError;
            }


            return status;
        }
    }
}