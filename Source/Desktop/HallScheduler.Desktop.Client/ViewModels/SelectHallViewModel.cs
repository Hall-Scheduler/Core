namespace HallScheduler.Desktop.Client.ViewModels
{
    using Helpers;
    using Server.DataTransferModels.Halls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class SelectHallViewModel : INotifyPropertyChanged
    {
        private LinqToEntitiesProvider queryProvider;
        private HallBriefDTO selectedItem;
        private string selectedValue;

        public SelectHallViewModel()
        {
            this.QueryProvider = new LinqToEntitiesProvider();
        }

        public LinqToEntitiesProvider QueryProvider
        {
            get
            {
                return this.queryProvider;
            }
            set
            {
                this.queryProvider = value;
                this.NotifyPropertyChanged();
            }
        }

        public HallBriefDTO SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
                this.NotifyPropertyChanged();
            }
        }

        public string SelectedValue
        {
            get
            {
                return this.selectedValue;
            }
            set
            {
                this.selectedValue = this.SelectedItem?.Name;
                this.NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}