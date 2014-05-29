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

            Teacher t1 = new Teacher { Id = 1, Name = "Hanne Sommer", Username = "eaasommer", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t2 = new Teacher { Id = 2, Name = "Torben Krøjmand", Username = "eaatk", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t3 = new Teacher { Id = 3, Name = "Erik Jacobsen", Username = "eaaej", Password = "fisk123", Role = Models.Enum.UserRoles.Master };
            Teacher t4 = new Teacher { Id = 4, Name = "Jörn Hujak", Username = "eaajh", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t5 = new Teacher { Id = 5, Name = "Karsten ITO", Username = "eaakarsten", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };

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

            //Testdata for TeacherSwitch
            ClassModel model1 = new ClassModel { Id = 100, Education = e1, ClassName = "Main Scheme" };
            ClassModel model2 = new ClassModel { Id = 101, Education = e1, ClassName = "Conflict Scheme" };

            context.Classes.AddOrUpdate(
                x => x.Id,
                model1, model2
            );

            LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb1_2 = new LessonBlock { Id = 2, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb2_1 = new LessonBlock { Id = 3, Teacher = t5, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb2_2 = new LessonBlock { Id = 4, Teacher = t5, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb3_1 = new LessonBlock { Id = 5, Teacher = t5, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb3_2 = new LessonBlock { Id = 6, Teacher = t5, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb3_3 = new LessonBlock { Id = 7, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = su1, Room = r3 };

            LessonBlock lb4_1 = new LessonBlock { Id = 8, Teacher = t4, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb4_2 = new LessonBlock { Id = 9, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb4_3 = new LessonBlock { Id = 10, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 2, Subject = su2, Room = r3 };
            LessonBlock lb4_4 = new LessonBlock { Id = 11, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 3, Subject = su2, Room = r3 };

            LessonBlock lb5_1 = new LessonBlock { Id = 12, Teacher = t5, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb5_2 = new LessonBlock { Id = 13, Teacher = t5, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb6_1 = new LessonBlock { Id = 14, Teacher = t5, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb6_2 = new LessonBlock { Id = 15, Teacher = t5, Date = new DateTime(2014, 6, 2), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb6_3 = new LessonBlock { Id = 16, Teacher = t5, Date = new DateTime(2014, 6, 2), BlockNumber = 2, Subject = su2, Room = r3 };

            LessonBlock lb7_1 = new LessonBlock { Id = 17, Teacher = t5, Date = new DateTime(2014, 6, 3), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb7_2 = new LessonBlock { Id = 18, Teacher = t5, Date = new DateTime(2014, 6, 3), BlockNumber = 1, Subject = su2, Room = r3 };

            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1, lb1_2,
                lb2_1, lb2_2,
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_1, lb5_2, 
                lb6_1, lb6_2, lb6_3,
                lb7_1, lb7_2
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 19, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r2 };

            LessonBlock lb_3_2 = new LessonBlock { Id = 20, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su1, Room = r3 };
            LessonBlock lb_3_3 = new LessonBlock { Id = 21, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = su1, Room = r3 };



            List<LessonBlock> listConflict = new List<LessonBlock>
            {
                lb_1_1,
                lb_3_2, lb_3_3
            };

            context.LessonBlocks.AddOrUpdate(
                x => x.Id,
                lb1_1, lb1_2,
                lb2_1, lb2_2,
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_1, lb5_2,
                lb6_1, lb6_2, lb6_3,
                lb7_1, lb7_2,

                lb_1_1,
                lb_3_2, lb_3_3
            );

            SubjectDistBlock sdb_1 = new SubjectDistBlock() { Id = 1, BlocksCount = 10, Subject = su2, Teacher = t5 };

            Scheme mainScheme = new Scheme { Semester = s1, Id = 100, ClassModel = model1, LessonBlocks = list, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 4), SubjectDistBlocks = new List<SubjectDistBlock>() { sdb_1 } };
            Scheme conflictScheme = new Scheme { Semester = s1, Id = 101, ClassModel = model2, LessonBlocks = listConflict, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 4) };

            context.Schemes.AddOrUpdate(
                x => x.Id,
                mainScheme, conflictScheme
            );


            //Testdata for set lessen behind own lessonblock
            //ClassModel model1 = new ClassModel { Id = 100, Education = e1, ClassName = "Main Scheme" };
            //ClassModel model2 = new ClassModel { Id = 101, Education = e1, ClassName = "Conflict Scheme" };

            //context.Classes.AddOrUpdate(
            //    x => x.Id,
            //    model1, model2
            //);

            //LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t4, Date = new DateTime(2014, 5, 25), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb1_2 = new LessonBlock { Id = 2, Teacher = t4, Date = new DateTime(2014, 5, 25), BlockNumber = 1, Subject = su1, Room = r3 };
            //LessonBlock lb1_3 = new LessonBlock { Id = 3, Teacher = t4, Date = new DateTime(2014, 5, 25), BlockNumber = 2, Subject = su1, Room = r3 };

            //LessonBlock lb2_1 = new LessonBlock { Id = 4, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r3 };

            //LessonBlock lb3_1 = new LessonBlock { Id = 5, Teacher = t4, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb3_2 = new LessonBlock { Id = 6, Teacher = t5, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su2, Room = r3 };

            //LessonBlock lb4_1 = new LessonBlock { Id = 7, Teacher = t5, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su2, Room = r3 };
            //LessonBlock lb4_2 = new LessonBlock { Id = 8, Teacher = t5, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su2, Room = r3 };
            //LessonBlock lb4_3 = new LessonBlock { Id = 9, Teacher = t5, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = su2, Room = r3 };

            //LessonBlock lb5_1 = new LessonBlock { Id = 10, Teacher = t4, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb5_2 = new LessonBlock { Id = 11, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = su2, Room = r3 };
            //LessonBlock lb5_3 = new LessonBlock { Id = 12, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 2, Subject = su2, Room = r3 };
            //LessonBlock lb5_4 = new LessonBlock { Id = 13, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 3, Subject = su2, Room = r3 };

            //LessonBlock lb6_1 = new LessonBlock { Id = 14, Teacher = t4, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su1, Room = r3 };

            //LessonBlock lb7_1 = new LessonBlock { Id = 15, Teacher = t4, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su1, Room = r3 };

            //List<LessonBlock> list = new List<LessonBlock> { 
            //    lb1_1, lb1_2, lb1_3,
            //    lb2_1, 
            //    lb3_1, lb3_2, 
            //    lb4_1, lb4_2, lb4_3, 
            //    lb5_1, lb5_2, lb5_3, lb5_4,
            //    lb6_1,
            //    lb7_1,
            //};

            //LessonBlock lb_1_1 = new LessonBlock { Id = 16, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r2 };
            //LessonBlock lb_1_4 = new LessonBlock { Id = 17, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 3, Subject = su1, Room = r2 };

            //LessonBlock lb_2_1 = new LessonBlock { Id = 18, Teacher = t4, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb_2_2 = new LessonBlock { Id = 19, Teacher = t4, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su1, Room = r3 };
            //LessonBlock lb_2_3 = new LessonBlock { Id = 20, Teacher = t4, Date = new DateTime(2014, 5, 27), BlockNumber = 2, Subject = su1, Room = r3 };

            //LessonBlock lb_3_1 = new LessonBlock { Id = 21, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb_3_2 = new LessonBlock { Id = 22, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su1, Room = r3 };

            //LessonBlock lb_4_1 = new LessonBlock { Id = 23, Teacher = t4, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su1, Room = r3 };

            //LessonBlock lb_5_1 = new LessonBlock { Id = 24, Teacher = t4, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb_5_2 = new LessonBlock { Id = 25, Teacher = t4, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su1, Room = r3 };

            //LessonBlock lb_6_1 = new LessonBlock { Id = 26, Teacher = t4, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su1, Room = r3 };



            //List<LessonBlock> listConflict = new List<LessonBlock>
            //{
            //    lb_1_1, lb_1_4,
            //    lb_2_1, lb_2_2, lb_2_3,
            //    lb_3_1, lb_3_2,
            //    lb_4_1,
            //    lb_5_1, lb_5_2,
            //    lb_6_1
            //};

            //context.LessonBlocks.AddOrUpdate(
            //    x => x.Id,
            //    lb1_1, lb1_2, lb1_3,
            //    lb2_1,
            //    lb3_1, lb3_2,
            //    lb4_1, lb4_2, lb4_3,
            //    lb5_1, lb5_2, lb5_3, lb5_4,
            //    lb6_1,
            //    lb7_1,

            //    lb_1_1, lb_1_4,
            //    lb_2_1, lb_2_2, lb_2_3,
            //    lb_3_1, lb_3_2,
            //    lb_4_1,
            //    lb_5_1, lb_5_2,
            //    lb_6_1
            //);

            //Scheme mainScheme = new Scheme { Semester = s1, Id = 100, ClassModel = model1, LessonBlocks = list, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 7) };
            //Scheme conflictScheme = new Scheme { Semester = s1, Id = 101, ClassModel = model2, LessonBlocks = listConflict, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 7) };

            //context.Schemes.AddOrUpdate(
            //    x => x.Id,
            //    mainScheme, conflictScheme
            //);

            ////TestData for FindAHole
            //ClassModel model1 = new ClassModel { Id = 100, Education = e1, ClassName = "Main Scheme" };
            //ClassModel model2 = new ClassModel { Id = 101, Education = e1, ClassName = "Conflict Scheme" };

            //context.Classes.AddOrUpdate(
            //    x => x.Id,
            //    model1, model2
            //);

            //LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb1_2 = new LessonBlock { Id = 2, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 1, Subject = su1, Room = r3 };
            //LessonBlock lb1_3 = new LessonBlock { Id = 3, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 2, Subject = su1, Room = r3 };
            //LessonBlock lb1_4 = new LessonBlock { Id = 4, Teacher = t4, Date = new DateTime(2014, 5, 26), BlockNumber = 3, Subject = su1, Room = r3 };

            //LessonBlock lb2_1 = new LessonBlock { Id = 5, Teacher = t5, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su2, Room = r3 };
            //LessonBlock lb2_2 = new LessonBlock { Id = 6, Teacher = t5, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su2, Room = r3 };

            //LessonBlock lb3_1 = new LessonBlock { Id = 7, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb3_2 = new LessonBlock { Id = 8, Teacher = t4, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su1, Room = r3 };

            //LessonBlock lb4_1 = new LessonBlock { Id = 9, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su2, Room = r3 };
            //LessonBlock lb4_2 = new LessonBlock { Id = 10, Teacher = t5, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = su2, Room = r3 };

            //LessonBlock lb5_1 = new LessonBlock { Id = 11, Teacher = t4, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb5_2 = new LessonBlock { Id = 12, Teacher = t4, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su1, Room = r3 };

            //LessonBlock lb7_1 = new LessonBlock { Id = 13, Teacher = t5, Date = new DateTime(2014, 6, 3), BlockNumber = 0, Subject = su2, Room = r3 };
            //LessonBlock lb7_2 = new LessonBlock { Id = 14, Teacher = t5, Date = new DateTime(2014, 6, 3), BlockNumber = 1, Subject = su2, Room = r3 };

            //LessonBlock lb9_1 = new LessonBlock { Id = 15, Teacher = t4, Date = new DateTime(2014, 6, 5), BlockNumber = 0, Subject = su1, Room = r3 };
            //LessonBlock lb9_2 = new LessonBlock { Id = 16, Teacher = t4, Date = new DateTime(2014, 6, 5), BlockNumber = 1, Subject = su1, Room = r3 };

            //List<LessonBlock> list = new List<LessonBlock> { 
            //    lb1_1, lb1_2, lb1_3, lb1_4,
            //    lb2_1, lb2_2, 
            //    lb3_1, lb3_2, 
            //    lb4_1, lb4_2, 
            //    lb5_1, lb5_2,
            //    lb7_1, lb7_2,
            //    lb9_1, lb9_2
            //};

            //LessonBlock lb_1_1 = new LessonBlock { Id = 17, Teacher = t4, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su1, Room = r2 };
            //LessonBlock lb_1_2 = new LessonBlock { Id = 18, Teacher = t4, Date = new DateTime(2014, 6, 2), BlockNumber = 1, Subject = su1, Room = r2 };

            //List<LessonBlock> listConflict = new List<LessonBlock>
            //{
            //    lb_1_1, lb_1_2
            //};

            //context.LessonBlocks.AddOrUpdate(
            //    x => x.Id,
            //    lb1_1, lb1_2, lb1_3, lb1_4,
            //    lb2_1, lb2_2,
            //    lb3_1, lb3_2,
            //    lb4_1, lb4_2,
            //    lb5_1, lb5_2,
            //    lb7_1, lb7_2,
            //    lb9_1, lb9_2,

            //    lb_1_1, lb_1_2
            //);

            //Scheme mainScheme = new Scheme { Semester = s1, Id = 100, ClassModel = model1, LessonBlocks = list, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 15) };
            //Scheme conflictScheme = new Scheme { Semester = s1, Id = 101, ClassModel = model2, LessonBlocks = listConflict, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 15) };

            //context.Schemes.AddOrUpdate(
            //    x => x.Id,
            //    mainScheme, conflictScheme
            //);
        }
    }
}
