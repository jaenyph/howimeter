using System.Net;
using HowIMeter.Engine.Workers;

namespace HowIMeter.Engine.Web.Http
{
    public class Response : IWorkerResult
    {
        private readonly WebResponse _response;

        internal Response(WebResponse response)
        {
            _response = response;
        }
    }
}
