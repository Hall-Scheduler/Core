namespace HallScheduler.Desktop.Client.Views
{
    using Infrastructure.Helpers;
    using Ninject;
    using Providers;
    using Server.DataTransferObjects.Events;
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
            this.ViewModel = new SelectHallViewModel();
            this.DataContext = this.ViewModel;
        }

        public SelectHallViewModel ViewModel { get; set; }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            NinjectHelper.Kernel.Get<IIdentityService>().ClearIdentity();
            var loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        protected void HandleListViewItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ViewModel.WeeklySchedule[0].HallId > 0)
            {
                var eventModel = ((ListViewItem)sender).Content as EventDTО; //Casting back to the binded EventDTM
                var eventId = eventModel.Id;

                var editEventView = new EditEventView(eventModel, this.ViewModel);

                // Workaround to prevent focus switching between windows
                Action showAction = () => editEventView.Show();
                this.Dispatcher.BeginInvoke(showAction);
            }
        }
    }
}
