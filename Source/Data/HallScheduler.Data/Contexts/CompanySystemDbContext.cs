namespace HallScheduler.Data.Contexts
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Contracts;
    using System.Data.Entity;
    using System;
    using Models;

    public class HallSchedulerDbContext : IdentityDbContext<User>, IHallSchedulerDbContext
    {
        public HallSchedulerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        //public virtual IDbSet<Country> Countries { get; set; }

        //public virtual IDbSet<City> Cities { get; set; }

        public static HallSchedulerDbContext Create()
        {
            return new HallSchedulerDbContext();
        }
    }
}