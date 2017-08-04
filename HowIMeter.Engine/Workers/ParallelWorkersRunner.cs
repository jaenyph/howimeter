using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowIMeter.Engine.Workers
{
    public class ParallelWorkersRunner : IWorkersRunner
    {
        private readonly IEnumerable<IWorker> _workers;

        public ParallelWorkersRunner(IEnumerable<IWorker> workers)
        {
            _workers = workers ?? throw new ArgumentNullException(nameof(workers));
        }

        public IEnumerable<Task<IWorkerResult>> Run()
        {
            return _workers.Select(worker => worker.Run()).ToArray();
        }
    }
}
