namespace HallScheduler.Desktop.Client.ViewModels
{
    using Infrastructure.Helpers;
    using Helpers;
    using Models;
    using Server.DataTransferObjects.Events;
    using Server.DataTransferObjects.Halls;
    using Services;
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
        private HallDetailedDTO hallDetails;
        private List<DailySchedule> weeklySchedule;
        private string selectedValue = " ";

        public SelectHallViewModel()
        {
            this.HttpService = new HttpService();
            this.QueryProvider = new LinqToEntitiesProvider(this.HttpService);
            this.InitializeEmptySchedule();
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

        public HttpService HttpService { get; set; }

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
                var response = await this.HttpService.Get<ResponseResult<HallScheduleDTO>>(url);

                var responseData = (response as ResponseResult<HallScheduleDTO>).Data;

                if (responseData != null)
                {
                    this.InitializeEmptySchedule();
                    this.ParseSchedule(responseData);
                }
                else
                {
                    this.NotifyPropertyChanged(PropertyHelpers.GetPropertyName(() => this.WeeklySchedule));
                }
            }
            catch (Exception exc)
            {
                // TODO: Notify user for HTTP request error.
            }
        }

        public void ParseSchedule(HallScheduleDTO schedule)
        {
            int monday = 0;
            int tuesday = 1;
            int wednesday = 2;
            int thursday = 3;
            int friday = 4;

            // TODO: Refactor me
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

            this.NotifyPropertyChanged(PropertyHelpers.GetPropertyName(() => this.WeeklySchedule));
        }

        public void InitializeEmptySchedule()
        {
            this.WeeklySchedule = new List<DailySchedule>()
                {
                    new DailySchedule() { DayOfWeek= DayOfWeek.Monday },
                    new DailySchedule() { DayOfWeek= DayOfWeek.Tuesday },
                    new DailySchedule() { DayOfWeek= DayOfWeek.Wednesday },
                    new DailySchedule() { DayOfWeek= DayOfWeek.Thursday },
                    new DailySchedule() { DayOfWeek= DayOfWeek.Friday }
                };
        }
    }

    // Extract to another class
    public class DailySchedule
    {
        public DailySchedule()
        {
            this.Schedule = new List<EventDTM>()
            {
                new EventDTM(){ StartsAt = new TimeSpan(7,30,0), EndsAt = new TimeSpan(9,15,0) },
                new EventDTM(){ StartsAt = new TimeSpan(9,30,0), EndsAt = new TimeSpan(11,15,0) },
                new EventDTM(){ StartsAt = new TimeSpan(11,30,0), EndsAt = new TimeSpan(13,15,0) },
                new EventDTM(){ StartsAt = new TimeSpan(13,45,0), EndsAt = new TimeSpan(15,30,0) },
                new EventDTM(){ StartsAt = new TimeSpan(15,45,0), EndsAt = new TimeSpan(17,30,0) },
                new EventDTM(){ StartsAt = new TimeSpan(17,45,0), EndsAt = new TimeSpan(19,30,0) },
                new EventDTM(){ StartsAt = new TimeSpan(19,45,0), EndsAt = new TimeSpan(21,30,0) }
            };
        }

        public DayOfWeek DayOfWeek { get; set; }

        public List<EventDTM> Schedule { get; set; }
    }
}