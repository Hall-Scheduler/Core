namespace HallScheduler.Desktop.Client.ViewModels
{
    using Commands;
    using Infrastructure.Helpers;
    using Models;
    using Ninject;
    using Providers;
    using Server.DataTransferObjects.Events;
    using Server.DataTransferObjects.Halls;
    using Server.DataTransferObjects.Users;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    public class EditEventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public EditEventViewModel(SelectHallViewModel caller)
        {
            this.Caller = caller;
            this.HttpService = NinjectHelper.Kernel.Get<IHttpService>();
            this.IdentityService = NinjectHelper.Kernel.Get<IIdentityService>();
            this.LecturersProvider = new LecturersProvider(this.HttpService);
            this.HallsProvider = new HallsProvider(this.HttpService);
        }

        public SelectHallViewModel Caller { get; set; }

        public IHttpService HttpService { get; set; }

        public IIdentityService IdentityService { get; set; }

        private EventDTО _selectedEventItem;
        public EventDTО SelectedEventItem
        {
            get
            {
                return this._selectedEventItem;
            }
            set
            {
                this._selectedEventItem = value;
                this.NotifyPropertyChanged();
            }
        }

        private LecturersProvider _lecturersProvider;
        public LecturersProvider LecturersProvider
        {
            get
            {
                return this._lecturersProvider;
            }
            set
            {
                this._lecturersProvider = value;
                this.NotifyPropertyChanged();
            }
        }

        private HallsProvider _hallsProvider;
        public HallsProvider HallsProvider
        {
            get
            {
                return this._hallsProvider;
            }
            set
            {
                this._hallsProvider = value;
                this.NotifyPropertyChanged();
            }
        }

        private UserBriefDTO _selectedLecturerItem;
        public UserBriefDTO SelectedLecturerItem
        {
            get
            {
                return this._selectedLecturerItem;
            }
            set
            {
                this._selectedLecturerItem = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _selectedLecturerValue;
        public string SelectedLecturerValue
        {
            get
            {
                return this._selectedLecturerValue;
            }
            set
            {
                this._selectedLecturerValue = this.SelectedLecturerItem?.FullName;
                this.NotifyPropertyChanged();
            }
        }

        private string _topic;
        public string Topic
        {
            get
            {
                return this._topic;
            }
            set
            {
                this._topic = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get
            {
                return this._notificationMessage;
            }
            set
            {
                this._notificationMessage = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _notificationMessageColor;
        public string NotificationMessageColor
        {
            get
            {
                return this._notificationMessageColor;
            }
            set
            {
                this._notificationMessageColor = value;
                this.NotifyPropertyChanged();
            }
        }

        public ICommand UpdateEventCommand
        {
            get
            {
                return new ActionCommand(this.UpdateEvent);
            }
        }

        private async void UpdateEvent()
        {
            try
            {
                var mustUpdateModel = this.UpdateEventModel();

                if(mustUpdateModel)
                {
                    var url = "http://localhost:38013/api/Events/Update";
                    var response = await this.HttpService.PostAsJsonAsync<ResponseResult<bool>>(url, this.SelectedEventItem);
                    var isUpdated = (response as ResponseResult<bool>).Data;

                    if (isUpdated)
                    {
                        this.SetNotificationMessage("Green", "Event updated successfully.");
                        this.Caller.LoadWeeklySchedule();
                    }
                    else
                    {
                        this.SetNotificationMessage("Red", "There was a problem updating the event.");
                    }
                }
                else
                {
                    this.SetNotificationMessage("Blue", "There is nothing to update here.");
                }
            }
            catch (Exception exc)
            {
                this.SetNotificationMessage("Red", exc.ToString());
            }
        }

        private bool UpdateEventModel()
        {
            var mustUpdateItem = false;

            if (this.SelectedLecturerItem != null)
            {
                this.SelectedEventItem.LecturerId = this.SelectedLecturerItem.Id;
                this.SelectedEventItem.LecturerName = this.SelectedLecturerItem.FullName;
                mustUpdateItem = true;
            }
            if (!String.IsNullOrEmpty(this.Topic))
            {
                this.SelectedEventItem.Topic = this.Topic;
                mustUpdateItem = true;
            }

            return mustUpdateItem;
        }

        private void SetNotificationMessage(string color, string message)
        {
            this.NotificationMessageColor = color;
            this.NotificationMessage = message;
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
