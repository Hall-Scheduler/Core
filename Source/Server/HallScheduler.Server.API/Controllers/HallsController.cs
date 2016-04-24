namespace HallScheduler.Server.API.Controllers
{
    using AutoMapper;
    using Common.Constants;
    using Data.Models;
    using DataTransferModels.Halls;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix(API.Halls)]
    public class HallsController : BaseController
    {
        public HallsController(IHallsService halls)
        {
            this.Halls = halls;
        }

        public IHallsService Halls { get; set; }

        [HttpGet]
        [Route(API.All)]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await this.Halls.All().To<HallBriefDTO>().ToListAsync();

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
            var hall = this.Halls.GetById(hallId);
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
            var result = await this.Halls.All().To<HallDetailedDTO>().ToListAsync();

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(result.Count),
                    result));
        }

        //
    }
}
