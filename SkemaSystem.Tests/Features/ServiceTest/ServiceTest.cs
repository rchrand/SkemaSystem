﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using SkemaSystem.Service;

namespace SkemaSystem.Tests.Features.ServiceTest
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void TestFindAHole()
        {
            ClassModel model1 = new ClassModel();

            Teacher t1 = new Teacher { Id = 1, Name = "TK"};
            Teacher t2 = new Teacher { Id = 2, Name = "HS" };

            LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014,5,26), BlockNumber = 1 };
            LessonBlock lb1_2 = new LessonBlock { Id = 2, Teacher = t1, Date = new DateTime(2014,5,26), BlockNumber = 2 };
            LessonBlock lb1_3 = new LessonBlock { Id = 3, Teacher = t1, Date = new DateTime(2014,5,26), BlockNumber = 3 };
            LessonBlock lb1_4 = new LessonBlock { Id = 4, Teacher = t1, Date = new DateTime(2014,5,26), BlockNumber = 4 };

            LessonBlock lb2_1 = new LessonBlock { Id = 5, Teacher = t2, Date = new DateTime(2014,5,27), BlockNumber = 1 };
            LessonBlock lb2_2 = new LessonBlock { Id = 6, Teacher = t2, Date = new DateTime(2014,5,27), BlockNumber = 2 };

            LessonBlock lb3_1 = new LessonBlock { Id = 7, Teacher = t1, Date = new DateTime(2014,5,28), BlockNumber = 1 };
            LessonBlock lb3_2 = new LessonBlock { Id = 8, Teacher = t1, Date = new DateTime(2014,5,28), BlockNumber = 2 };
            
            LessonBlock lb4_1 = new LessonBlock { Id = 9, Teacher = t2, Date = new DateTime(2014,5,29), BlockNumber = 1 };
            LessonBlock lb4_2 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014,5,29), BlockNumber = 2 };
            
            LessonBlock lb5_1 = new LessonBlock { Id = 11, Teacher = t1, Date = new DateTime(2014,5,30), BlockNumber = 1 };
            LessonBlock lb5_2 = new LessonBlock { Id = 12, Teacher = t1, Date = new DateTime(2014,5,30), BlockNumber = 2 };

            LessonBlock lb7_1 = new LessonBlock { Id = 13, Teacher = t2, Date = new DateTime(2014,6,3), BlockNumber = 1 };
            LessonBlock lb7_2 = new LessonBlock { Id = 14, Teacher = t2, Date = new DateTime(2014,6,3), BlockNumber = 2 };

            LessonBlock lb9_1 = new LessonBlock { Id = 15, Teacher = t1, Date = new DateTime(2014, 6, 5), BlockNumber = 1 };
            LessonBlock lb9_2 = new LessonBlock { Id = 16, Teacher = t1, Date = new DateTime(2014, 6, 5), BlockNumber = 2 };
            
            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1, lb1_2, lb1_3, lb1_4,
                lb2_1, lb2_2, 
                lb3_1, lb3_2, 
                lb4_1, lb4_2, 
                lb5_1, lb5_2,
                lb7_1, lb7_2,
                lb9_1, lb9_2
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 99, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 1 };
            LessonBlock lb_1_2 = new LessonBlock { Id = 100, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 2 };

            List<LessonBlock> listConflict = new List<LessonBlock>
            {
                lb_1_1, lb_1_2
            };

            Scheme mainScheme = new Scheme { ClassModel = model1, LessonBlocks = list, SemesterFinish = new DateTime(2014, 6, 15) };

            List<LessonBlock> blocks = new List<LessonBlock> { lb1_1, lb1_2 };

            Service.Service service = new Service.Service();

            List<DateTime> availableBlocks = service.FindAHoleInScheme(mainScheme, listConflict, blocks, new DateTime(2014,5,26));

            Assert.AreEqual(new DateTime(2014, 6, 4), availableBlocks[0]);
            Assert.AreEqual(new DateTime(2014, 6, 6), availableBlocks[1]);
            Assert.AreEqual(new DateTime(2014, 6, 9), availableBlocks[2]);

            mainScheme.SemesterFinish = new DateTime(2014, 6, 7);
            availableBlocks = service.FindAHoleInScheme(mainScheme, listConflict, blocks, new DateTime(2014,5,26));

            Assert.AreEqual(new DateTime(2014, 6, 4), availableBlocks[0]);
            Assert.AreEqual(new DateTime(2014, 6, 6), availableBlocks[1]);
        }

        [TestMethod]
        public void TestSetLessonBehindOwnLesson()
        {
            ClassModel model1 = new ClassModel();

            Teacher t1 = new Teacher { Id = 1, Name = "JH" };
            Teacher t2 = new Teacher { Id = 2, Name = "KR" };

            LessonBlock lb0_1 = new LessonBlock { Id = -2, Teacher = t1, Date = new DateTime(2014, 5, 25), BlockNumber = 1 };
            LessonBlock lb0_2 = new LessonBlock { Id = -1, Teacher = t1, Date = new DateTime(2014, 5, 25), BlockNumber = 2 };
            LessonBlock lb0_3 = new LessonBlock { Id = 0, Teacher = t1, Date = new DateTime(2014, 5, 25), BlockNumber = 3 };

            LessonBlock lb1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014,5,26), BlockNumber = 1 };

            LessonBlock lb2_1 = new LessonBlock { Id = 5, Teacher = t1, Date = new DateTime(2014,5,27), BlockNumber = 1 };
            LessonBlock lb2_2 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014,5,27), BlockNumber = 2 };

            LessonBlock lb3_1 = new LessonBlock { Id = 7, Teacher = t2, Date = new DateTime(2014,5,28), BlockNumber = 1 };
            LessonBlock lb3_2 = new LessonBlock { Id = 8, Teacher = t2, Date = new DateTime(2014,5,28), BlockNumber = 2 };
            LessonBlock lb3_3 = new LessonBlock { Id = 8, Teacher = t2, Date = new DateTime(2014, 5, 28), BlockNumber = 3 };
            
            LessonBlock lb4_1 = new LessonBlock { Id = 9, Teacher = t1, Date = new DateTime(2014,5,29), BlockNumber = 1 };
            LessonBlock lb4_2 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014,5,29), BlockNumber = 2 };
            LessonBlock lb4_3 = new LessonBlock { Id = 9, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 3 };
            LessonBlock lb4_4 = new LessonBlock { Id = 10, Teacher = t2, Date = new DateTime(2014, 5, 29), BlockNumber = 4 };

            LessonBlock lb5_2 = new LessonBlock { Id = 10, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 2 };

            LessonBlock lb6_1 = new LessonBlock { Id = 13, Teacher = t1, Date = new DateTime(2014,6,2), BlockNumber = 1 };
            
            List<LessonBlock> list = new List<LessonBlock> { 
                lb1_1,
                lb2_1, lb2_2, 
                lb3_1, lb3_2, lb3_3,
                lb4_1, lb4_2, lb4_3, lb4_4,
                lb5_2,
                lb6_1
            };

            LessonBlock lb_1_1 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 1 };
            LessonBlock lb_1_4 = new LessonBlock { Id = 1, Teacher = t1, Date = new DateTime(2014, 5, 26), BlockNumber = 4 };

            LessonBlock lb_2_1 = new LessonBlock { Id = 5, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 1 };
            LessonBlock lb_2_2 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 2 };
            LessonBlock lb_2_3 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 27), BlockNumber = 3 };

            LessonBlock lb_3_1 = new LessonBlock { Id = 7, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 1 };
            LessonBlock lb_3_2 = new LessonBlock { Id = 8, Teacher = t1, Date = new DateTime(2014, 5, 28), BlockNumber = 2 };

            LessonBlock lb_4_1 = new LessonBlock { Id = 9, Teacher = t1, Date = new DateTime(2014, 5, 29), BlockNumber = 1 };

            LessonBlock lb_5_1 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 1 };
            LessonBlock lb_5_2 = new LessonBlock { Id = 6, Teacher = t1, Date = new DateTime(2014, 5, 30), BlockNumber = 2 };

            LessonBlock lb_6_1 = new LessonBlock { Id = 13, Teacher = t1, Date = new DateTime(2014, 6, 2), BlockNumber = 1 };

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

            Service.Service service = new Service.Service();

            List<LessonBlock> blocks = new List<LessonBlock> { lb0_1, lb0_2 };
            List<DateTime> availableBlocks = service.setLessonBehindOwnLesson(mainScheme, listConflict, blocks, new DateTime(2014,5,26));

            Assert.AreEqual(new DateTime(2014, 5, 26), availableBlocks[0]);
            Assert.AreEqual(new DateTime(2014, 5, 30), availableBlocks[1]);
            Assert.AreEqual(new DateTime(2014, 6, 2), availableBlocks[2]);

            blocks = new List<LessonBlock> { lb0_1, lb0_2, lb0_3 };
            availableBlocks = service.setLessonBehindOwnLesson(mainScheme, listConflict, blocks, new DateTime(2014,5,26));

            Assert.AreEqual(new DateTime(2014, 6, 2), availableBlocks[0]);
        }
        
    }
}
