using HallScheduler.Desktop.Client.ViewModels;
using HallScheduler.Desktop.Infrastructure.ExtensionMethods;
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

namespace HallScheduler.Desktop.Client.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            this.InitializeComponent();
            this.ViewModel = new LoginViewModel();
            // Get username and password
            // Save password
            // Make readonly
            // Dispose when no longer needed
        }

        public LoginViewModel ViewModel { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var password = this.passwordBox.SecurePassword;
            this.ViewModel.Password = password;
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
