namespace HallScheduler.Desktop.Client.Views
{
    using HallScheduler.Desktop.Client.Constants;
    using HallScheduler.Desktop.Client.ViewModels;
    using HallScheduler.Desktop.Infrastructure.ExtensionMethods;
    using HallScheduler.Desktop.Infrastructure.Helpers;
    using HallScheduler.Desktop.Models;
    using HallScheduler.Desktop.Services.Contracts;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            this.InitializeComponent();
        }

        private DispatcherTimer Timer { get; set; }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var response = await this.Login();
            if (response == null)
            {
                this.DisplayErrorMessage("Invalid credentials. Please try again.");
                return;
            }

            var identityLoaded = await this.LoadIdentity(response);
            if (!identityLoaded)
            {
                this.DisplayErrorMessage("Identity load failure. There is something wrong with our service.");
                return;
            }

            var nextPage = new SelectHallView();
            nextPage.Show();

            this.Close();
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisplayErrorMessage(string errorMessage)
        {
            this.validationErrorsTextBlock.Text = errorMessage;
            this.Timer = new DispatcherTimer();
            this.Timer.Tick += HideValidationErrorMessages;
            this.Timer.Interval = new TimeSpan(0, 0, 4);
            this.Timer.Start();
        }

        private void HideValidationErrorMessages(object sender, EventArgs e)
        {
            this.validationErrorsTextBlock.Text = string.Empty;
            this.Timer.Stop();
        }
    
        private async Task<AuthTokenModel> Login()
        {
            AuthTokenModel response = null;

            var username = this.usernameBox.Text;
            var password = this.passwordBox.SecurePassword;

            if ((username.Length >= 5 && username.Length <= 50) &&
                (password.Length >= 5 && password.Length <= 50))
            {
                var tokenUrl = URL.Token;
                var tokenData = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password.ConvertToUnsecureString())
                };

                var httpService = NinjectHelper.Kernel.Get<IHttpService>();
                response = (await httpService.Post<AuthTokenModel>(tokenUrl, tokenData)) as AuthTokenModel;
            }

            password.Dispose();
            return response;
        }

        private async Task<bool> LoadIdentity(AuthTokenModel auth)
        {
            var httpService = NinjectHelper.Kernel.Get<IHttpService>();
            var identityService = NinjectHelper.Kernel.Get<IIdentityService>();
            identityService.AuthToken = auth.Access_Token.ConvertToSecureString();
            var identityLoaded = await identityService.LoadIdentity(httpService);

            return identityLoaded;
        }
    }
}
