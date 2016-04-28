namespace HallScheduler.Services.Data.Contracts
{
    using System.Linq;

    using Common.Contracts;
    using HallScheduler.Data.Models;

    public interface IUsersService : IService
    {
        IQueryable<User> All();

        User GetById(string userId);
    }
}
