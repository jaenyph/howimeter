using System;
using HowIMeter.Cli.Profilers;
using HowIMeter.Engine.Profilers;

namespace HowIMeter.Cli
{
    internal class ApplicationContext
    {
        public static readonly ApplicationContext Current = new ApplicationContext();

        private IBinaryProfiler _defaultBinaryProfiler;
        
        private ApplicationContext()
        {
            _defaultBinaryProfiler = new SimpleCliBinaryProfiler(0,0);
        }

        public IBinaryProfiler DefaultBinaryProfiler
        {
            get => _defaultBinaryProfiler;
            set
            {
                if (value == null)
                    throw new InvalidOperationException($"{nameof(DefaultBinaryProfiler)} could not be undefined");
                _defaultBinaryProfiler = value;
            }
        }
    }
}