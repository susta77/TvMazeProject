using System.Web.Http;
using WebActivatorEx;
using Bogem.Hedyla;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Bogem.Hedyla
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {

                        c.SingleApiVersion("v1", "Test.TvMaze");
                        c.ApiKey("Authorization")
                                        .Description("Token de autenticación JWT")
                                        .Name("Authorization")
                                        .In("header");

                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
