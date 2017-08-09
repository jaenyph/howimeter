using System;
using System.IO;
using System.Net;

namespace HowIMeter.Engine.Web.Http
{
    /// <summary>
    ///     A wrapper for HttpWebResponse
    /// </summary>
    internal class HttpResponse : IHttpResponse
    {
        private readonly HttpWebResponse _response;

        internal HttpResponse(HttpWebResponse response)
        {
            _response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public long ContentLength => _response.ContentLength;
        public string ContentType => _response.ContentType;
        public Uri ResponseUri => _response.ResponseUri;
        public bool SupportsHeaders => _response.SupportsHeaders;
        public HttpStatusCode StatusCode => _response.StatusCode;
        public string StatusDescription => _response.StatusDescription;

        public void Dispose()
        {
            _response.Dispose();
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }
}