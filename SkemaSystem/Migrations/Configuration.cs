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
            Teacher t1 = new Teacher { Name = "Hanne Sommer", Username = "eaasommer", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t2 = new Teacher { Name = "Torben Kr�jmand", Username = "eaatk", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t3 = new Teacher { Name = "Erik Jacobsen", Username = "eaaej", Password = "fisk123", Role = Models.Enum.UserRoles.Admin };
            Teacher t4 = new Teacher { Name = "J�rn Hujak", Username = "eaajh", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t5 = new Teacher { Name = "Karsten ITO", Username = "eaakarsten", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };

            ClassModel c1 = new ClassModel { ClassName = "12t" };
            ClassModel c2 = new ClassModel { ClassName = "12s" };

            Subject su1 = new Subject { Name = "SK" };
            Subject su2 = new Subject { Name = "SD" };
            Subject su3 = new Subject { Name = "ITO" };

            SemesterSubjectBlock ssb1 = new SemesterSubjectBlock { Subject = su1, BlocksCount = 40 };
            SemesterSubjectBlock ssb2 = new SemesterSubjectBlock { Subject = su2, BlocksCount = 20 };
            SemesterSubjectBlock ssb3 = new SemesterSubjectBlock { Subject = su3, BlocksCount = 20 };
            SemesterSubjectBlock ssb4 = new SemesterSubjectBlock { Subject = su1, BlocksCount = 10 };

            List<SemesterSubjectBlock> ssdbList1 = new List<SemesterSubjectBlock>();
            ssdbList1.Add(ssb1); ssdbList1.Add(ssb2); ssdbList1.Add(ssb3);

            List<SemesterSubjectBlock> ssdbList2 = new List<SemesterSubjectBlock>();
            ssdbList2.Add(ssb4);

            Semester s1 = new Semester { Number = 1, Blocks = ssdbList1 };
            Semester s2 = new Semester { Number = 2, Blocks = ssdbList2 };


            Education e1 = new Education { Name = "DMU" };
            Education e2 = new Education { Name = "MDU" };

            List<Teacher> tList = new List<Teacher>();
            tList.Add(t1);
            tList.Add(t2);
            tList.Add(t3);
            tList.Add(t4);
            tList.Add(t5);

            e1.Teachers = tList;

            List<Semester> sList = new List<Semester>();
            sList.Add(s1);
            sList.Add(s2);

            e1.Semesters = sList;

            c1.Education = e1;
            c2.Education = e1;

            context.Educations.AddOrUpdate(
                e => e.Name,

                new Education { Id = 1, Name = "DMU" },

                new Education { Name = "DMU" },
                new Education { Name = "FIBCA" },
                new Education { Name = "MDU" }

            );

            context.Teachers.AddOrUpdate(
                t => t.Name,
                t1, t2, t3, t4, t5
            );


            context.Classes.AddOrUpdate(
                c => c.ClassName,
                c1, c2
                );

            context.Subjects.AddOrUpdate(
                s => s.Name,
                su1, su2, su3);

            context.Semesters.AddOrUpdate(
                s => s.Id,
                s1, s2);

            context.Rooms.AddOrUpdate(r => r.RoomName,
                new Room() { RoomName = "A1.1", Id = 1},
                new Room() { RoomName = "A1.2", Id = 1},
                new Room() { RoomName = "A2.1", Id = 1},
                new Room() { RoomName = "B1.1", Id = 1},
                new Room() { RoomName = "B1.2", Id = 1}
                );
        }

    }
}
