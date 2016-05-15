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
    using Server.Infrastructure.Mapping;
    using System.Linq.Expressions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using HallScheduler.Common.Constants;
    public class UsersService : IUsersService
    {
        public UsersService(
            IRepository<User> users,
            IRepository<IdentityRole> roles)
        {
            this.users = users;
            this.roles = roles;
        }

        private IRepository<User> users;

        private IRepository<IdentityRole> roles;

        public IQueryable<User> All()
        {
            return this.users.All();
        }

        public IQueryable<User> AllWithRole(string roleName)
        {
            var roleId = this.roles.All().FirstOrDefault(x => x.Name == roleName).Id;
            return this.users.All().Where(x=> x.Roles.Any(z=> z.RoleId == roleId));
        }

        public User GetById(string userId)
        {
            return this.users.GetById(userId);
        }
    }
}