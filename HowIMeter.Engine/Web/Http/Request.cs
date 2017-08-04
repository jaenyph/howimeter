using System;
using System.Net;
using System.Threading.Tasks;
using HowIMeter.Engine.Workers;

namespace HowIMeter.Engine.Web.Http
{
    public class Request : IWorker
    {
        public Uri Uri { get; set; }

        public async Task<IWorkerResult> Run()
        {
            return await WebRequest.CreateHttp(Uri).GetResponseAsync().ContinueWith(t => new Response(t.Result));
        }
    }
}
