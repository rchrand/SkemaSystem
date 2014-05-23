using SkemaSystem.Models;
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

            return View(db.Schemes.FirstOrDefault());
        }


        public ActionResult ChangeSubjectDropDown(int scheme)
        {
            return PartialView("_SubjectDropDown", db.Schemes.Single(x => x.Id == scheme));
        }
	}
}