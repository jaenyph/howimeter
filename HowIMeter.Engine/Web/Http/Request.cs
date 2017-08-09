﻿using System;
using System.Threading.Tasks;
using HowIMeter.Engine.Profilers;
using HowIMeter.Engine.Workers;

namespace HowIMeter.Engine.Web.Http
{
    public class Request : IWorker
    {
        private readonly IBinaryProfiler _profiler;

        public Request(Uri uri, IBinaryProfiler responseProfiler)
        {
            _profiler = responseProfiler ?? throw new ArgumentNullException(nameof(responseProfiler));
            Uri = uri;
        }

        public Uri Uri { get; }

        public async Task<IWorkerResult> Run()
        {
            var httpRequest = new HttpRequest(Uri);
            return await httpRequest.GetResponseAsync().ContinueWith(response =>
            {
                httpRequest.GetRequestStreamAsync().ContinueWith(request =>
                {
                    
                });
                return new Response(response.Result);
            });
        }
    }
}