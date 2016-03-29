namespace CompanySystem.Data.Contexts
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Contracts;
    using System.Data.Entity;
    using System;
    using Models;

    public class CompanySystemDbContext : IdentityDbContext<User>, ICompanySystemDbContext
    {
        public CompanySystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        //public virtual IDbSet<Country> Countries { get; set; }

        //public virtual IDbSet<City> Cities { get; set; }

        public static CompanySystemDbContext Create()
        {
            return new CompanySystemDbContext();
        }
    }
}