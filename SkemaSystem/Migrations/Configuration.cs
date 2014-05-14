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
                t => t.Name,
                new Teacher { Id = 1, Name = "Torben Krøjmand" },
                new Teacher { Id = 2, Name = "Hanne Sommer" });
        }
    }
}
