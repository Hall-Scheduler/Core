﻿namespace HallScheduler.Server.API.Controllers
{
    using System.Web.Http;

    using Common.Constants;
    using Data.Models;
    using DataTransferObjects.Events;
    using Infrastructure;
    using Services.Data.Contracts;

    [RoutePrefix(API.Events)]
    public class EventsController : BaseController
    {
        public EventsController(IEventsService eventsService)
        {
            this.EventsService = eventsService;
        }

        public IEventsService EventsService { get; set; }

        [HttpPost]
        [Route(API.Update)]
        //[Authorize(Roles = "Administrator, Moderator")]
        public IHttpActionResult UpdateEvent(EventDTO model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Try update 
            var eventDatabaseModel = this.Mapper.Map<Event>(model);
            var updateResult = this.EventsService.Update(eventDatabaseModel);

            // Prepare response
            var message = string.Empty;
            var isSuccessful = false;
            if (updateResult > 0)
            {
                message = "Event successfully updated.";
                isSuccessful = true;
            }
            else if (updateResult == API.Overlap)
            {
                message = "Event overlap occured. Update failed.";
            }
            else if (updateResult == API.Conflict)
            {
                message = "Database validation error.";
            }
            else if (updateResult == API.InvalidModel)
            {
                message = "Event start time cannot be after event end time";
            }

            return this.Ok(
                new ResponseResultObject(
                    isSuccessful,
                    message,
                    eventDatabaseModel));
        }

        [HttpPost]
        [Route(API.Create)]
        //[Authorize(Roles = "Administrator, Moderator")]
        public IHttpActionResult CreateEvent(EventDTO model)
        {
            // Validate model
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            // Try create
            var eventDatabaseModel = this.Mapper.Map<Event>(model);
            var creationResult = this.EventsService.Add(eventDatabaseModel);

            // Prepare response
            var message = string.Empty;
            var isSuccessful = false;
            if (creationResult == API.Overlap)
            {
                message = "Event overlap occured. Creation failed.";
            }
            else if (creationResult == API.Conflict)
            {
                message = "Database validation error.";
            }
            else if (creationResult == API.InvalidModel)
            {
                message = "Event start time cannot be after event end time";
            }
            else if (creationResult > 0)
            {
                message = "Event successfully added.";
                isSuccessful = true;
            }

            return this.Ok(
                new ResponseResultObject(
                    isSuccessful,
                    message,
                    this.Mapper.Map<EventDTO>(eventDatabaseModel)));
        }

        [HttpGet]
        [Route(API.Delete)]
        //[Authorize(Roles = "Administrator, Moderator")]
        public IHttpActionResult DeleteEvent(int eventToDeleteId)
        {
            var eventToDelete = this.EventsService.GetById(eventToDeleteId);
            this.EventsService.Delete(eventToDelete);

            return this.Ok(
                new ResponseResultObject(
                    true,
                    "Event successfully deleted",
                    this.Mapper.Map<EventDTO>(eventToDelete)));
        }
    }
}
