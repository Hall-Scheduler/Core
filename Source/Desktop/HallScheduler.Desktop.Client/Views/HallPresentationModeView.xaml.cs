namespace HallScheduler.Desktop.Client.Views
{
    using Models;
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
    using System.Windows.Threading;
    using ViewModels;    

    public partial class HallPresentationModeView : Window
    {
        public HallPresentationModeView(SelectHallViewModel vm)
        {
            this.InitializeComponent();
            this.ViewModel = vm;
            this.Background =
                new ImageBrush(
                    new BitmapImage(
                        new Uri(BaseUriHelper.GetBaseUri(this), "../Images/PresentationMode-1.jpg")));

            this.StartClock();
            this.StartEventsPresentation();
            this.StartBackgroundSwitch();
        }

        public Random Random { get; set; } = new Random();

        public SelectHallViewModel ViewModel { get; set; }

        private DispatcherTimer ClockTimer { get; set; }

        private DispatcherTimer EventsTimer { get; set; }

        private DispatcherTimer BackgroundTimer { get; set; }

        private void StartClock()
        {
            this.ClockTimer = new DispatcherTimer();
            this.ClockTimer.Tick += ClockTimer_Tick;
            this.ClockTimer.Interval = new TimeSpan(0, 0, 1);
            this.ClockTimer.Start();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            this.txtCurrentTime.Text = $"Current time - {DateTime.Now.ToLongTimeString()}";
        }

        private void StartBackgroundSwitch()
        {
            this.BackgroundTimer = new DispatcherTimer();
            this.BackgroundTimer.Tick += BackgroundSwitchTimer_Tick;
            this.BackgroundTimer.Interval = new TimeSpan(0, 0, 5);
            this.BackgroundTimer.Start();
        }

        private void BackgroundSwitchTimer_Tick(object sender, EventArgs e)
        {
            var imageIndex = this.Random.Next(1, 5);

            this.Background = 
                new ImageBrush(
                    new BitmapImage(
                        new Uri(BaseUriHelper.GetBaseUri(this), $"../Images/PresentationMode-{imageIndex}.jpg")));
        }

        private void StartEventsPresentation()
        {
            this.EventsTimer = new DispatcherTimer();
            this.EventsTimer.Tick += EventsTimer_Tick;
            this.EventsTimer.Interval = new TimeSpan(0, 0, 1);
            this.EventsTimer.Start();
        }

        private void EventsTimer_Tick(object sender, EventArgs e)
        {
            var currentDay = DateTime.Now.DayOfWeek;
            var currentTimeTotalMilliseconds = DateTime.Now.TimeOfDay.TotalMilliseconds;
            var schedule = this.ViewModel.WeeklySchedule[this.GetDayOfWeekIndex(currentDay)];

            for (int i = 0; i < schedule.Schedule.Count; i++)
            {
                var checkedEvent = schedule.Schedule[i];

                if (checkedEvent.StartsAt.TotalMilliseconds < currentTimeTotalMilliseconds &&
                    currentTimeTotalMilliseconds < checkedEvent.EndsAt.TotalMilliseconds)
                {
                    // We have an event currently being situated
                    this.txtCurrentLecturer.Text = $"Lecturer - {checkedEvent.LecturerName}";
                    this.txtCurrentTopic.Text = $"{checkedEvent.Topic}";
                    this.SetupUpcomingEvents(schedule, currentTimeTotalMilliseconds, i);
                    break;
                }
                else
                {
                    this.txtCurrentTopic.Text = "There are currently no events";
                    this.txtCurrentLecturer.Text = "";
                    this.SetupUpcomingEvents(schedule, currentTimeTotalMilliseconds);
                }
            }
        }

        private void SetupUpcomingEvents(DailySchedule schedule, double currentTimeTotalMilliseconds, int startIndex = 0)
        {
            for (int i = startIndex; i < schedule.Schedule.Count; i++)
            {
                var checkedEvent = schedule.Schedule[i];

                if (checkedEvent.StartsAt.TotalMilliseconds > currentTimeTotalMilliseconds)
                {
                    // We have a next event
                    this.txtNextEvent.Text = $"Comming next in {checkedEvent.StartsAt.ToString()}{Environment.NewLine}{checkedEvent.Topic}{Environment.NewLine}Lecturer - {checkedEvent.LecturerName}{Environment.NewLine}";
                    break;
                }
                else
                {
                    this.txtNextEvent.Text = $"Comming next{Environment.NewLine}There are currently no upcoming events";
                }
            }
        }

        private int GetDayOfWeekIndex(DayOfWeek currentDay)
        {
            switch (currentDay)
            {
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
                default:
                    return -1;
            }
        }
    }
}
