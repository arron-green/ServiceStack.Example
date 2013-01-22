using System;
using System.Web;
using Example.Api.App_Start;

namespace Example.Api
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }
    }
}