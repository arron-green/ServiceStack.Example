using System.Collections.Generic;
using Example.Api.Repository;
using Example.Api.Services;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.Redis;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace Example.Api.App_Start
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("Example.Api", typeof(AwardService).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            //Set JSON web services to return idiomatic JSON camelCase properties
            JsConfig.EmitCamelCaseNames = true;

            //Register Typed Config some services might need to access
            var appConfig = new AppConfig(new AppSettings());

            //appconfig will contain properties from web.config
            container.Register(appConfig);

            //inform api that this will be using BasicAuth to authenticate/authorize users
            Plugins.Add(new AuthFeature(
                () => new AuthUserSession(),
                new IAuthProvider[] { new BasicAuthProvider(), }));

            //add registration functionality (user will need admin role to access this)
            Plugins.Add(new RegistrationFeature());

            //add validation using fluent validation package
            Plugins.Add(new ValidationFeature());

            //register service to validate
            container.RegisterValidators(typeof(AwardService).Assembly);

            if (appConfig.UseRedis){
                //setup cache client to user redis
                container.Register<IRedisClientsManager>(c => new PooledRedisClientManager(appConfig.RedisReadWriteHosts.ToArray()));
                container.Register<ICacheClient>(c => c.Resolve<IRedisClientsManager>().GetCacheClient());

                //setup redis for authentication repository
                container.Register<IUserAuthRepository>(c => new RedisAuthRepository(c.Resolve<IRedisClientsManager>()));
            }
            else
            {
                //setup cache client to be InMemory
                container.Register<ICacheClient>(c => new MemoryCacheClient());

                //setup authentication repository to be InMemory
                container.Register<IUserAuthRepository>(c => new InMemoryAuthRepository());
            }
            

            //seed possible users
            SeedUsers(container.Resolve<IUserAuthRepository>());

            //register any repository classes here
            container.RegisterAutoWired<AwardRepository>();
        }

        private static void SeedUsers(IUserAuthRepository users)
        {
            //check to see if user exists
            if (users.GetUserAuthByUserName("admin.user") == null)
            {
                //create user
                const string pwd = "password";
                string hash, salt;
                new SaltedHash().GetHashAndSaltString(pwd, out hash, out salt);
                users.CreateUserAuth(new UserAuth
                    {
                        Id = 1,
                        DisplayName = "Admin User",
                        Email = "admin.user@example.com",
                        UserName = "admin.user",
                        FirstName = "Admin",
                        LastName = "User",
                        PasswordHash = hash,
                        Salt = salt,
                        Roles = new List<string> {RoleNames.Admin},
                        Permissions = new List<string> {"God"}
                    }, pwd);
            }

            //check to see if user exists
            if (users.GetUserAuthByUserName("joe.budget") == null)
            {
                //create user
                const string pwd = "password";
                string hash, salt;
                new SaltedHash().GetHashAndSaltString(pwd, out hash, out salt);
                users.CreateUserAuth(new UserAuth
                    {
                        Id = 1,
                        DisplayName = "Joe BudgetHolder",
                        Email = "joe.budgetholder@example.com",
                        UserName = "joe.budget",
                        FirstName = "Joe",
                        LastName = "BudgetHolder",
                        PasswordHash = hash,
                        Salt = salt,
                        Roles = new List<string> {"BudgetUser"},
                        Permissions = new List<string> {"GiveAward"}
                    }, pwd);
            }

            //check to see if user exists
            if (users.GetUserAuthByUserName("jane.employee") == null)
            {
                //create user
                const string pwd = "password";
                string hash, salt;
                new SaltedHash().GetHashAndSaltString(pwd, out hash, out salt);
                users.CreateUserAuth(new UserAuth
                    {
                        Id = 1,
                        DisplayName = "Jane Employee",
                        Email = "jane.employee@gmail.com",
                        UserName = "jane.employee",
                        FirstName = "Jane",
                        LastName = "Employee",
                        PasswordHash = hash,
                        Salt = salt,
                    }, pwd);
            }
        }
    }
}