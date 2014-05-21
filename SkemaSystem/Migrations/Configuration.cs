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
            context.Educations.AddOrUpdate(
                e => e.Name,
                new Education { Name = "DMU" },
                new Education { Name = "FIBA" },
                new Education { Name = "MDU" }
            );

            //context.Teachers.AddOrUpdate(
            //    t => t.Name,
            //    new Teacher { Name = "Hanne Sommer" },
            //    new Teacher { Name = "Torben Kroejmand" }
            //);

            //context.Rooms.AddOrUpdate(
            //    r => r.RoomName,
            //    new Room { RoomName = "A1.1" },
            //    new Room { RoomName = "A1.12" },
            //    new Room { RoomName = "A1.13" },
            //    new Room { RoomName = "A1.14" },
            //    new Room { RoomName = "A1.15" }
            //);

        }
    }
}
