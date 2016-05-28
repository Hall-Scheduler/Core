namespace HallScheduler.Desktop.Client.ViewModels
{
    using Commands;
    using Data.Common.Enumerations;
    using Infrastructure.Helpers;
    using Models;
    using Ninject;
    using Providers;
    using Server.DataTransferObjects.Events;
    using Server.DataTransferObjects.Halls;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Views;

    public class SelectHallViewModel : INotifyPropertyChanged
    {
        private HallsProvider queryProvider;
        private HallBriefDTO selectedItem;
        private List<DailySchedule> weeklySchedule;
        private string selectedValue = " ";
        private bool isSchedulingEnabled = false;

        //Start timer to update the collection every 3 seconds
        public SelectHallViewModel()
        {
            this.HttpService = NinjectHelper.Kernel.Get<IHttpService>();
            this.IdentityService = NinjectHelper.Kernel.Get<IIdentityService>();
            this.isSchedulingEnabled = this.IdentityService.UserIdentity.Roles
                .Intersect(Enum.GetValues(typeof(IdentityRoleType)).Cast<IdentityRoleType>().ToList())
                .Count() > 0;
            this.QueryProvider = new HallsProvider(this.HttpService);
        }

        public HallsProvider QueryProvider
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

        public IHttpService HttpService { get; set; }

        public IIdentityService IdentityService { get; set; }

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
                this.LoadWeeklySchedule();
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

        private EventDTО selectedEvent;
        public EventDTО SelectedEvent
        {
            get
            {
                return this.selectedEvent;
            }
            set
            {
                this.selectedEvent = value;
                this.NotifyPropertyChanged();
            }
        }

        public List<DailySchedule> WeeklySchedule
        {
            get
            {
                return this.weeklySchedule;
            }
            set
            {
                this.weeklySchedule = value;
                this.NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadWeeklySchedule()
        {
            // TODO: Cache current request results
            try
            {
                var url = "http://localhost:38013/api/Halls/Schedule?hallId=" + this.SelectedItem.Id;
                var response = await this.HttpService.GetAsync<ResponseResult<HallScheduleDTO>>(url);
                var responseData = (response as ResponseResult<HallScheduleDTO>).Data;

                this.RefreshSchedule(responseData.Id);

                if (responseData != null)
                {
                    this.ParseSchedule(responseData);
                }
                else
                {
                    this.NotifyPropertyChanged(PropertyHelper.GetPropertyName(() => this.WeeklySchedule));
                }
            }
            catch (Exception exc)
            {
                // TODO: Notify user for HTTP request error.
                var error = exc.ToString();
            }
        }

        public void ParseSchedule(HallScheduleDTO schedule)
        {
            Comparison<EventDTО> eventsComparer = (x, y) => x.StartsAt.TotalMilliseconds.CompareTo(y.StartsAt.TotalMilliseconds);

            schedule.Monday.Sort(eventsComparer);
            schedule.Tuesday.Sort(eventsComparer);
            schedule.Wednesday.Sort(eventsComparer);
            schedule.Thursday.Sort(eventsComparer);
            schedule.Friday.Sort(eventsComparer);

            this.WeeklySchedule[0].Schedule = schedule.Monday;
            this.WeeklySchedule[1].Schedule = schedule.Tuesday;
            this.WeeklySchedule[2].Schedule = schedule.Wednesday;
            this.WeeklySchedule[3].Schedule = schedule.Thursday;
            this.WeeklySchedule[4].Schedule = schedule.Friday;

            this.NotifyPropertyChanged(PropertyHelper.GetPropertyName(() => this.WeeklySchedule));
        }

        public void RefreshSchedule(int hallId)
        {
            this.WeeklySchedule = new List<DailySchedule>()
                {
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Monday, hallId) { DayOfWeek = DayOfWeek.Monday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Tuesday, hallId) { DayOfWeek = DayOfWeek.Tuesday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Wednesday, hallId) { DayOfWeek = DayOfWeek.Wednesday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Thursday, hallId) { DayOfWeek = DayOfWeek.Thursday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Friday, hallId) { DayOfWeek = DayOfWeek.Friday }
                };
        }
    }
}
