namespace HallScheduler.Desktop.Client.Views
{
    using Server.DataTransferObjects.Halls;
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
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for HallPresentationModeView.xaml
    /// </summary>
    public partial class HallPresentationModeView : Window
    {
        public HallPresentationModeView(HallBriefDTO hall)
        {
            this.InitializeComponent();
            this.ViewModel = hall;
            this.Background =
                new ImageBrush(
                    new BitmapImage(
                        new Uri(BaseUriHelper.GetBaseUri(this), "../Images/PresentationMode-1.jpg")));
        }

        public HallBriefDTO ViewModel { get; set; }

        private void StartPresentation()
        {

        }
    }
}
