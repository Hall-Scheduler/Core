namespace CompanySystem.Server.API.Controllers
{
    using AutoMapper.QueryableExtensions;
    using DataTransferModels.Users;
    using Infrastructure.Mapping;
    using Infrastructure.Validation;
    using Services.Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
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
        //[Route("All")]
        //[ValidateRequestModel]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await this.users.All()
                .To<UserDetailedDataTransferModel>()
                .ToListAsync();

            return this.Ok(result);
        }
    }
}