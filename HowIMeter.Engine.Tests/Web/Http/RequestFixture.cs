using System;
using HowIMeter.Engine.Profilers;
using HowIMeter.Engine.Web.Http;
using NUnit.Framework;

namespace HowIMeter.Engine.Tests.Web.Http
{
    [TestFixture]
    public class RequestFixture
    {
        [Test]
        public void RunReturnsNotNull()
        {
            var sut = new Request(new Uri("./test", UriKind.Relative), NoopBinaryProfiler.Default);

            var actual = sut.Run();

            Assert.NotNull(actual);
        }
    }
}
