using ServiceStack.CacheAccess;

namespace Example.Api.Repository
{
    public class AwardRepository
    {
        public void CreateAward(int pointAmount, string personalMessage, string comments, ISession session)
        {
            //call database here
        }
    }
}