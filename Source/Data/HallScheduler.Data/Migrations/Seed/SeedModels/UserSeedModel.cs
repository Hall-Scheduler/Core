namespace HallScheduler.Data.Migrations.Seed.SeedModels
{
    using HallScheduler.Data.Common.Enumerations;

    public class UserSeedModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Cabinet { get; set; }

        public AcademicRankType AcademicRank { get; set; }

        public FacultyType Faculty { get; set; }
    }
}