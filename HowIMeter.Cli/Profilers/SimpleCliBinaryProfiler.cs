using System;
using HowIMeter.Engine.Profilers;

namespace HowIMeter.Cli.Profilers
{
    internal class SimpleCliBinaryProfiler : IBinaryProfiler
    {
        private readonly int _x;
        private readonly int _y;
        private int _bytesRead;
        private int _bytesWrote;

        public SimpleCliBinaryProfiler(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void OnBytesRead(byte[] bytes, int count)
        {
            _bytesRead = count;
            Refresh();
        }

        public void OnBytesWrote(byte[] bytes, int count)
        {
            _bytesWrote = count;
            Refresh();
        }

        private void Refresh()
        {
            var previousLeftPosition = Console.CursorLeft;
            var previousTopPosition = Console.CursorTop;
            var wasCursorVisible = Console.CursorVisible;

            try
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(_x, _y);
                Console.WriteLine($"Read:{_bytesRead} / Write:{_bytesWrote}");
            }
            finally
            {
                Console.SetCursorPosition(previousLeftPosition, previousTopPosition);
                Console.CursorVisible = wasCursorVisible;
            }
        }
    }
}
