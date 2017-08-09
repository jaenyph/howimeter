using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HowIMeter.Engine.Profilers
{
    /// <summary>
    ///     Call the appropriate BinaryProfiler callbacks for a given stream operations
    /// </summary>
    internal class BinaryProfilerStreamMonitor
    {
        private readonly bool _isWriting;
        private readonly object _streamSyncRoot;
        private readonly IBinaryProfiler _profiler;

        public BinaryProfilerStreamMonitor(IBinaryProfiler profiler, bool isWriting, object streamSyncRoot = null)
        {
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _isWriting = isWriting;
            _streamSyncRoot = streamSyncRoot ?? new object();
        }

        /// <summary>
        ///     Start using the current instance BinaryProfiler for the given stream
        /// </summary>
        /// <param name="stream">The stream to monitor</param>
        /// <param name="cancellationToken"></param>
        public Task<IBinaryProfiler> Monitor(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            return Task.Run(() =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return _profiler;

                var isSeekable = stream.CanSeek;
                long previousStreamPosition = -1;
                long nextPositionToRead = 0;
                do
                {
                    int readValue;

                    if (cancellationToken.IsCancellationRequested)
                        return _profiler;

                    lock (_streamSyncRoot)
                    {
                        if (isSeekable)
                        {
                            previousStreamPosition = stream.Position;
                            stream.Position = nextPositionToRead;
                        }

                        readValue = stream.ReadByte();
                        if(readValue > -1)
                            ++nextPositionToRead;

                        if (isSeekable)
                            stream.Position = previousStreamPosition;
                    }


                    if (readValue == -1)
                    {
                        if (nextPositionToRead != 0)
                        {
                            //End of stream reached:
                            return _profiler;
                        }

                        //Stream not yet feed, wait for input
                        if (cancellationToken.IsCancellationRequested)
                            return _profiler;

                        Thread.Sleep(500);
                        continue;
                    }

                    var bytes = new[] {Convert.ToByte(readValue)};

                    if (_isWriting)
                        _profiler.OnBytesWrote(bytes, 1);
                    else
                        _profiler.OnBytesRead(bytes, 1);

                } while (true);

            }, cancellationToken);
        }
    }
}