namespace HallScheduler.Desktop.Client.Configuration
{
    using Ninject.Modules;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            // Create bindings
            //Bind<IFlagsDbContext>().To<FlagsDbContext>().InSingletonScope();
            //Bind<IGameplay>().To<GameplayViewModel>();
            //Bind<IStartScreen>().To<StartScreenViewModel>();
            //Bind<IScoreboard>().To<ScoreboardViewModel>();
            //Bind<IEngine>().To<EngineViewModel>().InSingletonScope();
        }
    }
}