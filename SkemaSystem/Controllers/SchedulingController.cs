using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;
using SkemaSystem.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Transactions;
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

            ViewBag.SubjectDistBlocks = new List<SubjectDistBlock>();

            return View();
        }


        public ActionResult ChangeSubjectDropDown(int scheme)
        {
            ViewBag.SubjectDistBlocks = db.Schemes.Single(x => x.Id == scheme).SubjectDistBlocks;

            return PartialView("_SubjectDropDown");
        }

        [Route("lesson"), HttpPost]
        public ActionResult ScheduleLesson(int schemeId, int subjectId, int roomId, DateTime date, int blockNumber)
        {
            LessonBlock lesson;

            TransactionOptions options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.Serializable,
                Timeout = TransactionManager.DefaultTimeout
            };

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                /*Scheme scheme = db.Schemes.Single(s => s.Id.Equals(schemeId));

                Room room = db.Rooms.Single(r => r.Id.Equals(roomId));

                SubjectDistBlock sdb = scheme.SubjectDistBlocks.Single(s => s.Id.Equals(subjectId));

                Subject subject = sdb.Subject;

                Teacher teacher = sdb.Teacher;

                lesson = new LessonBlock()
                {
                    BlockNumber = blockNumber,
                    Date = date,
                    Room = room,
                    Subject = subject,
                    Teacher = teacher
                };

                try
                {
                    bool conflicting = SchedulingService.IsConflicting(scheme, lesson, db.Rooms, db.Schemes);
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    return Json(new { message = e.Message });
                }

                scheme.LessonBlocks.Add(lesson);

                db.SaveChanges();
                scope.Complete();*/

                try
                {
                    lesson = SchedulingService.ScheduleLesson(schemeId, subjectId, roomId, date, blockNumber, db.Schemes, db.Rooms);
                    
                    // if no exception thrown
                    db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception e)
                {
                    scope.Dispose();
                    return Json(new { message = e.Message });
                }
            }
            return PartialView("_LessonBlockPartial", lesson);
        }

        [Route("lesson/delete"), HttpPost]
        public ActionResult DeleteLessons(int schemeId, string lessonIds)
        {
            // TODO check permissions

            TransactionOptions options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead,
                Timeout = TransactionManager.DefaultTimeout
            };

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                /*Scheme scheme = db.Schemes.Single(s => s.Id.Equals(schemeId));

                string[] ids = lessonIds.Split(',');

                scheme.LessonBlocks.RemoveAll(l => ids.Contains(l.Id.ToString()));

                db.SaveChanges();
                scope.Complete();*/

                if (SchedulingService.DeleteLessons(schemeId, lessonIds, db.Schemes))
                {
                    db.SaveChanges();
                    scope.Complete();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Route("lesson/relocate"), HttpPost]
        public ActionResult RelocateLesson(int schemeId, string lessonIds, int roomId)
        {
            //// TODO check permissions

            //TransactionOptions options = new TransactionOptions
            //{
            //    IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead,
            //    Timeout = TransactionManager.DefaultTimeout
            //};

            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            //{
            //    Scheme scheme = db.Schemes.Single(s => s.Id.Equals(schemeId));

            //    string[] ids = lessonIds.Split(',');

            //    IEnumerable<LessonBlock> blocks = scheme.LessonBlocks.Where(l => ids.Contains(l.Id.ToString()));

            //    Room room = db.Rooms.Single(r => r.Id.Equals(roomId));

            //    foreach (var block in blocks)
            //    {
            //        block.Room = room;

            //        if (!SchedulingService.IsRoomAvailable(db.Schemes, block, scheme))
            //        {
            //            scope.Dispose();
            //            return Json(new { message = "Lokalet er ikke ledigt på det pågældende tidspunkt." });
            //        }
            //    }

            //    db.SaveChanges();
            //    scope.Complete();
            //}
            // TODO check permissions

            TransactionOptions options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead,
                Timeout = TransactionManager.DefaultTimeout
            };

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                if (SchedulingService.RelocateLesson(schemeId, lessonIds, roomId, db.Schemes, db.Rooms))
                {
                    db.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    scope.Dispose();
                    return Json(new { message = "Lokalet er ikke ledigt på det pågældende tidspunkt." });
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult ChangeScheme(int scheme)
        {
            Scheme _scheme = db.Schemes.Single(x => x.Id == scheme);
            
            DateTime startDate = new DateTime(2014, 5, 25);

            Scheme data = _scheme;

            TableViewModel tvm = new TableViewModel() { ClassName = _scheme.ClassModel.ClassName, StartDate = SchedulingService.CalculateStartDate(startDate), TableCells = SchedulingService.buildScheme(startDate, data) };

            SchemeViewModel model = new SchemeViewModel();
            model.Classname = _scheme.ClassModel.ClassName;
            model.Schemes.Add(tvm);
            if (scheme == 2)
            {
                model.Schemes.Add(tvm);
            }
            return PartialView("_SchemePartial", model);
        }
	}
}