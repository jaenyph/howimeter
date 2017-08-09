namespace HowIMeter.Engine.Profilers
{
    /// <summary>
    /// A dummy binrary profiler that does nothing
    /// </summary>
    public class NoopBinaryProfiler : IBinaryProfiler
    {
        public static readonly NoopBinaryProfiler Default = new NoopBinaryProfiler();

        private NoopBinaryProfiler()
        {
        }

        public void OnBytesRead(byte[] bytes, int count)
        {
        }

        public void OnBytesWrote(byte[] bytes, int count)
        {
        }
    }
}