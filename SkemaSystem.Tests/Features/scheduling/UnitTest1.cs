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
        //[TestMethod]
        //public void dimensionsIsCreated()
        //{
        //    Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 27), Testdata());

        //    Assert.AreEqual(2, dic.Keys.Count);
        //    Assert.AreEqual(5, dic[0].Count);
        //}

        //[TestMethod]
        //public void noLessonBlocksFound()
        //{
        //    Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 14), Testdata());

        //    Assert.IsNotNull(dic);
        //    Assert.AreEqual(false, dic.ContainsKey(0));
        //}

        //[TestMethod]
        //public void noLessonsInBetweenOtherLesson()
        //{
        //    Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 28), Testdata());

        //    Assert.IsNull(dic[1][1]);
        //    Assert.IsNotNull(dic[0][1]);
        //}

        ///*
        // * this test is assuring that when a date which is 
        // * not a monday is passed as a parameter, the schedulingservice
        // * still gets blocks for the dates whole week.
        // * */

        //[TestMethod]
        //public void AllwaysStartingAtMonday()
        //{
        //    //Date is not a monday
        //    Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 28), Testdata());

        //    //assert there is something on monday
        //    Assert.IsNotNull(dic[0][0]);
        //}

        //[TestMethod]
        //public void ShowNextWeekAfterFridays()
        //{
        //    Dictionary<int, List<LessonBlock>> dic = SchedulingService.buildScheme(new DateTime(2014, 5, 31), Testdata());

        //    Assert.AreEqual(false, dic.ContainsKey(0));

        //    dic = SchedulingService.buildScheme(new DateTime(2014, 5, 30), Testdata());

        //    Assert.IsNotNull(dic[0]);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void FindConflictsOnDateAndBlockNumber()
        //{
        //    SubjectDistBlock sdb = new SubjectDistBlock()
        //    {
        //        Subject = new Subject() { Name = "SD" },
        //        Teacher = new Teacher() { Name = "testHanne" }
        //    };

        //    //If theres a conflict on date and blocknumber on the same scheme.
        //    bool conflict = SchedulingService.IsConflicting(TestOtherSchemes()[0], new LessonBlock()
        //    {
        //        BlockNumber = 0,
        //        Date = new DateTime(2014, 5, 26),
        //        Room = new Room() { Id = -1, RoomName = "temproom"},
        //        /*Subject = new Subject() { Name = "SD" },
        //        Teacher = new Teacher() { Name = "testHanne"}*/
        //        Subject = sdb
        //    }, TestRooms(), TestOtherSchemes());

        //    Assert.IsTrue(conflict);

        //    conflict = SchedulingService.IsConflicting(TestOtherSchemes()[0], new LessonBlock()
        //    {
        //        BlockNumber = 3,
        //        Date = new DateTime(2014, 5, 26),
        //        Room = new Room() { Id = -1, RoomName = "temproom"},
        //        /*Subject = new Subject() { Name = "SD" },
        //        Teacher = new Teacher() { Name = "testHanne" }*/
        //        Subject = sdb
        //    }, TestRooms(), TestOtherSchemes());

        //    //If teacher is available at the same time.


        //    //If the room is available at the same time.

        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void FindConflictOnTeacher()
        //{
        //    SubjectDistBlock sdb = new SubjectDistBlock()
        //    {
        //        Subject = new Subject() { Name = "SD" },
        //        Teacher = TestTeacher()[0]
        //    };

        //    bool conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
        //    {
        //        BlockNumber = 1,
        //        Date = new DateTime(2014, 6, 2),
        //        Room = TestRooms()[0],
        //        /*Subject = new Subject() { Name = "SD" },
        //        Teacher = TestTeacher()[0]*/
        //        Subject = sdb
        //    }, TestRooms(), TestOtherSchemes());

        //    Assert.IsTrue(conflict);
            
        //    conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
        //    {
        //        BlockNumber = 3,
        //        Date = new DateTime(2014, 5, 26),
        //        Room = TestRooms()[0],
        //        /*Subject = new Subject() { Name = "SD" },
        //        Teacher = TestTeacher()[0]*/
        //        Subject = sdb
        //    }, TestRooms(), TestOtherSchemes());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void FindConflictOnRoom()
        //{
        //    SubjectDistBlock sdb = new SubjectDistBlock()
        //    {
        //        Subject = new Subject() { Name = "SD" },
        //        Teacher = new Teacher() { Id = -1, Name = "Dummy" }
        //    };

        //    bool conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
        //    {
        //        BlockNumber = 1,
        //        Date = new DateTime(2014, 6, 4),
        //        Room = TestRooms()[1],
        //        /*Subject = new Subject() { Name = "SD" },
        //        Teacher = new Teacher() { Id = -1, Name = "Dummy" }*/
        //        Subject = sdb
        //    }, TestRooms(), TestOtherSchemes());

        //    Assert.IsTrue(conflict); 
            
        //    conflict = SchedulingService.IsConflicting(Testdata(), new LessonBlock()
        //    {
        //        BlockNumber = 3,
        //        Date = new DateTime(2014, 5, 26),
        //        Room = TestRooms()[0],
        //        /*Subject = new Subject() { Name = "SD" },
        //        Teacher = new Teacher() { Id = -1, Name = "Dummy" }*/
        //        Subject = sdb
        //    }, TestRooms(), TestOtherSchemes());
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void CreateLessonBlockWithNoComplications()
        //{
        //    List<Scheme> schemes = TestOtherSchemes();
        //    List<Room> rooms = TestRooms();

        //    LessonBlock lessonBlock = SchedulingService.ScheduleLesson(
        //        schemes[0].Id,
        //        schemes[0].SubjectDistBlocks[0].Subject.Id,
        //        rooms[0].Id,
        //        new DateTime(2014, 6, 23),
        //        0,
        //        schemes,
        //        rooms);

        //    Assert.IsNotNull(lessonBlock);

        //    // fails, throw exception
        //    lessonBlock = SchedulingService.ScheduleLesson(
        //        schemes[0].Id,
        //        schemes[0].SubjectDistBlocks[0].Subject.Id,
        //        rooms[0].Id,
        //        new DateTime(2014, 6, 20),
        //        2,
        //        schemes,
        //        rooms);
        //}

        //[TestMethod]
        //public void DeleteSeveralLessonBlocks()
        //{
        //    List<Scheme> schemes = TestOtherSchemes();

        //    Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

        //    string lessonIds = "1,2,10,11"; // 11 doesn't exists, but shouldn't conflict

        //    bool result = SchedulingService.DeleteLessons(schemes[0].Id, lessonIds, schemes);

        //    Assert.IsTrue(result);

        //    Assert.AreEqual(7, schemes[0].LessonBlocks.Count);
        //}

        //[TestMethod]
        //public void RelocateSeveralLessonBlocks()
        //{
        //    List<Scheme> schemes = TestOtherSchemes();
        //    List<Room> rooms = TestRooms();

        //    string lessonIds = "1,2,10,11"; // 11 doesn't exists, but shouldn't conflict

        //    bool result = SchedulingService.RelocateLesson(schemes[0].Id, lessonIds, rooms[2].Id, schemes, rooms);

        //    Assert.IsTrue(result);

        //    // this lesson can't be relocated to room[1], because its in use
        //    lessonIds = "1";

        //    result = SchedulingService.RelocateLesson(schemes[0].Id, lessonIds, rooms[1].Id, schemes, rooms);

        //    Assert.IsFalse(result);
        //}


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
        [ExpectedException(typeof(Exception))]
        public void US11_1_1_FindConflictsOnDateAndBlockNumber()
        {
            List<Scheme> schemes = TestOtherSchemes();

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(1, 1, 1, new DateTime(2014, 5, 26), 0, schemes, TestRooms());

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        public void US11_1_2_NoConflictsOnDateAndBlockNumber()
        {
            List<Scheme> schemes = TestOtherSchemes();

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(1, 1, 1, new DateTime(2014, 5, 26), 4, schemes, TestRooms());

            Assert.AreEqual(11, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void US11_2_1_FindConflictOnTeacher()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();
            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(2, 1, 2, new DateTime(2014, 6, 2), 1, schemes, rooms);

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        public void US11_2_2_FindNoConflictOnTeacher()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();
            Assert.AreEqual(11, schemes[1].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(2, 1, 2, new DateTime(2014, 5, 26), 3, schemes, rooms);

            Assert.AreEqual(12, schemes[1].LessonBlocks.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void US11_3_1_FindConflictOnRoom()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();
            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(1, 1, 2, new DateTime(2014, 6, 4), 1, schemes, rooms);

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        public void US11_3_2_FindNoConflictsOnRoom()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();
            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(1, 1, 1, new DateTime(2014, 6, 4), 3, schemes, rooms);

            Assert.AreEqual(11, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        public void US11_5_1_DeleteSeveralLessonBlocks()
        {
            List<Scheme> schemes = TestOtherSchemes();

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            string lessonIds = "1,2,10,11"; // 11 doesn't exists, but shouldn't conflict

            SchedulingService.DeleteLessons(schemes[0].Id, lessonIds, schemes);

            Assert.AreEqual(7, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        public void US11_6_1_RelocateSeveralLessonBlocksWithComplications()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();

            string lessonIds = "1,2,11,12"; // 11 doesn't exists, but shouldn't conflict
            // 2, should conflict.


            SchedulingService.RelocateLesson(schemes[1].Id, lessonIds, rooms[0].Id, schemes, rooms);

            LessonBlock lesson1 = schemes[1].LessonBlocks.Find(l => l.Id == 1);
            LessonBlock lesson2 = schemes[1].LessonBlocks.Find(l => l.Id == 2);
            LessonBlock lesson11 = schemes[1].LessonBlocks.Find(l => l.Id == 11);
            LessonBlock lesson12 = schemes[1].LessonBlocks.Find(l => l.Id == 12);

            Assert.AreEqual("A2.13", lesson1.Room.RoomName);
            Assert.AreEqual("A2.13", lesson2.Room.RoomName);
            Assert.AreEqual("A2.13", lesson11.Room.RoomName);
        }

        [TestMethod]
        public void US11_6_2_RelocateSeveralLessonBlocksWithoutComplications()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();

            string lessonIds = "1,3,11,12"; // 11 doesn't exists, but shouldn't conflict

            SchedulingService.RelocateLesson(schemes[1].Id, lessonIds, rooms[0].Id, schemes, rooms);

            LessonBlock lesson1 = schemes[1].LessonBlocks.Find(l => l.Id == 1);
            LessonBlock lesson3 = schemes[1].LessonBlocks.Find(l => l.Id == 3);
            LessonBlock lesson11 = schemes[1].LessonBlocks.Find(l => l.Id == 11);
            LessonBlock lesson12 = schemes[1].LessonBlocks.Find(l => l.Id == 12);

            Assert.AreEqual("A1.12", lesson1.Room.RoomName);
            Assert.AreEqual("A1.12", lesson3.Room.RoomName);
            Assert.AreEqual("A1.12", lesson11.Room.RoomName);
        }

        [TestMethod]
        public void US11_INT_1_IsSchemeAssociatedWithNewLessonBlock()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);

            SchedulingService.ScheduleLesson(1, 1, 1, new DateTime(2014, 6, 10), 3, schemes, rooms);

            Assert.AreEqual(11, schemes[0].LessonBlocks.Count);
            Assert.AreEqual("2014-06-10", schemes[0].LessonBlocks[10].Date.ToString("yyyy-MM-dd"));
        }



        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void US11_4_1_FindConflictOnOptionalSchedule()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();
            SubjectDistBlock sdb1 = new SubjectDistBlock() { Teacher = TestTeacher()[0], Subject = schemes[0].SubjectDistBlocks[0].Subject };

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);
            Services.SchedulingService.ScheduleLesson(schemes[0].Id, schemes[0].SubjectDistBlocks[0].Subject.Id, rooms[0].Id, new DateTime(2014, 6, 11), 1, schemes, rooms);
            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);
        }

        [TestMethod]
        public void US11_4_2_FindNoConflictOnOptionalSchedule()
        {
            List<Scheme> schemes = TestOtherSchemes();
            List<Room> rooms = TestRooms();
            SubjectDistBlock sdb1 = new SubjectDistBlock() { Teacher = TestTeacher()[0], Subject = schemes[0].SubjectDistBlocks[0].Subject };

            Assert.AreEqual(10, schemes[0].LessonBlocks.Count);
            Services.SchedulingService.ScheduleLesson(schemes[0].Id, schemes[0].SubjectDistBlocks[0].Subject.Id, rooms[0].Id, new DateTime(2014, 6, 12), 1, schemes, rooms);
            Assert.AreEqual(11, schemes[0].LessonBlocks.Count);
        }

        private static Scheme Testdata()
        {
            Subject s = new Subject() { Name = "SD" };
            SubjectDistBlock sdb1 = new SubjectDistBlock() { Teacher = TestTeacher()[0], Subject = s, Id = 1 };

            return new Scheme()
            {
                ConflictSchemes = new List<Scheme>(),
                Id = 1,
                ClassModel = new ClassModel(),
                LessonBlocks = new List<LessonBlock>() { 
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 26),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 26),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 27),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 28),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 28),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 29),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 29),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 0,
                        Date = new DateTime(2014, 5, 30),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 1,
                        Date = new DateTime(2014, 5, 30),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                    new LessonBlock(){ 
                        BlockNumber = 2,
                        Date = new DateTime(2014, 6, 20),
                        Room = TestRooms()[0],
                        Subject = sdb1
                    },
                },
                Semester = null,
                SubjectDistBlocks = new List<SubjectDistBlock>() { sdb1 },
            };
        }

        private static List<Scheme> TestOtherSchemes()
        {
            Subject s = new Subject() { Name = "SD", Id = 1 };
            SubjectDistBlock sdb1 = new SubjectDistBlock() { Teacher = TestTeacher()[0], Subject = s, Id = 1 };
            SubjectDistBlock sdb2 = new SubjectDistBlock() { Teacher = TestTeacher()[1], Subject = s, Id = 2 };

            return new List<Scheme>()
            {
                new Scheme()
                {
                    ConflictSchemes = new List<Scheme>() { 
                        new Scheme() { 
                            Id = 5, 
                            ClassModel = null, 
                            Name = "Valgfag", 
                            LessonBlocks = new List<LessonBlock>() { new LessonBlock() { 
                                Id = 20, 
                                BlockNumber=1, 
                                Date = new DateTime(2014,6,11),
                                Room = TestRooms()[0], 
                                Subject = sdb1} 
                            } 
                        } 
                    },
                    Id = 1,
                    ClassModel = new ClassModel(),
                    LessonBlocks = new List<LessonBlock>() { 
                        new LessonBlock(){
                            Id = 1,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 26),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock() {
                            Id = 2,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 26),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 3,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 27),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 4,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 28),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 5,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 28),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 6,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 29),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 7,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 29),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 8,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 30),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 9,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 5, 30),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                        new LessonBlock(){ 
                            Id = 10,
                            BlockNumber = 2,
                            Date = new DateTime(2014, 6, 20),
                            Room = TestRooms()[0],
                            Subject = sdb1
                        },
                    },
                    Semester = null,
                    SubjectDistBlocks = new List<SubjectDistBlock>() {
                        sdb1
                    },
                },
                new Scheme()
                {
                    ConflictSchemes = new List<Scheme>(),
                    Id = 2,
                    ClassModel = new ClassModel() {
                        ClassName = "12t fake"
                    },
                    Semester = null,
                    SubjectDistBlocks = new List<SubjectDistBlock>() { sdb1, sdb2 },
                    LessonBlocks = new List<LessonBlock>() { 
                        new LessonBlock()
                        {
                            Id = 1,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 2),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 2,
                            BlockNumber = 2,
                            Date = new DateTime(2014, 6, 20),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 3,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 3),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 4,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 3),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 5,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 4),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 6,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 4),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 7,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 6, 5),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 8,
                            BlockNumber = 1,
                            Date = new DateTime(2014, 6, 5),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 9,
                            BlockNumber = 2,
                            Date = new DateTime(2014, 5, 28),
                            Room = TestRooms()[1],
                            Subject = sdb1
                        },
                        new LessonBlock()
                        {
                            Id = 10,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 26),
                            Room = TestRooms()[1],
                            Subject = sdb2
                        }
                        ,
                        new LessonBlock()
                        {
                            Id = 11,
                            BlockNumber = 0,
                            Date = new DateTime(2014, 5, 6),
                            Room = TestRooms()[1],
                            Subject = sdb2
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
                new Room(){ Id = 2, RoomName = "A2.13" },
                new Room(){ Id = 3, RoomName = "A2.14" }
            };
        }
    }
}
