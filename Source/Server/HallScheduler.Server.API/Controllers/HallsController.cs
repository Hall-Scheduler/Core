﻿namespace HallScheduler.Server.API.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Common.Constants;
    using DataTransferObjects.Halls;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;

    [RoutePrefix(API.Halls)]
    public class HallsController : BaseController
    {
        public HallsController(IHallsService halls)
        {
            this.HallsService = halls;
        }

        public IHallsService HallsService { get; set; }

        [HttpGet]
        [Route(API.All)]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await this.HallsService.All().To<HallBriefDTO>().ToListAsync();

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(result.Count),
                    result));
        }

        [HttpGet]
        [Route(API.Schedule)]
        public IHttpActionResult GetSchedule(int hallId)
        {
            var hall = this.HallsService.GetById(hallId);
            var result = this.Mapper.Map<HallScheduleDTO>(hall);

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(API.Single),
                    result));
        }

        [HttpGet]
        [Route("HallDetailsWithSchedule")]
        public IHttpActionResult GetHallDetailsWithSchedule(int hallId)
        {
            var hall = this.HallsService.GetById(hallId);
            var result = this.Mapper.Map<HallDetailedDTO>(hall);

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(API.Single),
                    result));
        }

        [HttpGet]
        [Route(API.FullSchedule)]
        public async Task<IHttpActionResult> GetFullSchedule()
        {
            var result = await this.HallsService.All().To<HallDetailedDTO>().ToListAsync();

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(result.Count),
                    result));
        }
    }
}
