namespace HowIMeter.Engine.Web.Http
{
    public interface IHttpRequest : IWebRequest
    {
        string Accept { get; set; }


        int ContinueTimeout { get; set; }


        bool HaveResponse { get; }
    }
}