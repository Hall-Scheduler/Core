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
        public EventsService(
            IRepository<Event> events, 
            IRepository<Hall> halls)
        {
            this.Events = events;
            this.Halls = halls;
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
            scheduleTree.BuildTreeFromList(schedule);

            // Try insert
            var canInsertEvent = scheduleTree.CanInsert(modelAsScheduleNode);
            
            if (canInsertEvent)
            {
                if (model.StartsAt.TotalMilliseconds < model.EndsAt.TotalMilliseconds)
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
                    return API.InvalidModel;
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
            scheduleTree.BuildTreeFromList(schedule);

            // Try update
            var canInsertUpdatedEvent = scheduleTree.CanInsert(modelAsScheduleNode);
            if(canInsertUpdatedEvent)
            {
                if (model.StartsAt.TotalMilliseconds < model.EndsAt.TotalMilliseconds)
                {
                    try
                    {
                        var eventToUpdate = this.Events.All().FirstOrDefault(x => x.Id == model.Id);
                        eventToUpdate.LecturerId = model.LecturerId;
                        eventToUpdate.StartsAt = model.StartsAt;
                        eventToUpdate.EndsAt = model.EndsAt;
                        eventToUpdate.DayOfWeek = model.DayOfWeek;
                        eventToUpdate.Topic = model.Topic;
                        eventToUpdate.HallId = model.HallId;
                        this.Events.Update(eventToUpdate);
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
                    return API.InvalidModel;
                }
            }
            else
            {
                return API.Overlap;
            }
        }

        public void DeleteById(int eventId)
        {
            var eventToDelete = this.Events.All().FirstOrDefault(x => x.Id == eventId);
            this.Delete(eventToDelete);
        }

        public void Delete(Event model)
        {
            this.Events.Delete(model);
            this.Events.SaveChanges();
        }

        public Event GetById(int eventId)
        {
            return this.Events.All().FirstOrDefault(x => x.Id == eventId);
        }
    }
}