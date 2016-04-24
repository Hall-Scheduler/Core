namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common.Enumerations;
    using Contexts;
    using HallScheduler.Common.Constants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using SeedModels;

    public class UsersSeeder : ISeeder
    {
        public int Priority { get; set; } = 2;

        public List<UserSeedModel> Users { get; set; } = new List<UserSeedModel>();

        public void Seed(HallSchedulerDbContext context)
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

        public void SeedUser(HallSchedulerDbContext context, UserSeedModel model)
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
                    AcademicRank = model.AcademicRank,
                    Faculty = model.Faculty
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
            var randomGenerator = new Random();
            var firstNames = new string[]
            {
                "Ivan",
                "Mariyan",
                "Peter",
                "Dinko",
                "Georgi",
                "Nikolay",
                "Ivaylo",
                "Simeon",
                "Stiliyan"
            };
            var lastNames = new string[]
            {
                "Kolev",
                "Simeonov",
                "Georgiev",
                "Stoyanov",
                "Iliev",
                "Martinov",
                "Dimitrov",
                "Ivanov",
                "Bogdanov"
            };

            var faculties = Enum.GetValues(typeof(FacultyType))
                .Cast<FacultyType>()
                .ToList();

            // Administrators
            for (int i = 1; i <= 10; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = firstNames[randomGenerator.Next(0, firstNames.Length)],
                    LastName = lastNames[randomGenerator.Next(0, lastNames.Length)],
                    UserName = "admin" + i + "@site.com",
                    Email = "admin" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "35988888888" + (i - 1),
                    Role = Roles.Administrator,
                    AcademicRank = AcademicRankType.HeadOfDepartment,
                    Faculty = faculties[randomGenerator.Next(0, faculties.Count)]
                };

                this.Users.Add(model);
            }

            // Moderators
            for (int i = 1; i <= 20; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = firstNames[randomGenerator.Next(0,firstNames.Length)],
                    LastName = lastNames[randomGenerator.Next(0, lastNames.Length)],
                    UserName = "moderator" + i + "@site.com",
                    Email = "moderator" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "3597777777" + (i - 1),
                    Role = Roles.Moderator,
                    AcademicRank = AcademicRankType.Dean,
                    Faculty = faculties[randomGenerator.Next(0, faculties.Count)]
                };

                this.Users.Add(model);
            }

            // Professors
            for (int i = 1; i <= 30; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = firstNames[randomGenerator.Next(0, firstNames.Length)],
                    LastName = lastNames[randomGenerator.Next(0, lastNames.Length)],
                    UserName = "prof" + i + "@site.com",
                    Email = "prof" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "35966666666" + (i - 1),
                    Role = Roles.Professor,
                    AcademicRank = AcademicRankType.Professor,
                    Faculty = faculties[randomGenerator.Next(0, faculties.Count)]
                };

                this.Users.Add(model);
            }

            // Students
            for (int i = 1; i <= 40; i++)
            {
                var model = new UserSeedModel()
                {
                    FirstName = firstNames[randomGenerator.Next(0, firstNames.Length)],
                    LastName = lastNames[randomGenerator.Next(0, lastNames.Length)],
                    UserName = "stud" + i + "@site.com",
                    Email = "stud" + i + "@site.com",
                    Password = "123123",
                    PhoneNumber = "35955555555" + (i - 1),
                    Role = null,
                    AcademicRank = AcademicRankType.RegularStudent,
                    Faculty = faculties[randomGenerator.Next(0, faculties.Count)]
                };

                this.Users.Add(model);
            }
        }
    }
}
