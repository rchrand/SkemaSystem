using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;
using SkemaSystem.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("scheduling")]
    [Route("{action=index}")]
    public class SchedulingController : BaseController
    {
        public ActionResult Index()
        {
            IEnumerable<SelectListItem> schemes = from s in db.Schemes
                                                  select new SelectListItem { Text = s.ClassModel.ClassName + " " + SqlFunctions.StringConvert((double)s.Semester.Number).Trim() + ". semester", Value = SqlFunctions.StringConvert((double)s.Id).Trim() };
            ViewBag.schemes = schemes;

            IEnumerable<SelectListItem> educations = from e in db.Educations
                                                     select new SelectListItem { Text = e.Name, Value = SqlFunctions.StringConvert((double)e.Id).Trim() };
            ViewBag.educations = educations;

            IEnumerable<SelectListItem> rooms = from r in db.Rooms
                                                     select new SelectListItem { Text = r.RoomName, Value = SqlFunctions.StringConvert((double)r.Id).Trim() };
            ViewBag.rooms = rooms;

            /*Dictionary<int, List<TableCellViewModel>> dic = new Dictionary<int,List<TableCellViewModel>>();
            dic.Add(0, new List<TableCellViewModel>() { new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { Teacher = db.Teachers.FirstOrDefault(), SubjectName = "SD", Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
            dic.Add(1, new List<TableCellViewModel>() { new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, null, null, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
            dic.Add(2, new List<TableCellViewModel>() { new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { Teacher = db.Teachers.FirstOrDefault(), SubjectName = "SD", Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
            dic.Add(3, new List<TableCellViewModel>() { null, new TableCellViewModel() { Teacher = db.Teachers.FirstOrDefault(), SubjectName = "SD", Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });*/

            DateTime startDate = DateTime.Now;

            TableViewModel tvb = new TableViewModel() { ClassName = "12TFake", StartDate = startDate, Id = 1, TableCells = SchedulingService.buildScheme(startDate, Testdata()) };

            ViewBag.TableViewModel = tvb;

            return View(db.Schemes.FirstOrDefault());
        }


        public ActionResult ChangeSubjectDropDown(int scheme)
        {
            return PartialView("_SubjectDropDown", db.Schemes.Single(x => x.Id == scheme));
        }


        public ActionResult ChangeScheme(int scheme)
        {
            Scheme _scheme = db.Schemes.Single(x => x.Id == scheme);
            
            DateTime startDate = new DateTime(2014, 5, 25);

            Scheme data = scheme == 1 ? Testdata() : Testdata2();

            TableViewModel tvm = new TableViewModel() { ClassName = "12TFake", StartDate = SchedulingService.CalculateStartDate(startDate), Id = 1, TableCells = SchedulingService.buildScheme(startDate, data) };

            SchemeViewModel model = new SchemeViewModel();
            model.Classname = "12TFake";
            model.Schemes.Add(tvm);
            if (scheme == 2)
            {
                model.Schemes.Add(tvm);
            }
            return PartialView("_SchemePartial", model);
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
                },
                Semester = null,
                SubjectDistBlocks = null,
            };
        }

        private static Scheme Testdata2()
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
                },
                Semester = null,
                SubjectDistBlocks = null,
            };
        }
	}
}