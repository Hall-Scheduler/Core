namespace CompanySystem.Data.Migrations.Seed.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contexts;
    using Common.Constants;
    using SeedModels;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Models;
    using CompanySystem.Common.Constants;
    using Common.Enumerations;
    public class UsersSeeder : ISeeder
    {
        public List<UserSeedModel> Users { get; set; } = new List<UserSeedModel>();

        public void Seed(CompanySystemDbContext context)
        {
            if (!context.Users.Any())
            {
                this.InitializeUserSeedModels();

                foreach (var user in this.Users)
                {
                    this.SeedUser(context, user);
                }
            }
        }

        public void SeedUser(CompanySystemDbContext context, UserSeedModel model)
        {
            if (!context.Users.Any(u => u.UserName == model.UserName))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                userManager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 5,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    AcademicRank = model.AcademicRank
                };

                IdentityResult result = userManager.Create(user, model.Password);

                if (!result.Succeeded)
                {
                    throw new OperationCanceledException(result.Errors.First());
                }

                if (model.Role != null)
                {
                    userManager.AddToRole(user.Id, model.Role);
                }

                context.SaveChanges();
            }
        }

        public void InitializeUserSeedModels()
        {
            // Administrators
            for (int i = 1; i <= 10; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    UserName = "admin" + i + "@site.com",
                    Email = "admin" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "35988888888" + (i - 1),
                    Role = Roles.Administrator,
                    AcademicRank = AcademicRank.HeadOfDepartment
                };

                this.Users.Add(model);
            }

            // Moderators
            for (int i = 1; i <= 20; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    UserName = "mod" + i + "@site.com",
                    Email = "mod" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "3597777777" + (i - 1),
                    Role = Roles.Moderator,
                    AcademicRank = AcademicRank.Dean
                };

                this.Users.Add(model);
            }

            // Professors
            for (int i = 1; i <= 30; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    UserName = "prof" + i + "@site.com",
                    Email = "prof" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "35966666666" + (i - 1),
                    Role = Roles.Professor,
                    AcademicRank = AcademicRank.Professor
                };

                this.Users.Add(model);
            }

            // Students
            for (int i = 1; i <= 40; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = "FirstName " + i,
                    LastName = "LastName " + i,
                    UserName = "stud" + i + "@site.com",
                    Email = "stud" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "35955555555" + (i - 1),
                    Role = null,
                    AcademicRank = AcademicRank.RegularStudent
                };

                this.Users.Add(model);
            }
        }
    }
}
