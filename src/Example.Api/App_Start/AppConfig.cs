using System.Collections.Generic;
using ServiceStack.Configuration;

namespace Example.Api.App_Start
{
    public class AppConfig
    {
        public AppConfig(IResourceManager appSettings)
        {
            RedisReadWriteHosts = appSettings.Get("RedisReadWriteHosts", new List<string>());
            UseRedis = appSettings.Get("UseRedis", false);
        }
        public List<string> RedisReadWriteHosts { get; set; }
        public bool UseRedis { get; set; }
    }
}