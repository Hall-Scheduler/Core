namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using Contexts;

    public interface ISeeder
    {
        void Seed(HallSchedulerDbContext context);
    }
}
