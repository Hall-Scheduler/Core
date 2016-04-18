namespace HallScheduler.Data.Migrations
{
    using Contexts;
    using Seed;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<HallSchedulerDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        public SeedFactory SeedFactory { get; set; } = new SeedFactory();

        protected override void Seed(HallSchedulerDbContext context)
        {
            foreach(var seeder in this.SeedFactory.Seeders)
            {
                seeder.Seed(context);
            }
        }
    }
}
