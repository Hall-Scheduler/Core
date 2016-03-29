namespace CompanySystem.Data.Migrations.Seed
{
    using Seeders;
    using System.Collections.Generic;

    public class SeedFactory
    {
        public SeedFactory()
        {
            this.Seeders.Add(new RolesSeeder());
            this.Seeders.Add(new UsersSeeder());
        }

        public ICollection<ISeeder> Seeders { get; set; } = new List<ISeeder>();
    }
}