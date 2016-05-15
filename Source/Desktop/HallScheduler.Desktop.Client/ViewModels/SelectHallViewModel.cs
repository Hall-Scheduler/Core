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
    using Views;

    public class SelectHallViewModel : INotifyPropertyChanged
    {
        private HallsProvider queryProvider;
        private HallBriefDTO selectedItem;
        private List<DailySchedule> weeklySchedule;
        private string selectedValue = " ";
        private bool isSchedulingEnabled = false;

        public SelectHallViewModel()
        {
            this.HttpService = NinjectHelper.Kernel.Get<IHttpService>();
            this.IdentityService = NinjectHelper.Kernel.Get<IIdentityService>();
            this.isSchedulingEnabled = this.IdentityService.UserIdentity.Roles
                .Intersect(Enum.GetValues(typeof(IdentityRoleType)).Cast<IdentityRoleType>().ToList())
                .Count() > 0;

            this.QueryProvider = new HallsProvider(this.HttpService);
            this.ClearSchedule(-1);
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

                this.ClearSchedule(responseData.Id);

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
            int monday = 0;
            int tuesday = 1;
            int wednesday = 2;
            int thursday = 3;
            int friday = 4;

            // TODO: Refactor me please
            var mondaySchedule = this.WeeklySchedule[monday];
            for (int j = 0; j < schedule.Monday.Count; j++)
            {
                var position = mondaySchedule.Schedule.FindIndex(x => x.StartsAt.Equals(schedule.Monday[j].StartsAt));
                mondaySchedule.Schedule[position] = schedule.Monday[j];
            }

            var tuesdaySchedule = this.WeeklySchedule[tuesday];
            for (int j = 0; j < schedule.Tuesday.Count; j++)
            {
                var position = tuesdaySchedule.Schedule.FindIndex(x => x.StartsAt.Equals(schedule.Tuesday[j].StartsAt));
                tuesdaySchedule.Schedule[position] = schedule.Tuesday[j];
            }

            var wednesdaySchedule = this.WeeklySchedule[wednesday];
            for (int j = 0; j < schedule.Wednesday.Count; j++)
            {
                var position = wednesdaySchedule.Schedule.FindIndex(x => x.StartsAt.Equals(schedule.Wednesday[j].StartsAt));
                wednesdaySchedule.Schedule[position] = schedule.Wednesday[j];
            }

            var thursdaySchedule = this.WeeklySchedule[thursday];
            for (int j = 0; j < schedule.Thursday.Count; j++)
            {
                var position = thursdaySchedule.Schedule.FindIndex(x => x.StartsAt.Equals(schedule.Thursday[j].StartsAt));
                thursdaySchedule.Schedule[position] = schedule.Thursday[j];
            }

            var fridaySchedule = this.WeeklySchedule[friday];
            for (int j = 0; j < schedule.Friday.Count; j++)
            {
                var position = fridaySchedule.Schedule.FindIndex(x => x.StartsAt.Equals(schedule.Friday[j].StartsAt));
                fridaySchedule.Schedule[position] = schedule.Friday[j];
            }

            this.NotifyPropertyChanged(PropertyHelper.GetPropertyName(() => this.WeeklySchedule));
        }

        public void ClearSchedule(int hallId)
        {
            this.WeeklySchedule = new List<DailySchedule>()
                {
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Monday, hallId) { DayOfWeek= DayOfWeek.Monday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Tuesday, hallId) { DayOfWeek= DayOfWeek.Tuesday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Wednesday, hallId) { DayOfWeek= DayOfWeek.Wednesday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Thursday, hallId) { DayOfWeek= DayOfWeek.Thursday },
                    new DailySchedule(this.isSchedulingEnabled, DayOfWeek.Friday, hallId) { DayOfWeek= DayOfWeek.Friday }
                };
        }
    }
}
