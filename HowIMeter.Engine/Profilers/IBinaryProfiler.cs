namespace HowIMeter.Engine.Profilers
{
    public interface IBinaryProfiler
    {
        void OnBytesRead(byte[] bytes, int count);

        void OnBytesWrote(byte[] bytes, int count);
    }
}