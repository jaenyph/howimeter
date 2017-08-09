using System;
using System.IO;
using System.Net;

namespace HowIMeter.Engine.Web.Http
{
    public interface IHttpResponse : IDisposable
    {
        long ContentLength { get; }
        string ContentType { get; }
        Uri ResponseUri { get; }
        bool SupportsHeaders { get; }
        HttpStatusCode StatusCode { get; }
        string StatusDescription { get; }
        Stream GetResponseStream();
    }
}