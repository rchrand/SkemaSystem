namespace SkemaSystem.Migrations
{
    using SkemaSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SkemaSystem.Models.SkeamSystemDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SkemaSystem.Models.SkeamSystemDb context)
        {
            context.Educations.AddOrUpdate(
                e => e.Name,
                new Education { Name = "DMU" }
            );

            context.Teachers.AddOrUpdate(
                t => t.Name,
                new Teacher { Name = "Hanne Sommer" },
                new Teacher { Name = "Torben Kroejmand" }
            );

        }
    }
}
