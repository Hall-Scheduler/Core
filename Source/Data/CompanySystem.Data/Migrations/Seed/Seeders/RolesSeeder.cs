namespace CompanySystem.Data.Migrations.Seed.Seeders
{
    using Contexts;
    using CompanySystem.Common.Constants;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    public class RolesSeeder : ISeeder
    {
        public void Seed(CompanySystemDbContext context)
        {
            this.SeedRole(Roles.Administrator, context);
            this.SeedRole(Roles.Moderator, context);
            this.SeedRole(Roles.Professor, context);
        }

        private void SeedRole(string name, CompanySystemDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole { Name = name };

            manager.Create(role);
            context.SaveChanges();
        }
    }
}
