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
            Education e1 = new Education { Id = 1, Name = "DMU", NumberOfSemesters = 4 };
            Education e2 = new Education { Id = 2, Name = "MDU", NumberOfSemesters = 6 };

            Teacher t1 = new Teacher { Id = 1, Name = "Hanne Sommer", Username = "eaasommer", Password = "fisk123", Role = UserRoles.Teacher };
            Teacher t2 = new Teacher { Id = 2, Name = "Torben Krøjmand", Username = "eaatk", Password = "fisk123", Role = UserRoles.Teacher };
            Teacher t3 = new Teacher { Id = 3, Name = "Erik Jacobsen", Username = "eaaej", Password = "fisk123", Role = UserRoles.Master };
            Teacher t4 = new Teacher { Id = 4, Name = "Jörn Hujak", Username = "eaajh", Password = "fisk123", Role = UserRoles.Teacher };
            Teacher t5 = new Teacher { Id = 5, Name = "Karsten ITO", Username = "eaakarsten", Password = "fisk123", Role = UserRoles.Teacher };

            ClassModel c1 = new ClassModel { Id = 1, ClassName = "12t" };
            ClassModel c2 = new ClassModel { Id = 2, ClassName = "12s" };

            Subject su1 = new Subject { Id = 1, Name = "SK", Education = e1, OptionalSubject = false };
            Subject su2 = new Subject { Id = 2, Name = "SD", Education = e1, OptionalSubject = false };
            Subject su3 = new Subject { Id = 3, Name = "ITO", Education = e1, OptionalSubject = false };

            SemesterSubjectBlock ssb1 = new SemesterSubjectBlock { Id = 1, Subject = su1, BlocksCount = 40 };
            SemesterSubjectBlock ssb2 = new SemesterSubjectBlock { Id = 2, Subject = su2, BlocksCount = 20 };
            SemesterSubjectBlock ssb3 = new SemesterSubjectBlock { Id = 3, Subject = su3, BlocksCount = 20 };
            SemesterSubjectBlock ssb4 = new SemesterSubjectBlock { Id = 4, Subject = su1, BlocksCount = 10 };
            SemesterSubjectBlock ssb5 = new SemesterSubjectBlock { Id = 5, Subject = su1, BlocksCount = 40 };
            SemesterSubjectBlock ssb6 = new SemesterSubjectBlock { Id = 6, Subject = su1, BlocksCount = 50 };

            List<SemesterSubjectBlock> ssdbList1 = new List<SemesterSubjectBlock>();
            ssdbList1.Add(ssb1); ssdbList1.Add(ssb2); ssdbList1.Add(ssb3);

            List<SemesterSubjectBlock> ssdbList2 = new List<SemesterSubjectBlock>();
            ssdbList2.Add(ssb4);

            List<SemesterSubjectBlock> ssdbList3 = new List<SemesterSubjectBlock>();
            ssdbList3.Add(ssb5);

            List<SemesterSubjectBlock> ssdbList4 = new List<SemesterSubjectBlock>();
            ssdbList4.Add(ssb6);

            Semester s1 = new Semester { Id = 1, Number = 1, Blocks = ssdbList1 };
            Semester s2 = new Semester { Id = 2, Number = 2, Blocks = ssdbList2 };
            Semester s3 = new Semester { Id = 3, Number = 3, Blocks = ssdbList3 };
            Semester s4 = new Semester { Id = 4, Number = 4, Blocks = ssdbList4 };

            Scheme sch1 = new Scheme { ClassModel = c1, Semester = s1, Id = 1 };
            Scheme sch2 = new Scheme { ClassModel = c1, Semester = s2, Id = 2 };
            Scheme sch3 = new Scheme { ClassModel = c2, Semester = s1, Id = 3 };

            Room r1 = new Room { Id = 1, RoomName = "SH-A1.13" };
            Room r2 = new Room { Id = 2, RoomName = "SH-A1.8" };
            Room r3 = new Room { Id = 3, RoomName = "SH-A1.4" };

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
            sList.Add(s3);
            sList.Add(s4);

            e1.Semesters = sList;

            c1.Education = e1;
            c2.Education = e1;

            //Testdata for TeacherSwitch

            SubjectDistBlock sdb_1 = new SubjectDistBlock() { Id = 1, BlocksCount = 10, Subject = su1, Teacher = t1 };
            SubjectDistBlock sdb_2 = new SubjectDistBlock() { Id = 1, BlocksCount = 10, Subject = su2, Teacher = t2 };
            SubjectDistBlock sdb_3 = new SubjectDistBlock() { Id = 1, BlocksCount = 10, Subject = su3, Teacher = t3 };

            LessonBlock lb1_1 = new LessonBlock { Id = 101, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = sdb_1, Room = r3 };
            LessonBlock lb1_2 = new LessonBlock { Id = 102, Date = new DateTime(2014, 5, 26), BlockNumber = 1, Subject = sdb_1, Room = r3 };

            LessonBlock lb2_1 = new LessonBlock { Id = 103, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = sdb_2, Room = r3 };
            LessonBlock lb2_2 = new LessonBlock { Id = 104, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = sdb_2, Room = r3 };

            LessonBlock lb3_1 = new LessonBlock { Id = 105, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = sdb_2, Room = r3 };
            LessonBlock lb3_2 = new LessonBlock { Id = 106, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = sdb_2, Room = r3 };
            LessonBlock lb3_3 = new LessonBlock { Id = 107, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = sdb_1, Room = r3 };

            LessonBlock lb4_1 = new LessonBlock { Id = 108, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = sdb_1, Room = r3 };
            LessonBlock lb4_2 = new LessonBlock { Id = 109, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = sdb_2, Room = r3 };
            LessonBlock lb4_3 = new LessonBlock { Id = 110, Date = new DateTime(2014, 5, 29), BlockNumber = 2, Subject = sdb_2, Room = r3 };
            LessonBlock lb4_4 = new LessonBlock { Id = 111, Date = new DateTime(2014, 5, 29), BlockNumber = 3, Subject = sdb_2, Room = r3 };

            LessonBlock lb5_1 = new LessonBlock { Id = 112, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = sdb_2, Room = r3 };
            LessonBlock lb5_2 = new LessonBlock { Id = 113, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = sdb_2, Room = r3 };

            LessonBlock lb6_1 = new LessonBlock { Id = 114, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = sdb_2, Room = r3 };
            LessonBlock lb6_2 = new LessonBlock { Id = 115, Date = new DateTime(2014, 6, 2), BlockNumber = 1, Subject = sdb_2, Room = r3 };
            LessonBlock lb6_3 = new LessonBlock { Id = 116, Date = new DateTime(2014, 6, 2), BlockNumber = 2, Subject = sdb_2, Room = r3 };

            LessonBlock lb7_1 = new LessonBlock { Id = 117, Date = new DateTime(2014, 6, 3), BlockNumber = 0, Subject = sdb_2, Room = r3 };
            LessonBlock lb7_2 = new LessonBlock { Id = 118, Date = new DateTime(2014, 6, 3), BlockNumber = 1, Subject = sdb_2, Room = r3 };

            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1, lb1_2,
                lb2_1, lb2_2,
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_1, lb5_2, 
                lb6_1, lb6_2, lb6_3,
                lb7_1, lb7_2
            };

            Scheme mainScheme = new Scheme { Semester = s1, Id = 100, ClassModel = c1, LessonBlocks = list, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 4), SubjectDistBlocks = new List<SubjectDistBlock>() { sdb_1, sdb_2, sdb_3 } };

            e1.Schemes = new List<Scheme>() {
                mainScheme
            };

            context.Educations.AddOrUpdate(
                e => e.Id,
                e1, e2

            );

            context.Teachers.AddOrUpdate(
                t => t.Id,
                t1, t2, t3, t4, t5
            );


            context.Classes.AddOrUpdate(
                c => c.Id,
                c1, c2
                );

            context.Subjects.AddOrUpdate(
                s => s.Id,
                su1, su2, su3);

            context.Semesters.AddOrUpdate(
                s => s.Id,
                s1, s2, s3, s4);

            context.Rooms.AddOrUpdate(
                r => r.Id,
                r1, r2, r3
            );

            context.LessonBlocks.AddOrUpdate(
                x => x.Id,
                lb1_1, lb1_2,
                lb2_1, lb2_2,
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_1, lb5_2,
                lb6_1, lb6_2, lb6_3,
                lb7_1, lb7_2
            );
            
            context.Schemes.AddOrUpdate(
                x => x.Id,
                mainScheme
            );
        }
    }
}
