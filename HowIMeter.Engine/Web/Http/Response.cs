using HowIMeter.Engine.Workers;

namespace HowIMeter.Engine.Web.Http
{
    public class Response : IWorkerResult
    {
        private readonly IHttpResponse _response;

        internal Response(IHttpResponse response)
        {
            _response = response;
        }
    }
}