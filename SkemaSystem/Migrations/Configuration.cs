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
        }

        protected override void Seed(SkemaSystem.Models.SkeamSystemDb context)
        {
            context.Teachers.AddOrUpdate(
              p => p.Name,
              new Teacher { Name = "Torben Krøjmand" },
              new Teacher { Name = "Hanne Sommer" },
              new Teacher { Name = "Erik Jacobsen" }
            );

            context.Educations.AddOrUpdate(
                e => e.Name,
                new Education { Name = "DMU" },
                new Education { Name = "MDU" }
            );
        }
    }
}
