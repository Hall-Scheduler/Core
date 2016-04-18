namespace HallScheduler.Server.API.Controllers
{
    using AutoMapper;
    using Infrastructure.Mapping;
    using System.Web.Http;

    public class BaseController : ApiController
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}
