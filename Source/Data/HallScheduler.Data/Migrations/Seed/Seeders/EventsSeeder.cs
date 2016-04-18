namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using Contexts;
    using HallScheduler.Common.Constants;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class EventsSeeder : ISeeder
    {
        public int Priority { get; set; } = 4;

        public Random Generator { get; set; } = new Random();

        public Event LastEvent { get; set; } = null;

        public int CurrentStartTime { get; set; } = 440;

        public int PreviousDuration { get; set; }

        public void Seed(HallSchedulerDbContext context)
        {
            if (!context.Events.Any())
            {
                var professorRole = context.Roles.Where(x => x.Name == Roles.Professor).FirstOrDefault();

                var eventTopics = new string[]
                {
                    "Introduction to Programming with C",
                    "Object-oriented programming with Java",
                    "Data structures and algorithms",
                    "Relational databases",
                    "Desktop applications with WPF",
                    "Mobile applications",
                    "Cross-platform mobile applications with Xamarin",
                    "Cross-platform mobile applications with NativeScript",
                    "Concurrent and parallel programming with C#"
                };

                var halls = context.Halls.ToList();
                var users = context.Users.Where(x => x.Roles.Any(z => z.RoleId == professorRole.Id)).ToList();
                var durationsInMinutes = new int[] { 45, 60, 120, 90, 150 };
                var startTimes = new int[] { 450, 510, 575, 680, 815 };
                var daysOfTheWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();

                for (int i = 1; i <= 50; i++)
                {
                    var hall = halls[i];

                    for (int j = 0; j < startTimes.Length; j++)
                    {
                        var lecturer = users[this.Generator.Next(0, users.Count)];
                        var dayOfWeek = daysOfTheWeek[this.Generator.Next(0, daysOfTheWeek.Count)];

                        var hallEvent = new Event()
                        {
                            LecturerId = lecturer.Id,
                            DayOfWeek = dayOfWeek,
                            HallId = hall.Id,
                            StartsAt = startTimes[j],
                            EndsAt = startTimes[j] + durationsInMinutes[j],
                            Topic = eventTopics[this.Generator.Next(0, eventTopics.Length)]
                        };

                        context.Events.AddOrUpdate(hallEvent);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}