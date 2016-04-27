namespace HallScheduler.Server.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Common.Constants;
    using System.Threading.Tasks;

    [RoutePrefix(API.Identity)]
    public class IdentityController : BaseController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            // Get current user
            // Return UserIdentityModel
            return this.Ok();
        }
    }
}
