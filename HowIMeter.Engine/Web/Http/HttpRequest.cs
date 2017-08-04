using System;
using System.Net;

namespace HowIMeter.Engine.Web.Http
{
    internal class HttpRequest : IHttpRequest
    {
        private readonly HttpWebRequest _request;

        public HttpRequest(string uri)
        {
            _request = WebRequest.CreateHttp(uri);
        }

        public HttpRequest(Uri uri)
        {
            _request = WebRequest.CreateHttp(uri);
        }

        public int ContinueTimeout => _request.ContinueTimeout;

        public string Accept
        {
            get => _request.Accept;
            set => _request.Accept = value;
        }

        public string Method
        {
            get => _request.Method;
            set => _request.Method = value;
        }

        public Uri RequestUri => _request.RequestUri;

        public ICredentials Credentials
        {
            get => _request.Credentials;
            set => _request.Credentials = value;
        }

        public string ContentType
        {
            get => _request.ContentType;
            set => _request.ContentType = value;
        }

        public bool HaveResponse => _request.HaveResponse;

        public bool UseDefaultCredentials
        {
            get => _request.UseDefaultCredentials;
            set => _request.UseDefaultCredentials = value;
        }

        public IWebProxy Proxy
        {
            get => _request.Proxy;
            set => _request.Proxy = value;
        }

        int IHttpRequest.ContinueTimeout
        {
            get => _request.ContinueTimeout;
            set => _request.ContinueTimeout = value;
        }

        public void Abort()
        {
            _request.Abort();
        }
    }
}