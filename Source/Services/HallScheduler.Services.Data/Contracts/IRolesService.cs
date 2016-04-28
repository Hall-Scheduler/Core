namespace HallScheduler.Services.Data.Contracts
{
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IRolesService
    {
        IQueryable<IdentityRole> All();

        IdentityRole GetById(int roleId);

        string GetNameById(string roleId);
    }
}
