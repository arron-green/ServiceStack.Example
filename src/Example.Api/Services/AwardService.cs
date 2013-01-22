using System;
using Example.Api.Filters;
using Example.Api.Messaging;
using Example.Api.Repository;
using ServiceStack.ServiceInterface;

namespace Example.Api.Services
{
    //example of using filters within the service level
    [RecordIpFilter]
    public class AwardService : Service
    {
        //this will get populated by the IoC within Apphost
        public AwardRepository AwardRepository { get; set; }

        public object Post(AwardRequest request)
        {
            AwardResponse response;
            try
            {
                AwardRepository.CreateAward(request.PointAmount, request.PersonalMessage, request.Comments, Session);
                response = new AwardResponse { IsSuccess = true };
            }
            catch (Exception e)
            {
                response = new AwardResponse { IsSuccess = false };
            }
            return response;
        }
    }
}