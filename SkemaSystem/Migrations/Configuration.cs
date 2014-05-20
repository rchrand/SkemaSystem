namespace SkemaSystem.Migrations
{
    using SkemaSystem.Models;
    using System;
    using System.Collections.Generic;
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
            /*context.Educations.AddOrUpdate(
                e => e.Name,
                new Education { Name = "DMU" },
                new Education { Name = "FIBCA" },
                new Education { Name = "MDU" }
            );*/

            /*var teacher = new Teacher() { Name = "Hanne Sommer", Educations = new List<Education>() { context.Educations.FirstOrDefault() } };
            context.Teachers.AddOrUpdate(teacher);*/

            /*context.Teachers.AddOrUpdate(
                t => t.Name,
                new Teacher { Name = "Hanne Sommer", Educations = new List<Education>() { context.Educations.FirstOrDefault() } },
                new Teacher { Name = "Torben Kroejmand" }
            );

            var teacher = context.Teachers.First();*/
            //var education = context.Educations.First();

            //teacher.Educations.Add(education);
            //education.Teachers.Add(teacher);

            context.Educations.AddOrUpdate(
                e => e.Name,
                new Education { Name = "DMU" },
                new Education { Name = "FIBCA" },
                new Education { Name = "MDU" }
            );
            
            context.Teachers.AddOrUpdate(
                t => t.Name,
                new Teacher { Name = "Hanne Sommer" },
                new Teacher { Name = "Torben Kroejmand" }
            );

            //context.Teachers.FirstOrDefault().Educations.Add(context.Educations.First());
        }
    }
}
