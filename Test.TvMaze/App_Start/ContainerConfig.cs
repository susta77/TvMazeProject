using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using TvMaze.Project.Services;

namespace Test.TvMaze.App_Start
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Registrar controladores Web API.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Registrar clases BL, DAO, etc.
            Register(builder);

            // Construye el contenedor.
            IContainer container = builder.Build();

            // Establece el DependencyResolver para Web API.
            return container;
        }

        private static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<JwtService>().As<IJwtService>();
            string value = System.Configuration.ConfigurationManager.AppSettings["MongoDb"];
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            var databaseName = System.Configuration.ConfigurationManager.AppSettings["DBName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            builder.RegisterInstance(database).As<IMongoDatabase>();
        }



    }
}




