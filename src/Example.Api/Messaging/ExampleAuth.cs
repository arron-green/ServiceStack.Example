using ServiceStack.Authentication.OpenId;
using ServiceStack.Configuration;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace Example.Api.Messaging
{
    //NOTE: when it comes time to hook into a custom authentication provider
    //public class ExampleAuth : AuthProvider
    //{
    //    public override bool IsAuthorized(IAuthSession session, IOAuthTokens tokens, Auth request = null)
    //    {
    //        return true;
    //    }

    //    public override object Authenticate(IServiceBase authService, IAuthSession session, Auth request)
    //    {
    //        return true;
    //    }
    //}

    //NOTE: when it comes time to implement an openid provider
    //public class ExampleOpenId : OpenIdOAuthProvider
    //{
    //    public ExampleOpenId(IResourceManager resourceManager, string name, string realm): base(resourceManager, name, realm)
    //    {
    //    }
    //}
}