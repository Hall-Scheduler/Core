namespace HallScheduler.Server.API.Controllers
{
    using Data.Contexts;
    using DataTransferObjects.Users;
    using HallScheduler.Common.Constants;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Common.Constants;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpGet]
        [Route("All")]
        //[ValidateRequestModel]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await this.users.All().To<UserDetailedDTO>().ToListAsync();

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(result.Count),
                    result));
        }

        [HttpGet]
        [Route("Lecturers")]
        public IHttpActionResult GetAllLecturers()
        {
            var result = this.users.AllWithRole(Roles.Professor).To<UserBriefDTO>().ToList();

            return this.Ok(
                new ResponseResultObject(
                    API.Success,
                    API.ReturnedItems(result.Count),
                    result));
        }
    }
}