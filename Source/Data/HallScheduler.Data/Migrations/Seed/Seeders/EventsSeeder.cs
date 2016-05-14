namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using Common.Enumerations;
    using Contexts;
    using HallScheduler.Common.Constants;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class EventsSeeder : ISeeder
    {
        public int Priority { get; set; } = 4;

        public Random Generator { get; set; } = new Random();

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

                var halls = context.Halls.Where(x => x.Type != HallType.Cabinet).ToList();
                var users = context.Users.Where(x => x.Roles.Any(z => z.RoleId == professorRole.Id)).ToList();
                var daysOfWeek = new List<DayOfWeek>(5)
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday
                };

                TimeSpan[] start = new TimeSpan[] {
                    new TimeSpan(7,30,0),
                    new TimeSpan(9,30,0),
                    new TimeSpan(11,30,0),
                    new TimeSpan(13,45,0),
                    new TimeSpan(15,45,0),
                    new TimeSpan(17,45,0),
                    new TimeSpan(19,45,0),
                };

                TimeSpan[] end = new TimeSpan[] {
                    new TimeSpan(9,15,0),
                    new TimeSpan(11,15,0),
                    new TimeSpan(13,15,0),
                    new TimeSpan(15,30,0),
                    new TimeSpan(17,30,0),
                    new TimeSpan(19,30,0),
                    new TimeSpan(21,30,0),
                };

                for (int i = 0; i < 50; i++)
                {
                    var hall = halls[i];

                    for (int k = 0; k < daysOfWeek.Count; k++)
                    {
                        var day = daysOfWeek[k];

                        for (int j = 0; j < start.Length; j++)
                        {
                            var lecturer = users[this.Generator.Next(0, users.Count)];
                            var topic = eventTopics[this.Generator.Next(0, eventTopics.Length)];
                            var hallEvent = new Event()
                            {
                                HallId = hall.Id,
                                LecturerId = lecturer.Id,
                                DayOfWeek = day,
                                StartsAt = start[j],
                                EndsAt = end[j],
                                Topic = topic
                            };

                            context.Events.AddOrUpdate(hallEvent);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
