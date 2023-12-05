using Autofac.Integration.WebApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin;
using Owin;
using System.Text;
using System.Web.Http;
using Microsoft.Owin.Security;
using MongoDB.Driver;
using Autofac;

[assembly: OwinStartup(typeof(Test.TvMaze.App_Start.Startup))]

namespace Test.TvMaze.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ContainerConfig.Configure();

            var config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var key = Encoding.UTF8.GetBytes(System.Configuration.ConfigurationManager.AppSettings["KeySecret"]);
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }
            });
            var container = ContainerConfig.Configure();
            var builder = new ContainerBuilder();

            builder.Register(c =>
            {
                var configuration = c.Resolve<System.Configuration.Configuration>();
                var connectionString = configuration.AppSettings.Settings["MongoDb"].Value;
                return new MongoClient(connectionString);
            }).As<IMongoClient>().SingleInstance();

            builder.Register(c =>
            {
                var client = c.Resolve<IMongoClient>();
                var configuration = c.Resolve<System.Configuration.Configuration>();
                var databaseName = configuration.AppSettings.Settings["MongoDb:TvMazeDb"].Value;
                return client.GetDatabase(databaseName);
            }).InstancePerLifetimeScope();


        }
    }
}
