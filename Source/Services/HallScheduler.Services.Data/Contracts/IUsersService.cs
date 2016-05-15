namespace HallScheduler.Services.Data.Contracts
{
    using System.Linq;

    using Common.Contracts;
    using HallScheduler.Data.Models;

    public interface IUsersService : IService
    {
        IQueryable<User> All();

        IQueryable<User> AllWithRole(string roleName);

        User GetById(string userId);
    }
}
