namespace HallScheduler.Data.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    using Models;

    public interface IHallSchedulerDbContext
    {
        IDbSet<Hall> Halls { get; set; }

        IDbSet<Event> Events { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
