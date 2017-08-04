using System;
using System.Net;

namespace HowIMeter.Engine.Web
{
    public interface IWebRequest
    {
        string ContentType { get; set; }

        ICredentials Credentials { get; set; }

        string Method { get; set; }

        Uri RequestUri { get; }

        bool UseDefaultCredentials { get; set; }
        IWebProxy Proxy { get; set; }
        void Abort();
    }
}