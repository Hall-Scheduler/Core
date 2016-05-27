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
    public class ScheduleEventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ScheduleEventViewModel(SelectHallViewModel caller)
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

        private string _dayOfWeek;
        public string DayOfWeek
        {
            get
            {
                return this._dayOfWeek;
            }
            set
            {
                this._dayOfWeek = value;
                this.NotifyPropertyChanged();
            }
        }

        public List<string> DaysOfTheWeek
        {
            get
            {
                return Enum.GetValues(typeof(DayOfWeek))
                    .Cast<DayOfWeek>()
                    .Select(x => x.ToString())
                    .ToList();
            }
        }

        private string _startsAt;
        public string StartsAt
        {
            get
            {
                return this._startsAt;
            }
            set
            {
                this._startsAt = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _endsAt;
        public string EndsAt
        {
            get
            {
                return this._endsAt;
            }
            set
            {
                this._endsAt = value;
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

        public ICommand CreateEventCommand
        {
            get
            {
                return new ActionCommand(this.AddEvent);
            }
        }

        private async void UpdateEvent()
        {
            try
            {
                var mustUpdateModel = this.UpdateEventModel();

                if (mustUpdateModel)
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

        private async void AddEvent()
        {
            try
            {
                var eventToAdd = new EventDTО
                {
                    DayOfWeek = this.DayOfWeek,
                    StartsAt = this.ParseTimeString(this.StartsAt),
                    EndsAt = this.ParseTimeString(this.EndsAt),
                    HallId = this.Caller.SelectedItem.Id,
                    LecturerId = this.SelectedLecturerItem.Id,
                    Topic = this.Topic
                };
                var url = "http://localhost:38013/api/Events/Create";
                var response = await this.HttpService.PostAsJsonAsync<ResponseResult<EventDTО>>(url, eventToAdd);
                var responseResult = (response as ResponseResult<EventDTО>);
                var isCreated = responseResult.Data.Id > 0;

                if (isCreated)
                {
                    this.SetNotificationMessage("Green", responseResult.Message);
                    this.Caller.LoadWeeklySchedule();
                }
                else
                {
                    this.SetNotificationMessage("Red", responseResult.Message);
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
