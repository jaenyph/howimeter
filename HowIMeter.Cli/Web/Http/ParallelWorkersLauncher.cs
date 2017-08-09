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
        private readonly string _uri;
        private readonly IBinaryProfiler _profiler;

        public HttpParallelWorkersLauncher(int count, string uri, IBinaryProfiler profiler)
        {
            _count = count;
            _uri = uri;
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
        }

        public ApplicationErrorKind Launch()
        {
            if (!Uri.TryCreate(_uri, UriKind.Absolute, out Uri uri))
            {
                return ApplicationErrorKind.InvalidUri;
            }

            var workers = Enumerable.Range(0, _count).Select(e => new Request(uri, _profiler));
            var result = new ParallelWorkersRunner(workers).Run();
            Task.WhenAll(result).Wait();

            return ApplicationErrorKind.NoError;
        }
    }
}