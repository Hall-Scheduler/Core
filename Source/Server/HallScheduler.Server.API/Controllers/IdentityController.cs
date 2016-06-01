namespace HallScheduler.Server.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Common.Constants;
    using Data.Common.Enumerations;
    using DataTransferObjects.Account;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [RoutePrefix(API.Identity)]
    public class IdentityController : BaseController
    {
        public IdentityController(IUsersService users, IRolesService roles)
        {
            this.Users = users;
            this.Roles = roles;
        }

        private IUsersService Users { get; set; }

        private IRolesService Roles { get; set; }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.Users.GetById(userId);
            var userRoles = user.Roles.ToList();
            var identityRoles = new List<IdentityRoleType>(userRoles.Count);

            foreach(var role in userRoles)
            {
                var roleName = this.Roles.GetNameById(role.RoleId);
                var identityRole = (IdentityRoleType)Enum.Parse(typeof(IdentityRoleType), roleName);
                identityRoles.Add(identityRole);
            }

            var result = new UserIdentity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                AcademicRank = user.AcademicRank,
                Faculty = user.Faculty,
                FullName = $"{user.FirstName} {user.LastName}",
                Roles = identityRoles
            };
          
            return this.Ok(
                new ResponseResultObject(
                    API.Success, 
                    API.ReturnedItems(API.Single), 
                    result));
        }
    }
}
