namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contexts;

    public class EventsSeeder : ISeeder
    {
        public Random Generator { get; set; } = new Random();

        public void Seed(HallSchedulerDbContext context)
        {
            if (!context.Events.Any())
            {
                var halls = context.Halls.ToList();

                for (int i = 1; i < 60; i++)
                {

                }
            }
        }
    }
}