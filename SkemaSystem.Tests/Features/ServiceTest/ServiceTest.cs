using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using SkemaSystem.Services;

namespace SkemaSystem.Tests.Features.ServiceTest
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void setNewSemesterForClassTest()
        {
            Semester semester1 = new Semester { Id = 1, Number = 1 };
            Semester semester2 = new Semester { Id = 2, Number = 2 };
            Education education = new Education { Name = "DMU", Semesters = new List<Semester> { semester1, semester2 }, NumberOfSemesters = 2 };
            ClassModel model = new ClassModel { Education = education, ClassName = "12T" };

            DateTime start = DateTime.Today;
            DateTime finish = DateTime.Today.AddDays(30);

            ConflictService service = new ConflictService();

            service.setNewSemesterForClass(model, semester1, start, finish);

            Assert.AreEqual(start, model.ActiveSchemes[0].SemesterStart);
            Assert.AreEqual(finish, model.ActiveSchemes[0].SemesterFinish);
        }

        /*[TestMethod]
        public void TestFindAHole()
        {
            ClassModel model1 = new ClassModel();

            Teacher t1 = new Teacher { Id = 1, Name = "TK" };
            Teacher t2 = new Teacher { Id = 2, Name = "HS" };

            LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 0 };
            LessonBlock lb1_2 = new LessonBlock { Id = 2, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 1 };
            LessonBlock lb1_3 = new LessonBlock { Id = 3, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 1 };
            LessonBlock lb1_4 = new LessonBlock { Id = 4, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 2 };

            LessonBlock lb2_1 = new LessonBlock { Id = 5, Teacher = t2, Date = new DateTime(2014, 5, 27), BlockNumber = 0 };
            LessonBlock lb2_2 = new LessonBlock { Id = 6, Teacher = t2, Date = new DateTime(2014, 5, 27), BlockNumber = 1 };

            LessonBlock lb3_1 = new LessonBlock { Id = 7, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 0 };
            LessonBlock lb3_2 = new LessonBlock { Id = 8, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 1 };

            LessonBlock lb4_1 = new LessonBlock { Id = 9, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 0 };
            LessonBlock lb4_2 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 1 };

            LessonBlock lb5_1 = new LessonBlock { Id = 11, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 0 };
            LessonBlock lb5_2 = new LessonBlock { Id = 12, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 1 };

            LessonBlock lb7_1 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014, 6, 3), BlockNumber = 0 };
            LessonBlock lb7_2 = new LessonBlock { Id = 14, Teacher = t2, Date = new DateTime(2014, 6, 3), BlockNumber = 1 };

            LessonBlock lb9_1 = new LessonBlock { Id = 15, Teacher = t1, Date = new DateTime(2014, 6, 5), BlockNumber = 0 };
            LessonBlock lb9_2 = new LessonBlock { Id = 16, Teacher = t1, Date = new DateTime(2014, 6, 5), BlockNumber = 1 };

            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1, lb1_2, lb1_3, lb1_4,
                lb2_1, lb2_2, 
                lb3_1, lb3_2, 
                lb4_1, lb4_2, 
                lb5_1, lb5_2,
                lb7_1, lb7_2,
                lb9_1, lb9_2
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 99, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 0 };
            LessonBlock lb_1_2 = new LessonBlock { Id = 100, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 1 };

            List<LessonBlock> listConflict = new List<LessonBlock>
            {
                lb_1_1, lb_1_2
            };

            Scheme mainScheme = new Scheme { ClassModel = model1, LessonBlocks = list, SemesterFinish = new DateTime(2014, 6, 15) };

            List<LessonBlock> blocks = new List<LessonBlock> { lb1_1, lb1_2 };

            ConflictService service = new ConflictService();

            Dictionary<DateTime, int> availableBlocks = service.FindAHoleInScheme(mainScheme, listConflict, blocks, new DateTime(2014, 5, 26));

            DateTime date = new DateTime(2014, 6, 4);
            Assert.AreEqual(0, availableBlocks[date]);
            date = new DateTime(2014, 6, 6);
            Assert.AreEqual(0, availableBlocks[date]);
            date = new DateTime(2014, 6, 9);
            Assert.AreEqual(0, availableBlocks[date]);

            mainScheme.SemesterFinish = new DateTime(2014, 6, 7);
            availableBlocks = service.FindAHoleInScheme(mainScheme, listConflict, blocks, new DateTime(2014, 5, 26));

            date = new DateTime(2014, 6, 4);
            Assert.AreEqual(0, availableBlocks[date]);
            date = new DateTime(2014, 6, 6);
            Assert.AreEqual(0, availableBlocks[date]);
            Assert.AreEqual(2, availableBlocks.Count());
        }

        [TestMethod]
        public void TestSetLessonBehindOwnLesson()
        {
            ClassModel model1 = new ClassModel();

            Teacher t1 = new Teacher { Id = 1, Name = "JH" };
            Teacher t2 = new Teacher { Id = 2, Name = "KR" };

            LessonBlock lb0_1 = new LessonBlock { Id = -2, Teacher = t1, Date = new DateTime(2014, 5, 25), BlockNumber = 0 };
            LessonBlock lb0_2 = new LessonBlock { Id = -1, Teacher = t1, Date = new DateTime(2014, 5, 25), BlockNumber = 1 };
            LessonBlock lb0_3 = new LessonBlock { Id = 0, Teacher = t1, Date = new DateTime(2014, 5, 25), BlockNumber = 2 };

            LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 0 };

            LessonBlock lb2_1 = new LessonBlock { Id = 5, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 0 };
            LessonBlock lb2_2 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 1 };

            LessonBlock lb3_1 = new LessonBlock { Id = 7, Teacher = t2, Date = new DateTime(2014, 5, 28), BlockNumber = 0 };
            LessonBlock lb3_2 = new LessonBlock { Id = 8, Teacher = t2, Date = new DateTime(2014, 5, 28), BlockNumber = 1 };
            LessonBlock lb3_3 = new LessonBlock { Id = 8, Teacher = t2, Date = new DateTime(2014, 5, 28), BlockNumber = 2 };

            LessonBlock lb4_1 = new LessonBlock { Id = 9, Teacher = t1, Date = new DateTime(2014, 5, 29), BlockNumber = 0 };
            LessonBlock lb4_2 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 1 };
            LessonBlock lb4_3 = new LessonBlock { Id = 9, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 2 };
            LessonBlock lb4_4 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 3 };

            LessonBlock lb5_2 = new LessonBlock { Id = 10, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 1 };

            LessonBlock lb6_1 = new LessonBlock { Id = 13, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 0 };

            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1,
                lb2_1, lb2_2, 
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_2,
                lb6_1
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 0 };
            LessonBlock lb_1_4 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 3 };

            LessonBlock lb_2_1 = new LessonBlock { Id = 5, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 0 };
            LessonBlock lb_2_2 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 1 };
            LessonBlock lb_2_3 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 2 };

            LessonBlock lb_3_1 = new LessonBlock { Id = 7, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 0 };
            LessonBlock lb_3_2 = new LessonBlock { Id = 8, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 1 };

            LessonBlock lb_4_1 = new LessonBlock { Id = 9, Teacher = t1, Date = new DateTime(2014, 5, 29), BlockNumber = 0 };

            LessonBlock lb_5_1 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 0 };
            LessonBlock lb_5_2 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 1 };

            LessonBlock lb_6_1 = new LessonBlock { Id = 13, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 0 };

            List<LessonBlock> listConflict = new List<LessonBlock>
            {
                lb_1_1, lb_1_4,
                lb_2_1, lb_2_2, lb_2_3,
                lb_3_1, lb_3_2,
                lb_4_1,
                lb_5_1, lb_5_2,
                lb_6_1
            };

            Scheme mainScheme = new Scheme { ClassModel = model1, LessonBlocks = list, SemesterFinish = new DateTime(2014, 6, 7) };

            ConflictService service = new ConflictService();

            List<LessonBlock> blocks = new List<LessonBlock> { lb0_1, lb0_2 };
            Dictionary<DateTime, int> availableBlocks = service.setLessonBehindOwnLesson(mainScheme, listConflict, blocks, new DateTime(2014, 5, 26));

            DateTime date = new DateTime(2014, 6, 9);
            Assert.AreEqual(1, availableBlocks[date]);
            date = new DateTime(2014, 6, 9);
            Assert.AreEqual(2, availableBlocks[date]);
            date = new DateTime(2014, 6, 9);
            Assert.AreEqual(1, availableBlocks[date]);

            blocks = new List<LessonBlock> { lb0_1, lb0_2, lb0_3 };
            availableBlocks = service.setLessonBehindOwnLesson(mainScheme, listConflict, blocks, new DateTime(2014, 5, 26));

            date = new DateTime(2014, 6, 9);
            Assert.AreEqual(1, availableBlocks[date]);
            Assert.AreEqual(1, availableBlocks.Count());
        }

        [TestMethod]
        public void TestSwitchWithOtherTeacher()
        {
            Education edu = new Education { Name = "DMU" };
            ClassModel model = new ClassModel { Id = 1, Education = edu };

            Teacher t1 = new Teacher { Name = "TK" };
            Teacher t2 = new Teacher { Name = "KR" };

            LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 0 };
            LessonBlock lb1_2 = new LessonBlock { Id = 2, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 1 };

            List<LessonBlock> blocks = new List<LessonBlock>
            {
                lb1_1,
                lb1_2
            };

            LessonBlock lb2_1 = new LessonBlock { Id = 5, Teacher = t2, Date = new DateTime(2014, 5, 27), BlockNumber = 0 };
            LessonBlock lb2_2 = new LessonBlock { Id = 6, Teacher = t2, Date = new DateTime(2014, 5, 27), BlockNumber = 1 };

            LessonBlock lb3_1 = new LessonBlock { Id = 7, Teacher = t2, Date = new DateTime(2014, 5, 28), BlockNumber = 0 };
            LessonBlock lb3_2 = new LessonBlock { Id = 8, Teacher = t2, Date = new DateTime(2014, 5, 28), BlockNumber = 1 };
            LessonBlock lb3_3 = new LessonBlock { Id = 8, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 2 };

            LessonBlock lb4_1 = new LessonBlock { Id = 9, Teacher = t1, Date = new DateTime(2014, 5, 29), BlockNumber = 0 };
            LessonBlock lb4_2 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 1 };
            LessonBlock lb4_3 = new LessonBlock { Id = 9, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 2 };
            LessonBlock lb4_4 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 3 };

            LessonBlock lb5_1 = new LessonBlock { Id = 11, Teacher = t2, Date = new DateTime(2014, 5, 30), BlockNumber = 0 };
            LessonBlock lb5_2 = new LessonBlock { Id = 11, Teacher = t2, Date = new DateTime(2014, 5, 30), BlockNumber = 1 };

            LessonBlock lb6_1 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014, 6, 2), BlockNumber = 0 };
            LessonBlock lb6_2 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014, 6, 2), BlockNumber = 1 };
            LessonBlock lb6_3 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014, 6, 2), BlockNumber = 2 };

            LessonBlock lb7_1 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014, 6, 3), BlockNumber = 0 };
            LessonBlock lb7_2 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014, 6, 3), BlockNumber = 1 };

            List<LessonBlock> list = new List<LessonBlock> 
            {
                lb1_1, lb1_2,
                lb2_1, lb2_2,
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_1, lb5_2,
                lb6_1, lb6_2, lb6_3,
                lb7_1, lb7_2
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 99, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 0 };

            LessonBlock lb_3_2 = new LessonBlock { Id = 99, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 1 };
            LessonBlock lb_3_3 = new LessonBlock { Id = 99, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 2 };

            List<LessonBlock> conflictlist = new List<LessonBlock>
            {
                lb_1_1,
                lb_3_2, lb_3_3
            };

            Scheme mainScheme = new Scheme { ClassModel = model, LessonBlocks = list, SemesterFinish = new DateTime(2014, 6, 4) };

            ConflictService service = new ConflictService();

            Dictionary<DateTime, int> availableBlocks = service.switchWithOtherTeacher(mainScheme, conflictlist, blocks, t2, new DateTime(2014, 5, 26));

            DateTime date = new DateTime(2014, 5, 27);
            Assert.AreEqual(0, availableBlocks[date]);
            date = new DateTime(2014, 5, 29);
            Assert.AreEqual(1, availableBlocks[date]);
            date = new DateTime(2014, 5, 30);
            Assert.AreEqual(0, availableBlocks[date]);

            blocks.Add(new LessonBlock { Date = new DateTime(2014, 5, 26), BlockNumber = 3, Teacher = t1 });
            availableBlocks = service.switchWithOtherTeacher(mainScheme, conflictlist, blocks, t2, new DateTime(2014,5,26));

            date = new DateTime(2014, 5, 29);
            Assert.AreEqual(1, availableBlocks[date]);
            date = new DateTime(2014, 6, 2);
            Assert.AreEqual(2, availableBlocks[date]);
        }*/
    }
}
