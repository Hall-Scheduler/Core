namespace HallScheduler.Server.API.Controllers
{
    using Data.Contexts;
    using DataTransferModels.Users;
    using HallScheduler.Common.Constants;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;

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
            var result = await this.users.All()
                .To<UserDetailedDataTransferModel>()
                .ToListAsync();

            return this.Ok(new ResponseResultObject(true, $"Returned {result.Count} items", result));
        }

        [HttpGet]
        [Route("Professors")]
        public async Task<IHttpActionResult> GetAllProfessors()
        {
            var context = new HallSchedulerDbContext();
            var professorsRole = context.Roles.Where(x => x.Name == Roles.Professor).FirstOrDefault();
            var result = await context.Users
                .Where(x => x.Roles.Any(z => z.RoleId == professorsRole.Id))
                .Select(x => new
                {
                    UserName = x.UserName,
                    Email = x.Email
                }).ToListAsync();

            return this.Ok(new ResponseResultObject(true, $"Returned {result.Count} items", result));
        }
    }
}