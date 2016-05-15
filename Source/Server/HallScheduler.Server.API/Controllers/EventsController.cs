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
            var eventDatabaseModel = this.Mapper.Map<Event>(model);

            if (model.Id > 0)
            {
                this.EventsService.Update(eventDatabaseModel);
            }
            else
            {
                this.EventsService.Add(eventDatabaseModel);
            }

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(API.Single),
                    API.Success));
        }
    }
}
