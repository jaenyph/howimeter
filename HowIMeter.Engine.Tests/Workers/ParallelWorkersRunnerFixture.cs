using System.Collections.Generic;
using System.Linq;
using HowIMeter.Engine.Workers;
using Moq;
using NUnit.Framework;

namespace HowIMeter.Engine.Tests.Workers
{
    [TestFixture]
    public class ParallelWorkersRunnerFixture
    {

        [SetUp]
        public void SetUp()
        {
            WorkersMocks = new List<Mock<IWorker>>();
            for (var i = 0; i < 5; ++i)
            {
                var mock = new Mock<IWorker>();
                WorkersMocks.Add(mock);
            }
            Sut = new ParallelWorkersRunner(WorkersMocks.Select(m => m.Object));
        }

        private ICollection<Mock<IWorker>> WorkersMocks { get; set; }
        private ParallelWorkersRunner Sut { get; set; }

        [Test]
        public void RunIsCalledOnAllWorkers()
        {
            var sut = Sut;

            sut.Run();

            foreach (var workersMock in WorkersMocks)
            {
                workersMock.Verify(w => w.Run(), Times.Once);
            }
        }
    }
}
