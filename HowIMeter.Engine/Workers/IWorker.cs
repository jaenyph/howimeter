using System.Threading.Tasks;

namespace HowIMeter.Engine.Workers
{
    public interface IWorker
    {
        Task<IWorkerResult> Run();
    }
}
