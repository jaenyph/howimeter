using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HowIMeter.Engine.Profilers;
using Moq;
using NUnit.Framework;

namespace HowIMeter.Engine.Tests.Profilers
{
    [TestFixture]
    public class BinaryStreamProfilerFixture
    {

        private const int MinimumStreamMillisecondsDuration = 5000;
        private static readonly object StreamSyncRoot = new object();

        private readonly Action<object> _constantWriter = streamObj =>
        {
            var stream = streamObj as Stream;
            var start = DateTime.UtcNow;

            while (DateTime.UtcNow.Subtract(start).TotalMilliseconds < MinimumStreamMillisecondsDuration)
            {
                lock (StreamSyncRoot)
                {
                    stream.WriteByte(1);
                }
                
            }
        };

        [TestCase(true)]
        [TestCase(false)]
        public void MonitorUsesIBinaryProfilerCallbacks(bool isWriting)
        {
            //Arrange
            const int taskWaitTimeout = MinimumStreamMillisecondsDuration + MinimumStreamMillisecondsDuration / 10;
            var stream = new MemoryStream();
            var profilerMock = new Mock<IBinaryProfiler>();
            var sut = new BinaryProfilerStreamMonitor(profilerMock.Object, isWriting, StreamSyncRoot);
            var writerTask = new Task(_constantWriter, stream);
            writerTask.Start();

            //Act
            var actual = sut.Monitor(stream, new CancellationToken());
            
            actual.Wait(taskWaitTimeout);
            writerTask.Wait(taskWaitTimeout);

            //Assert
            if (isWriting)
            {
                profilerMock.Verify(p => p.OnBytesWrote(It.IsAny<byte[]>(), It.IsAny<int>()));
            }
            else
            {
                profilerMock.Verify(p => p.OnBytesRead(It.IsAny<byte[]>(), It.IsAny<int>()));
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MonitorUsesCancellationToken(bool isWriting)
        {
            //Arrange
            const int taskWaitTimeout = MinimumStreamMillisecondsDuration + MinimumStreamMillisecondsDuration / 10;
            var stream = new MemoryStream();
            var profilerMock = new Mock<IBinaryProfiler>();
            var sut = new BinaryProfilerStreamMonitor(profilerMock.Object, isWriting, StreamSyncRoot);
            var writerTask = new Task(_constantWriter, stream);
            writerTask.Start();
            var cancellationToken = new CancellationToken(true);

            //Act
            sut.Monitor(stream, cancellationToken);

            writerTask.Wait(taskWaitTimeout);

            //Assert
            profilerMock.Verify(p => p.OnBytesWrote(It.IsAny<byte[]>(), It.IsAny<int>()), Times.Never);
            profilerMock.Verify(p => p.OnBytesRead(It.IsAny<byte[]>(), It.IsAny<int>()), Times.Never);
        }
    }
}