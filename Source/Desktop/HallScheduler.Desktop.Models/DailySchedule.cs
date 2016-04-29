namespace HallScheduler.Desktop.Models
{
    using System;
    using System.Collections.Generic;

    using Server.DataTransferObjects.Events;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DailySchedule : INotifyPropertyChanged
    {
        private EventDTM selectedItem;

        public DailySchedule(bool isSchedulingEnabled) 
            : this()
        {
            this.IsSchedulingEnabled = isSchedulingEnabled;
        }

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

            this.SelectedItem = Schedule[0];
        }

        public DayOfWeek DayOfWeek { get; set; }

        public List<EventDTM> Schedule { get; set; }

        public bool IsSchedulingEnabled { get; set; }

        public EventDTM SelectedItem
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
