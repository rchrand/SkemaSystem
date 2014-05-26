using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{

    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("optionalSubjects")]
    [Route("{action=index}/{id?}")]
    public class OptionalSubjectsController : BaseController
    {

        [Route("")]
        public ActionResult Index()
        {
            return View(db.Schemes.Where(x => x.ClassModel == null)); // Show a list of schemes without classes => that means it is optional subjects! (valgfag in danish)
        }

        public ActionResult Create()
        {
            HashSet<string> years = new HashSet<string>();
            foreach (Scheme scheme in db.Schemes)
            {
                years.Add(scheme.YearString);
            }
            ViewBag.Years = years;

            ViewBag.Education = (from e in db.Educations
                                 where e.Name.Equals("DMU")
                                 select e).SingleOrDefault();

            ViewBag.Subjects = from s in db.Subjects
                               where s.OptionalSubject
                               select s;

            return View();
        }

        [HttpPost]
        public ActionResult Create(string stuff)
        {
            return null;
        }

        public ActionResult UpdateConflictsWith(string year, string semester)
        {
            int semesterId = Int32.Parse(semester);
            var schemes = from s in db.Schemes
                          where s.Semester.Id == semesterId && s.YearString.Equals(year)
                          select s;

            return PartialView("_ConflictSchemes", schemes);
        }

        public ActionResult CreateOptionalSubject(string name)
        {

            Subject su = new Subject { Name = name, OptionalSubject = true, Education = db.Educations.Where(x => x.Name.Equals("DMU")).SingleOrDefault() };
            db.Subjects.Add(su);
            db.SaveChanges();

            ViewBag.Subjects = from s in db.Subjects
                               where s.OptionalSubject
                               select s;

            return PartialView("_OptionalSubjectsList");
        }
	}
}