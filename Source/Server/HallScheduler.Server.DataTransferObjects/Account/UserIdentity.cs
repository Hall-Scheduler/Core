namespace HallScheduler.Server.DataTransferObjects.Account
{
    using Data.Common.Enumerations;
    using System.Collections.Generic;

    public class UserIdentity
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public AcademicRankType AcademicRank { get; set; }

        public FacultyType Faculty { get; set; }

        public List<IdentityRoleType> Roles { get; set; }
    }
}
