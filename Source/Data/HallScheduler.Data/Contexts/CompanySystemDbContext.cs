namespace HallScheduler.Data.Contexts
{
    using System.Data.Entity;

    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class HallSchedulerDbContext : IdentityDbContext<User>, IHallSchedulerDbContext
    {
        public HallSchedulerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Hall> Halls { get; set; }

        public IDbSet<Event> Events { get; set; }

        public static HallSchedulerDbContext Create()
        {
            return new HallSchedulerDbContext();
        }
    }
}
