using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;
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

            OptionalSubjectViewModel os = new OptionalSubjectViewModel();

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
        public ActionResult Create(string semesterId, string[] subject, string[] blockcount, string[] classModel)
        {
            Debug.WriteLine(semesterId + "GAGAGAG");
            int smid = Int32.Parse(semesterId);

            int[] blockArray = ConvertStringArraytoInt(blockcount);

            int[] classArray = ConvertStringArraytoInt(classModel);

            var optionalSubjects = (from su in db.Subjects
                                where su.OptionalSubject
                                select su);

            var semester = (from e in db.Semesters
                           where e.Id == smid
                           select e).SingleOrDefault();

            var classes = (from e in db.Classes
                           select e);

            List<Subject> conflictSubjects = new List<Subject>();
            List<ClassModel> conflictClasses = new List<ClassModel>();

            // Skal nok omskrives :)
            foreach (var item in optionalSubjects)
            {
                for (int i = 0; i < subject.Length; i++)
                {
                    if (item.Name.Equals(subject[i]))
                    {
                        conflictSubjects.Add(item);
                        semester.Blocks.Add(new SemesterSubjectBlock { Subject = item, BlocksCount = blockArray[i] });
                        item.conflictSubjects.Add(optionalSubjects.First(x => x.Name.Equals(subject[i])));
                    }

                }
            }

            foreach (var item in classes)
            {
                for (int i = 0; i < subject.Length; i++)
                {
                    if (item.Id.Equals(classArray[i]))
                    {
                        item.ActiveSchemes.Last().ConflictClasses.Add(item);
                    }
                }
            }


                return null;
        }

        private int[] ConvertStringArraytoInt(string[] item)
        {
            int[] result = new int[item.Length];
            for (int i = 0; i < item.Length; i++)
            {
                result[i] = Int32.Parse(item[i]);
            }
            return result;
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