namespace CompanySystem.Data.Migrations.Seed.Seeders
{
    using Contexts;

    public interface ISeeder
    {
        void Seed(CompanySystemDbContext context);
    }
}
