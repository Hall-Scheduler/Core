﻿namespace HallScheduler.Desktop.Client.Views
{
    using Infrastructure.Helpers;
    using Ninject;
    using Providers;
    using Services.Contracts;
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
    using ViewModels;

    /// <summary>
    /// Interaction logic for SelectHallView.xaml
    /// </summary>
    public partial class SelectHallView : Window
    {
        public SelectHallView()
        {
            this.InitializeComponent();
            this.DataContext = new SelectHallViewModel();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NinjectHelper.Kernel.Get<IIdentityService>().ClearIdentity();

            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }
    }
}
