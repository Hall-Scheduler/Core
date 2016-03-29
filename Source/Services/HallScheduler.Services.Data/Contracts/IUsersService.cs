namespace HallScheduler.Services.Data.Contracts
{
    using Common.Contracts;
    using HallScheduler.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUsersService : IService
    {
        IQueryable<User> All();
    }
}