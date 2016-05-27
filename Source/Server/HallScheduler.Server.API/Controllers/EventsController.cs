namespace HallScheduler.Server.API.Controllers
{
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Common.Constants;
    using System.Threading.Tasks;
    using DataTransferObjects.Events;
    using Data.Models;
    using Infrastructure;
    using HallScheduler.Common.Constants;
    using Data.Common;
    using Data.Common.Contracts;

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
        public IHttpActionResult UpdateEvent(EventDTО model)
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
            if (updateResult == API.Overlap)
            {
                message = "Event overlap occured. Update failed.";
            }
            else if (updateResult == API.Conflict)
            {
                message = "Database validation error.";
            }
            else if (updateResult > 0)
            {
                message = "Event successfully updated.";
                isSuccessful = true;
            }

            return this.Ok(
                new ResponseResultObject(
                    isSuccessful,
                    message,
                    eventDatabaseModel));
        }

        [HttpPost]
        [Route(API.Create)]
        public IHttpActionResult CreateEvent(EventDTО model)
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
            else if (creationResult > 0)
            {
                message = "Event successfully added.";
                isSuccessful = true;
            }

            return this.Ok(
                new ResponseResultObject(
                    isSuccessful,
                    message,
                    this.Mapper.Map<EventDTО>(eventDatabaseModel)));
        }
    }
}
