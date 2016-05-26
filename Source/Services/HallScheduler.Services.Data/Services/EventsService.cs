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
    using HallScheduler.Data.Common;
    using Server.Common.Constants;
    public class EventsService : IEventsService
    {
        public EventsService(IRepository<Event> events)
        {
            this.Events = events;
        }

        public IRepository<Event> Events { get; set; }

        public IRepository<Hall> Halls { get; set; }

        public IQueryable<Event> All()
        {
            return this.Events.All();
        }

        public int Add(Event model)
        {
            // Prepare data
            var modelAsScheduleNode = new ScheduleNode<Event>(model);
            var schedule = this.Halls.GetById(model.HallId).Schedule
                .Where(x => x.DayOfWeek == model.DayOfWeek)
                .Select(x => new ScheduleNode<Event>(x))
                .ToList();

            // Build tree
            var scheduleTree = new ScheduleTree<Event>();
            scheduleTree.BuildFromList(schedule);

            // Try insert
            var insertionSuccessfull = scheduleTree.CanInsert(modelAsScheduleNode);
            if (insertionSuccessfull)
            {
                try
                {
                    this.Events.Add(model);
                    this.Events.SaveChanges();
                    return model.Id;
                }
                catch (Exception)
                {
                    return API.Conflict;
                }
            }
            else
            {
                return API.Overlap;
            }
        }

        public int Update(Event model)
        {
            // Prepare data
            var modelAsScheduleNode = new ScheduleNode<Event>(model);
            var schedule = this.Halls.GetById(model.HallId).Schedule
                .Where(x => x.DayOfWeek == model.DayOfWeek && x.Id != model.Id)
                .Select(x => new ScheduleNode<Event>(x))
                .ToList();

            // Build tree
            var scheduleTree = new ScheduleTree<Event>();
            scheduleTree.BuildFromList(schedule);

            // Try update
            var insertionSuccessfull = scheduleTree.CanInsert(modelAsScheduleNode);
            if(insertionSuccessfull)
            {
                try
                {
                    this.Events.Update(model);
                    this.Events.SaveChanges();
                    return model.Id;
                }
                catch (Exception)
                {
                    return API.Conflict;
                }
              
            }
            else
            {
                return API.Overlap;
            }
        }
    }
}