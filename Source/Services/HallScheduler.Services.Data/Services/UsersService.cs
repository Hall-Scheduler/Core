namespace HallScheduler.Services.Data.Services
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HallScheduler.Data.Contracts;
    using HallScheduler.Data.Models;

    public class UsersService : IUsersService
    {
        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        private IRepository<User> users;

        public IQueryable<User> All()
        {
            return this.users.All();
        }

        public User GetById(string userId)
        {
            return this.users.GetById(userId);
        }
    }
}