namespace SkemaSystem.Migrations
{
    using SkemaSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

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
            );

            context.Teachers.AddOrUpdate(
                t => t.Name,
                new Teacher { Name = "Hanne Sommer" },
                new Teacher { Name = "Torben Kroejmand" }
            );

            context.Users.AddOrUpdate(
                u => u.UserName,
                new UserModel { UserName = "eaasommer", Password = "fisk123" },
                new UserModel { UserName = "eaatk", Password = "torben5" }
            );

            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }
            if (!Roles.RoleExists("Teacher"))
            {
                Roles.CreateRole("Teacher");
            }

            if (Roles.GetRolesForUser("eaatk").ToList().Count == 0)
            {
                Roles.AddUserToRole("eaatk", "Admin");
            }

            if (Roles.GetRolesForUser("eaasommer").ToList().Count == 0)
            {
                Roles.AddUserToRole("eaasommer", "Teacher");
            }

            );*/
        }
    }
}
