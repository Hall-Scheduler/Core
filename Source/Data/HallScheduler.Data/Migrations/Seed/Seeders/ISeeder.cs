namespace HallScheduler.Data.Migrations.Seed.Seeders
{
    using Contexts;

    public interface ISeeder
    {
        int Priority { get; set; }

        void Seed(HallSchedulerDbContext context);
    }
}
