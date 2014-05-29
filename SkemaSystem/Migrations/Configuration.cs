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
            
            Teacher t104 = new Teacher { Id = 104, Name = "SK - TeacherSwitch", Username = "eaasommer", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t105 = new Teacher { Id = 105, Name = "ITO - TeacherSwitch", Username = "eaatk", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };

            ClassModel model1 = new ClassModel { Id = 100, Education = e1, ClassName = "Main Scheme - TeacherSwitch" };
            ClassModel model2 = new ClassModel { Id = 101, Education = e1, ClassName = "Conflict Scheme - TeacherSwitch" };

            context.Classes.AddOrUpdate(
                x => x.Id,
                model1, model2
            );

            LessonBlock lb1_1 = new LessonBlock { Id = 101, Teacher = t104, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb1_2 = new LessonBlock { Id = 102, Teacher = t104, Date = new DateTime(2014, 5, 26), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb2_1 = new LessonBlock { Id = 103, Teacher = t105, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb2_2 = new LessonBlock { Id = 104, Teacher = t105, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb3_1 = new LessonBlock { Id = 105, Teacher = t105, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb3_2 = new LessonBlock { Id = 106, Teacher = t105, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb3_3 = new LessonBlock { Id = 107, Teacher = t104, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = su1, Room = r3 };

            LessonBlock lb4_1 = new LessonBlock { Id = 108, Teacher = t104, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb4_2 = new LessonBlock { Id = 109, Teacher = t105, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb4_3 = new LessonBlock { Id = 110, Teacher = t105, Date = new DateTime(2014, 5, 29), BlockNumber = 2, Subject = su2, Room = r3 };
            LessonBlock lb4_4 = new LessonBlock { Id = 111, Teacher = t105, Date = new DateTime(2014, 5, 29), BlockNumber = 3, Subject = su2, Room = r3 };

            LessonBlock lb5_1 = new LessonBlock { Id = 112, Teacher = t105, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb5_2 = new LessonBlock { Id = 113, Teacher = t105, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb6_1 = new LessonBlock { Id = 114, Teacher = t105, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb6_2 = new LessonBlock { Id = 115, Teacher = t105, Date = new DateTime(2014, 6, 2), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb6_3 = new LessonBlock { Id = 116, Teacher = t105, Date = new DateTime(2014, 6, 2), BlockNumber = 2, Subject = su2, Room = r3 };

            LessonBlock lb7_1 = new LessonBlock { Id = 117, Teacher = t105, Date = new DateTime(2014, 6, 3), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb7_2 = new LessonBlock { Id = 118, Teacher = t105, Date = new DateTime(2014, 6, 3), BlockNumber = 1, Subject = su2, Room = r3 };

            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1, lb1_2,
                lb2_1, lb2_2,
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_1, lb5_2, 
                lb6_1, lb6_2, lb6_3,
                lb7_1, lb7_2
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 119, Teacher = t104, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r2 };

            LessonBlock lb_3_2 = new LessonBlock { Id = 120, Teacher = t104, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su1, Room = r3 };
            LessonBlock lb_3_3 = new LessonBlock { Id = 121, Teacher = t104, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = su1, Room = r3 };



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
            Teacher t204 = new Teacher { Id = 204, Name = "SK - LessonBehind", Username = "eaasommer", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t205 = new Teacher { Id = 205, Name = "ITO - LessonBehind", Username = "eaatk", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };

            ClassModel model201 = new ClassModel { Id = 200, Education = e1, ClassName = "Main Scheme - LessonBehind" };
            ClassModel model202 = new ClassModel { Id = 201, Education = e1, ClassName = "Conflict Scheme - LessonBehind" };

            context.Classes.AddOrUpdate(
                x => x.Id,
                model201, model202
            );

            LessonBlock lb1_10 = new LessonBlock { Id = 201, Teacher = t204, Date = new DateTime(2014, 5, 25), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb1_20 = new LessonBlock { Id = 202, Teacher = t204, Date = new DateTime(2014, 5, 25), BlockNumber = 1, Subject = su1, Room = r3 };
            LessonBlock lb1_30 = new LessonBlock { Id = 203, Teacher = t204, Date = new DateTime(2014, 5, 25), BlockNumber = 2, Subject = su1, Room = r3 };

            LessonBlock lb2_10 = new LessonBlock { Id = 204, Teacher = t204, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r3 };

            LessonBlock lb3_10 = new LessonBlock { Id = 205, Teacher = t204, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb3_20 = new LessonBlock { Id = 206, Teacher = t205, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb4_10 = new LessonBlock { Id = 207, Teacher = t205, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb4_20 = new LessonBlock { Id = 208, Teacher = t205, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb4_30 = new LessonBlock { Id = 209, Teacher = t205, Date = new DateTime(2014, 5, 28), BlockNumber = 2, Subject = su2, Room = r3 };

            LessonBlock lb5_10 = new LessonBlock { Id = 210, Teacher = t204, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb5_20 = new LessonBlock { Id = 211, Teacher = t205, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = su2, Room = r3 };
            LessonBlock lb5_30 = new LessonBlock { Id = 212, Teacher = t205, Date = new DateTime(2014, 5, 29), BlockNumber = 2, Subject = su2, Room = r3 };
            LessonBlock lb5_40 = new LessonBlock { Id = 213, Teacher = t205, Date = new DateTime(2014, 5, 29), BlockNumber = 3, Subject = su2, Room = r3 };

            LessonBlock lb6_10 = new LessonBlock { Id = 214, Teacher = t204, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb7_10 = new LessonBlock { Id = 215, Teacher = t204, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su1, Room = r3 };

            List<LessonBlock> list200 = new List<LessonBlock> { 
                lb1_10, lb1_20, lb1_30,
                lb2_10, 
                lb3_10, lb3_20, 
                lb4_10, lb4_20, lb4_30, 
                lb5_10, lb5_20, lb5_30, lb5_40,
                lb6_10,
                lb7_10,
            };

            LessonBlock lb_1_10 = new LessonBlock { Id = 216, Teacher = t204, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r2 };
            LessonBlock lb_1_40 = new LessonBlock { Id = 217, Teacher = t204, Date = new DateTime(2014, 5, 26), BlockNumber = 3, Subject = su1, Room = r2 };

            LessonBlock lb_2_10 = new LessonBlock { Id = 218, Teacher = t204, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb_2_20 = new LessonBlock { Id = 219, Teacher = t204, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su1, Room = r3 };
            LessonBlock lb_2_30 = new LessonBlock { Id = 220, Teacher = t204, Date = new DateTime(2014, 5, 27), BlockNumber = 2, Subject = su1, Room = r3 };

            LessonBlock lb_3_10 = new LessonBlock { Id = 221, Teacher = t204, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb_3_20 = new LessonBlock { Id = 222, Teacher = t204, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb_4_10 = new LessonBlock { Id = 223, Teacher = t204, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su1, Room = r3 };

            LessonBlock lb_5_10 = new LessonBlock { Id = 224, Teacher = t204, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb_5_20 = new LessonBlock { Id = 225, Teacher = t204, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb_6_10 = new LessonBlock { Id = 226, Teacher = t204, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su1, Room = r3 };



            List<LessonBlock> listConflict200 = new List<LessonBlock>
            {
                lb_1_10, lb_1_40,
                lb_2_10, lb_2_20, lb_2_30,
                lb_3_10, lb_3_20,
                lb_4_10,
                lb_5_10, lb_5_20,
                lb_6_10
            };

            context.LessonBlocks.AddOrUpdate(
                x => x.Id,
                lb1_10, lb1_20, lb1_30,
                lb2_10,
                lb3_10, lb3_20,
                lb4_10, lb4_20, lb4_30,
                lb5_10, lb5_20, lb5_30, lb5_40,
                lb6_10,
                lb7_10,

                lb_1_10, lb_1_40,
                lb_2_10, lb_2_20, lb_2_30,
                lb_3_10, lb_3_20,
                lb_4_10,
                lb_5_10, lb_5_20,
                lb_6_10
            );

            Scheme mainScheme200 = new Scheme { Semester = s1, Id = 200, ClassModel = model201, LessonBlocks = list200, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 7) };
            Scheme conflictScheme201 = new Scheme { Semester = s1, Id = 201, ClassModel = model202, LessonBlocks = listConflict200, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 7) };

            context.Schemes.AddOrUpdate(
                x => x.Id,
                mainScheme200, conflictScheme201
            );

            //TestData for FindAHole
            Teacher t304 = new Teacher { Id = 304, Name = "SK - Hole", Username = "eaasommer", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };
            Teacher t305 = new Teacher { Id = 305, Name = "ITO - Hole", Username = "eaatk", Password = "fisk123", Role = Models.Enum.UserRoles.Teacher };

            ClassModel model301 = new ClassModel { Id = 300, Education = e1, ClassName = "Main Scheme - Hole" };
            ClassModel model302 = new ClassModel { Id = 301, Education = e1, ClassName = "Conflict Scheme - Hole" };

            context.Classes.AddOrUpdate(
                x => x.Id,
                model301, model302
            );

            LessonBlock lb1_100 = new LessonBlock { Id = 301, Teacher = t304, Date = new DateTime(2014, 5, 26), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb1_200 = new LessonBlock { Id = 302, Teacher = t304, Date = new DateTime(2014, 5, 26), BlockNumber = 1, Subject = su1, Room = r3 };
            LessonBlock lb1_300 = new LessonBlock { Id = 303, Teacher = t304, Date = new DateTime(2014, 5, 26), BlockNumber = 2, Subject = su1, Room = r3 };
            LessonBlock lb1_400 = new LessonBlock { Id = 304, Teacher = t304, Date = new DateTime(2014, 5, 26), BlockNumber = 3, Subject = su1, Room = r3 };

            LessonBlock lb2_100 = new LessonBlock { Id = 305, Teacher = t305, Date = new DateTime(2014, 5, 27), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb2_200 = new LessonBlock { Id = 306, Teacher = t305, Date = new DateTime(2014, 5, 27), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb3_100 = new LessonBlock { Id = 307, Teacher = t304, Date = new DateTime(2014, 5, 28), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb3_200 = new LessonBlock { Id = 308, Teacher = t304, Date = new DateTime(2014, 5, 28), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb4_100 = new LessonBlock { Id = 309, Teacher = t305, Date = new DateTime(2014, 5, 29), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb4_200 = new LessonBlock { Id = 310, Teacher = t305, Date = new DateTime(2014, 5, 29), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb5_100 = new LessonBlock { Id = 311, Teacher = t304, Date = new DateTime(2014, 5, 30), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb5_200 = new LessonBlock { Id = 312, Teacher = t304, Date = new DateTime(2014, 5, 30), BlockNumber = 1, Subject = su1, Room = r3 };

            LessonBlock lb7_100 = new LessonBlock { Id = 313, Teacher = t305, Date = new DateTime(2014, 6, 3), BlockNumber = 0, Subject = su2, Room = r3 };
            LessonBlock lb7_200 = new LessonBlock { Id = 314, Teacher = t305, Date = new DateTime(2014, 6, 3), BlockNumber = 1, Subject = su2, Room = r3 };

            LessonBlock lb9_100 = new LessonBlock { Id = 315, Teacher = t304, Date = new DateTime(2014, 6, 5), BlockNumber = 0, Subject = su1, Room = r3 };
            LessonBlock lb9_200 = new LessonBlock { Id = 316, Teacher = t304, Date = new DateTime(2014, 6, 5), BlockNumber = 1, Subject = su1, Room = r3 };

            List<LessonBlock> list300 = new List<LessonBlock> { 
                lb1_100, lb1_200, lb1_300, lb1_400,
                lb2_100, lb2_200, 
                lb3_100, lb3_200, 
                lb4_100, lb4_200, 
                lb5_100, lb5_200,
                lb7_100, lb7_200,
                lb9_100, lb9_200
            };

            LessonBlock lb_1_100 = new LessonBlock { Id = 317, Teacher = t304, Date = new DateTime(2014, 6, 2), BlockNumber = 0, Subject = su1, Room = r2 };
            LessonBlock lb_1_200 = new LessonBlock { Id = 318, Teacher = t304, Date = new DateTime(2014, 6, 2), BlockNumber = 1, Subject = su1, Room = r2 };

            List<LessonBlock> listConflict300 = new List<LessonBlock>
            {
                lb_1_100, lb_1_200
            };

            context.LessonBlocks.AddOrUpdate(
                x => x.Id,
                lb1_100, lb1_200, lb1_300, lb1_400,
                lb2_100, lb2_200, 
                lb3_100, lb3_200, 
                lb4_100, lb4_200, 
                lb5_100, lb5_200,
                lb7_100, lb7_200,
                lb9_100, lb9_200,

                lb_1_100, lb_1_200
            );

            Scheme mainScheme300 = new Scheme { Semester = s1, Id = 300, ClassModel = model301, LessonBlocks = list300, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 15) };
            Scheme conflictScheme300 = new Scheme { Semester = s1, Id = 301, ClassModel = model302, LessonBlocks = listConflict300, SemesterStart = new DateTime(2014, 5, 26), SemesterFinish = new DateTime(2014, 6, 15) };

            context.Schemes.AddOrUpdate(
                x => x.Id,
                mainScheme300, conflictScheme300
            );
        }
    }
}
