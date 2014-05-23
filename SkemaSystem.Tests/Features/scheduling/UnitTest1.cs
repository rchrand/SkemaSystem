using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using System.Collections.Generic;
using SkemaSystem.Services;
using SkemaSystem.Models.ViewModels;

namespace SkemaSystem.Tests.Features.scheduling
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void dimensionsIsCreated()
        {
            Dictionary<int, List<TableCellViewModel>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 27), Testdata());

            Assert.AreEqual(2, dic.Keys.Count);
            Assert.AreEqual(5, dic[0].Count);
        }

        [TestMethod]
        public void noLessonBlocksFound()
        {
            Dictionary<int, List<TableCellViewModel>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 14), Testdata());

            Assert.IsNotNull(dic);
            Assert.AreEqual(false,dic.ContainsKey(0));
        }

        [TestMethod]
        public void noLessonsInBetweenOtherLesson()
        {
            Dictionary<int, List<TableCellViewModel>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 28), Testdata());

            Assert.IsNull(dic[1][1]);
            Assert.IsNotNull(dic[0][1]);
        }

        /*
         * this test is assuring that when a date which is 
         * not a monday is passed as a parameter, the schedulingservice
         * still gets blocks for the dates whole week.
         * */

        [TestMethod]
        public void AllwaysStartingAtMonday()
        {
            //Date is not a monday
            Dictionary<int, List<TableCellViewModel>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 28), Testdata());

            //assert there is something on monday
            Assert.IsNotNull(dic[0][0]);
        }

        [TestMethod]
        public void ShowNextWeekAfterFridays()
        {
            Dictionary<int, List<TableCellViewModel>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 31), Testdata());

            Assert.AreEqual(false, dic.ContainsKey(0));

            dic = SchedulingService.buildScheme(new DateTime(2014, 5, 30), Testdata());

            Assert.IsNotNull(dic[0]);
        }

        private static Scheme Testdata()
        {
            return new Scheme()
            {
                Id = -1,
                ClassModel = null,
                LessonBlocks = new List<LessonBlock>() { 
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 26),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 26),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 27),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 28),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 28),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 29),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 29),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 30),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 30),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                    new LessonBlock(){ 
                        BlockNumber = 2,
                        Date = new DateTime(2014, 6, 20),
                        Room = new Room(){ RoomName = "A.1.12"},
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = new Teacher(){ Name = "Hanne"}
                    },
                },
                Semester = null,
                SubjectDistBlocks = null,
            };
        }
    }
}
