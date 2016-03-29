﻿namespace HallScheduler.Server.API
{
    using App_Start;
    using HallScheduler.Common.Constants;
    using Infrastructure.Mapping;
    using System.Reflection;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();

            var autoMapperConfig = new AutoMapperConfig();
            var models = Assembly.Load(Assemblies.ServerDataTransferModels);
            autoMapperConfig.Execute(models);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}