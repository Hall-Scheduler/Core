namespace CompanySystem.Server.API
{
    using App_Start;
    using System.Web.Http;
    using System.Reflection;
    using CompanySystem.Common.Constants;
    using Infrastructure.Mapping;

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