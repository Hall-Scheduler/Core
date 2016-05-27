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

                //var editEventView = new ScheduleEventView(eventModel, this.ViewModel);

                // Workaround to prevent focus switching between windows
                //Action showAction = () => editEventView.Show();
                //this.Dispatcher.BeginInvoke(showAction);
            }
        }

        private void btnScheduleEvent_Click(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.WeeklySchedule != null && this.ViewModel.WeeklySchedule[0].HallId > 0)
            {
                var input = "07:15";
                var inputAsTimespan = this.ParseTimeString(input);
                var scheduleEventView = new ScheduleEventView(this.ViewModel);

                // Workaround to prevent focus switching between windows
                Action showAction = () => scheduleEventView.Show();
                this.Dispatcher.BeginInvoke(showAction);
            }
        }

        private TimeSpan ParseTimeString(string timespan)
        {
            var elements = timespan.Split(':');
            var hours = elements[0][0] == '0' ? int.Parse(elements[0][1].ToString()) : int.Parse(elements[0]);
            var minutes = elements[1][0] == '0' ? int.Parse(elements[1][1].ToString()) : int.Parse(elements[1]);
            var seconds = 0;

            return new TimeSpan(hours, minutes, seconds);
        }
    }
}
