using Example.Api.Messaging;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace Example.Api.Filters
{
    /// <summary>
    /// Example filter that records the last IP address to perform an action
    /// </summary>
    public class RecordIpFilter : RequestFilterAttribute
    {
        public ICacheClient Cache { get; set; }

        public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            Cache.Add("LastIp" ,req.UserHostAddress);
        }
    }

    /// <summary>
    /// Example filter that populates property with the lastIp used (grabs from cache)
    /// </summary>
    public class LastIpFilter : ResponseFilterAttribute
    {
        public ICacheClient Cache { get; set; }

        public override void Execute(IHttpRequest req, IHttpResponse res, object responseDto)
        {
            var awd = responseDto as AwardResponse;
            if (awd != null)
            {
                awd.LastMessage = Cache.Get<string>("LastIp");
            }
        }
    }
}