namespace HallScheduler.Desktop.Models
{
    using System;
    using System.Collections.Generic;

    using Server.DataTransferObjects.Events;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DailySchedule : INotifyPropertyChanged
    {
        public int HallId { get; set; }

        private EventDTО selectedItem;

        public DailySchedule(bool isSchedulingEnabled, DayOfWeek dayOfWeek, int hallId) 
            : this(dayOfWeek, hallId)
        {
            this.IsSchedulingEnabled = isSchedulingEnabled;
        }

        public DailySchedule(DayOfWeek dayOfWeek, int hallId)
        {
            //this.Schedule = new List<EventDTО>()
            //{
            //    new EventDTО(){ StartsAt = new TimeSpan(7,30,0), EndsAt = new TimeSpan(9,15,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId },
            //    new EventDTО(){ StartsAt = new TimeSpan(9,30,0), EndsAt = new TimeSpan(11,15,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId  },
            //    new EventDTО(){ StartsAt = new TimeSpan(11,30,0), EndsAt = new TimeSpan(13,15,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId  },
            //    new EventDTО(){ StartsAt = new TimeSpan(13,45,0), EndsAt = new TimeSpan(15,30,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId  },
            //    new EventDTО(){ StartsAt = new TimeSpan(15,45,0), EndsAt = new TimeSpan(17,30,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId  },
            //    new EventDTО(){ StartsAt = new TimeSpan(17,45,0), EndsAt = new TimeSpan(19,30,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId  },
            //    new EventDTО(){ StartsAt = new TimeSpan(19,45,0), EndsAt = new TimeSpan(21,30,0), DayOfWeek = dayOfWeek.ToString(), HallId = hallId  }
            //};

            this.HallId = hallId;
            this.DayOfWeek = dayOfWeek;
            this.Schedule = new List<EventDTО>();
        }

        public DayOfWeek DayOfWeek { get; set; }

        public List<EventDTО> Schedule { get; set; }

        public bool IsSchedulingEnabled { get; set; }

        public EventDTО SelectedItem
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
