namespace HallScheduler.Services.Data.Services
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HallScheduler.Data.Models;
    using HallScheduler.Data.Contracts;

    public class EventsService : IEventsService
    {
        public EventsService(IRepository<Event> events)
        {
            this.Events = events;
        }

        public IRepository<Event> Events { get; set; }

        public IQueryable<Event> All()
        {
            return this.Events.All();
        }

        public void Add(Event model)
        {
            this.Events.Add(model);
            this.Events.SaveChanges();
        }

        public void Update(Event model)
        {
            this.Events.Update(model);
            this.Events.SaveChanges();
        }
    }
}