using Example.Api.Filters;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Example.Api.Messaging
{
    [LastIpFilter]
    public class AwardResponse
    {
        public bool IsSuccess { get; set; }
        public string LastMessage { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}