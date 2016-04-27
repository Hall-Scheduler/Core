namespace HallScheduler.Desktop.Client.Configuration
{
    using Ninject.Modules;
    using Services;
    using Services.Contracts;

    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpService>().To<HttpService>();
            Bind<IIdentityService>().To<IdentityService>().InSingletonScope();
        }
    }
}
