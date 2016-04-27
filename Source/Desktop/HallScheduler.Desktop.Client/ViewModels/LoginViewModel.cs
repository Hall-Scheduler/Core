namespace HallScheduler.Desktop.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;

    using HallScheduler.Desktop.Infrastructure.ExtensionMethods;

    public class LoginViewModel
    {
        public string UserName { get; set; }

        public SecureString Password { get; set; }
    }
}
