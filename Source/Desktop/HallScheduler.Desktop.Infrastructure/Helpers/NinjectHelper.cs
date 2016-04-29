namespace HallScheduler.Desktop.Infrastructure.Helpers
{
    using Ninject;
    using System;

    public class NinjectHelper
    {
        static NinjectHelper()
        {
            Kernel = new StandardKernel();
            Kernel.Load(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static StandardKernel Kernel { get; set; }
    }
}