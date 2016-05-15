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
    public partial class EditEventView : Window
    {
        public EditEventView(EventDTО eventModel)
        {
            this.InitializeComponent();
            this.ViewModel = new EditEventViewModel();
            this.ViewModel.SelectedEventItem = eventModel;
            this.DataContext = this.ViewModel;

            this.tbTopic.Text = eventModel.Topic;
        }

        public EditEventViewModel ViewModel { get; set; }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
