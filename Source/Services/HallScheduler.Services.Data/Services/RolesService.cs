namespace HallScheduler.Services.Data.Services
{
    using System.Linq;

    using Contracts;
    using HallScheduler.Data.Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RolesService : IRolesService
    {
        public RolesService(IRepository<IdentityRole> roles)
        {
            this.Roles = roles;
        }

        private IRepository<IdentityRole> Roles { get; set; }

        public IQueryable<IdentityRole> All()
        {
            return this.Roles.All();
        }

        public IdentityRole GetById(int roleId)
        {
            return this.Roles.GetById(roleId);
        }

        public string GetNameById(string roleId)
        {
            return this.Roles.GetById(roleId).Name;
        }
    }
}
