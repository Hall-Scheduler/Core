namespace HallScheduler.Server.API.Controllers
{
    using Data.Common.Enumerations;
    using DataTransferModels.Halls;
    using HallScheduler.Common.Constants;
    using HallScheduler.Data.Contexts;
    using HallScheduler.Server.Infrastructure;
    using Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/Halls")]
    public class HallsController : ApiController
    {
        [HttpGet]
        [Route("All")]
        public async Task<IHttpActionResult> GetAllHalls()
        {
            var context = new HallSchedulerDbContext();

            //var result = await context.Halls.Select(x => new
            //{
            //    HallName = x.Block.ToString() + x.Stage.ToString() + x.Room.ToString(),
            //    HallType = x.Type,
            //    HallSchedule = x.Schedule.Select(z => new
            //    {
            //        StartsAt = z.StartsAt,
            //        Duration = z.DurationInMinutes,
            //        Lecturer = z.Lecturer.UserName,
            //        Topic = z.Topic,
            //        DayOfTheWeek = z.DayOfWeek
            //    }).ToList()
            //}).ToListAsync();

            var result = await context.Halls.To<HallDTM>().ToListAsync();

            // 450 % 60 = 30
            // 450 / 60 = 7

            return this.Ok(new ResponseResultObject(true, $"Returned {result.Count} items", result));
        }
    }
}
