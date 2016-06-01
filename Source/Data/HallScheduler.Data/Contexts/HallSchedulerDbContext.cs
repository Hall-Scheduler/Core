namespace HallScheduler.Data.Contexts
{
    using System;
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

        public IDbSet<EventLog> EventLogs { get; set; }

        public IDbSet<EventSubscription> EventSubscriptions { get; set; }

        public static HallSchedulerDbContext Create()
        {
            return new HallSchedulerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventSubscription>()
                .HasRequired(x => x.Lecturer)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventSubscription>()
                .HasRequired(x => x.Event)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hall>()
                .HasRequired(x => x.Schedule)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Event>()
                .HasRequired(x => x.Hall)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
