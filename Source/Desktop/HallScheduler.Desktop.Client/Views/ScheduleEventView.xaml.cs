namespace HallScheduler.Desktop.Client.Views
{
    using HallScheduler.Desktop.Client.ViewModels;
    using HallScheduler.Server.DataTransferObjects.Events;
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

    /// <summary>
    /// Interaction logic for EditEventView.xaml
    /// </summary>
    public partial class ScheduleEventView : Window
    {
        public ScheduleEventView(SelectHallViewModel caller)
        {
            this.InitializeComponent();
            this.ViewModel = new ScheduleEventViewModel(caller);
            this.DataContext = this.ViewModel;
        }

        public ScheduleEventViewModel ViewModel { get; set; }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dropdownDays_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dropdownDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
