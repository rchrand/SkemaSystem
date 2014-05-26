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
            Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 27), Testdata());

            Assert.AreEqual(2, dic.Keys.Count);
            Assert.AreEqual(5, dic[0].Count);
        }

        [TestMethod]
        public void noLessonBlocksFound()
        {
            Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 14), Testdata());

            Assert.IsNotNull(dic);
            Assert.AreEqual(false, dic.ContainsKey(0));
        }

        [TestMethod]
        public void noLessonsInBetweenOtherLesson()
        {
            Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 28), Testdata());

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
            Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 28), Testdata());

            //assert there is something on monday
            Assert.IsNotNull(dic[0][0]);
        }

        [TestMethod]
        public void ShowNextWeekAfterFridays()
        {
            Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 31), Testdata());

            Assert.AreEqual(false, dic.ContainsKey(0));

            dic = SchedulingService.buildScheme(new DateTime(2014, 5, 30), Testdata());

            Assert.IsNotNull(dic[0]);
        }

        [TestMethod]
        public void FindConflictsOnDateAndBlockNumber()
        {
            //If theres a conflict on date and blocknumber on the same scheme.
            bool conflict = SchedulingService.IsConflicting(TestOtherSchemes()[0], new LessonBlock()
            {
                BlockNumber = 0,
                Date = new DateTime(2014, 5, 26),
                Room = new Room() { Id = -1, RoomName = "temproom"},
                Subject = new Subject() { Name = "SD" },
                Teacher = new Teacher() { Name = "testHanne"}
            }, TestRooms(), TestOtherSchemes());

            Assert.IsTrue(conflict);

            conflict = SchedulingService.IsConflicting(TestOtherSchemes()[0], new LessonBlock()
            {
                BlockNumber = 3,
                Date = new DateTime(2014, 5, 26),
                Room = new Room() { Id = -1, RoomName = "temproom"},
                Subject = new Subject() { Name = "SD" },
                Teacher = new Teacher() { Name = "testHanne" }
            }, TestRooms(), TestOtherSchemes());

            Assert.IsFalse(conflict);

            //If teacher is available at the same time.


            //If the room is available at the same time.

        }

        [TestMethod]
        public void FindConflictOnTeacher()
        {
            bool conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
            {
                BlockNumber = 3,
                Date = new DateTime(2014, 5, 26),
                Room = TestRooms()[0],
                Subject = new Subject() { Name = "SD" },
                Teacher = TestTeacher()[0]
            }, TestRooms(), TestOtherSchemes());

            Assert.IsFalse(conflict);

            conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
            {
                BlockNumber = 1,
                Date = new DateTime(2014, 6, 2),
                Room = TestRooms()[0],
                Subject = new Subject() { Name = "SD" },
                Teacher = TestTeacher()[0]
            }, TestRooms(), TestOtherSchemes());

            Assert.IsTrue(conflict);
        }

        [TestMethod]
        public void FindConflictOnRoom()
        {
            bool conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
            {
                BlockNumber = 3,
                Date = new DateTime(2014, 5, 26),
                Room = TestRooms()[0],
                Subject = new Subject() { Name = "SD" },
                Teacher = new Teacher() { Id = -1, Name = "Dummy" }
            }, TestRooms(), TestOtherSchemes());

            Assert.IsFalse(conflict);

            conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
            {
                BlockNumber = 1,
                Date = new DateTime(2014, 6, 4),
                Room = TestRooms()[1],
                Subject = new Subject() { Name = "SD" },
                Teacher = new Teacher() { Id = -1, Name = "Dummy" }
            }, TestRooms(), TestOtherSchemes());

            Assert.IsTrue(conflict);
        }

        private static Scheme Testdata()
        {
            return new Scheme()
            {
                Id = 1,
                ClassModel = null,
                LessonBlocks = new List<LessonBlock>() { 
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 26),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 26),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 27),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 28),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 28),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 29),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 29),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 30),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 30),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                    new LessonBlock(){ 
                        BlockNumber = 2,
                        Date = new DateTime(2014, 6, 20),
                        Room = TestRooms()[0],
                        Subject = new Subject(){ Name = "SD"},
                        Teacher = TestTeacher()[0]
                    },
                },
                Semester = null,
                SubjectDistBlocks = null,
            };
        }

        private static List<Scheme> TestOtherSchemes()
        {
            return new List<Scheme>()
            {
                new Scheme()
                {
                    Id = 1,
                    ClassModel = null,
                    LessonBlocks = new List<LessonBlock>() { 
                        new LessonBlock(){ 
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 26),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 26),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 27),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 28),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 28),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 29),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 29),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 30),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 30),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock(){ 
                            BlockNumber = 2,
                            Date = new DateTime(2014, 6, 20),
                            Room = TestRooms()[0],
                            Subject = new Subject(){ Name = "SD"},
                            Teacher = TestTeacher()[0]
                        },
                    },
                    Semester = null,
                    SubjectDistBlocks = null,
                },
                new Scheme()
                {
                    Id = 2,
                    ClassModel = null,
                    Semester = null,
                    SubjectDistBlocks = null,
                    LessonBlocks = new List<LessonBlock>() { 
                        new LessonBlock()
                        {
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 2),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 2),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 3),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 3),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 4),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 4),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 5),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 5),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        },
                        new LessonBlock()
                        {
                            BlockNumber = 2,
                            Date = new DateTime(2014, 5, 28),
                            Room = TestRooms()[1],
                            Subject = new Subject() { Name = "SD" },
                            Teacher = TestTeacher()[0]
                        }
                    }
                }
            };
        }
        

        private static List<Teacher> TestTeacher()
        {
            return new List<Teacher>() { 
                new Teacher(){ Id = 1, Name = "Hanne"},
                new Teacher(){ Id = 2, Name = "Torben"}
            };
        }

        private static List<Room> TestRooms()
        {
            return new List<Room>()
            {
                new Room(){ Id = 1, RoomName = "A1.12" },
                new Room(){ Id = 2, RoomName = "A2.13" }
            };
        }
    }
}
