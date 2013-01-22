using Example.Api.Filters;
using ServiceStack.FluentValidation;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Example.Api.Messaging
{
    [Route("/award", "POST")]
    //[Route("/award/{PointAmount}/{PersonalMessage}/{Comments}/{AwardDenomId}", "POST")]
    [Authenticate]
    [RequiredRole("BudgetUser")]
    [RequiredPermission("GiveAward")]
    public class AwardRequest : IReturn<AwardResponse>
    {
        public int PointAmount { get; set; }
        public string PersonalMessage { get; set; }
        public string Comments { get; set; }
        public int AwardDenomId { get; set; }
    }
}