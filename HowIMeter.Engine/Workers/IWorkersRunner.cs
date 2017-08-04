using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowIMeter.Engine.Workers
{
    public interface IWorkersRunner
    {
        IEnumerable<Task<IWorkerResult>> Run();
    }
}
