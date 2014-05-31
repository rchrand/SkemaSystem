using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;
using SkemaSystem.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{
    [RouteArea("Default", AreaPrefix="")]
    public class HomeController : BaseController
    {
        [Route("{education?}")]
        public ActionResult Index(string education)
        {
            if (education == null)
                return RedirectToAction("Index", new { education = db.Educations.FirstOrDefault().Name });

            Education edu = db.Educations.SingleOrDefault(x => x.Name.Equals(education));

            // Creating af grouped collection of schemes! Grouped by year - ordered by a bunch of things!
            var schemeGroupsQuery = from s in edu.Schemes
                                    where s.ClassModel != null && (s.SemesterStart - DateTime.Now).TotalDays < 30
                                    orderby s.SemesterStart descending, s.Semester.Number descending, s.ClassModel.ClassName ascending
                                    group s by s.YearString into g
                                    select new { Year = g.Key, Schemes = g };

            Dictionary<string, List<Scheme>> schemeGrouped = new Dictionary<string, List<Scheme>>();

            foreach (var g in schemeGroupsQuery)
            {
                List<Scheme> tempList = new List<Scheme>();
                foreach (var n in g.Schemes)
                {
                    tempList.Add(n);
                }

                string year = (g.Year.Contains("F")) ? g.Year.Replace("F", "Forår ") : g.Year.Replace("E", "Efterår ");
                schemeGrouped.Add(year, tempList);
            }
            ViewBag.schemeGroups = schemeGrouped;

            Scheme scheme = edu.Schemes.FirstOrDefault();

            return View();
        }

        [Route("{education?}/ChangeScheme")]
        public ActionResult ChangeScheme(int schemeId)
        {
            Scheme scheme = db.Schemes.Single(x => x.Id == schemeId);

            SchemeViewModel model = new SchemeViewModel();
            if (scheme != null) {
                ICollection<Dictionary<int, List<LessonBlock>>> tableCellsList = SchedulingService.AllSchemes(scheme);

                DateTime currentWeekStartDate = SchedulingService.CalculateStartDate(scheme.SemesterStart);
                foreach (Dictionary<int, List<LessonBlock>> tableCells in tableCellsList)
                {
                    TableViewModel tvm = new TableViewModel() { StartDate = currentWeekStartDate, TableCells = tableCells };
                    model.Schemes.Add(tvm);
                    currentWeekStartDate = currentWeekStartDate.AddDays(7);
                }

                model.Classname = scheme.ClassModel.ClassName;
                model.SemesterNumber = scheme.Semester.Number;
                model.Year = (scheme.YearString.Contains("F")) ? scheme.YearString.Replace("F", "Forår ") : scheme.YearString.Replace("E", "Efterår ");
            }
            return PartialView("_SchemePartial", model);
        }

        [Route("{education?}/ChangeOptionalSubjects")]
        public ActionResult ChangeOptionalSubjects(int schemeId, string education)
        {
            Education edu = db.Educations.SingleOrDefault(x => x.Name.Equals(education));

            Scheme scheme = edu.Schemes.Where(x => x.Id == schemeId).FirstOrDefault();

            ViewBag.schemeId = scheme.Id;

            return PartialView("_OptionalSubjects", scheme.ConflictSchemes);
        }

        [Route("{education?}/LockAndUnlockCheckboxes")]
        public ActionResult LockAndUnlockCheckboxes(int[] includeOptionalSubject, int schemeId)
        {
            Scheme scheme = db.Schemes.Where(x => x.Id == schemeId).FirstOrDefault();

            HashSet<int> addedIds = new HashSet<int>();

            if (includeOptionalSubject != null) { 
                foreach (Scheme sc in scheme.ConflictSchemes)
                    addedIds.Add(sc.Id);

                foreach (int id in includeOptionalSubject) {
                    Scheme s1 = db.Schemes.Where(x => x.Id == id).FirstOrDefault();
                    addedIds.Remove(s1.Id);
                }
            }


            return Json(new { success = true, deactivate_schemes = addedIds.ToArray()});
        }

        [Route("{education?}/MergeWithOptionalSubjects")]
        public ActionResult MergeWithOptionalSubjects(int[] includeOptionalSubject, int schemeId)
        {
            Scheme scheme = db.Schemes.Where(x => x.Id == schemeId).FirstOrDefault();

            // Get the stuff and update the scheme!

            SchemeViewModel model = new SchemeViewModel();
            if (scheme != null)
            {
                var schemesQuery = (from s in db.Schemes
                           where includeOptionalSubject.Contains(s.Id)
                           select s);

                List<Scheme> schemes = new List<Scheme>();
                try
                {
                    schemes = schemesQuery.ToList();
                }
                catch (Exception e) {}

                schemes.Add(scheme);

                ICollection<Dictionary<int, List<LessonBlock>>> tableCellsList = SchedulingService.AllMergedSchemes(schemes);

                DateTime currentWeekStartDate = SchedulingService.CalculateStartDate(scheme.SemesterStart);
                foreach (Dictionary<int, List<LessonBlock>> tableCells in tableCellsList)
                {
                    TableViewModel tvm = new TableViewModel() { StartDate = currentWeekStartDate, TableCells = tableCells };
                    model.Schemes.Add(tvm);
                    currentWeekStartDate = currentWeekStartDate.AddDays(7);
                }

                model.Classname = scheme.ClassModel.ClassName;
                model.SemesterNumber = scheme.Semester.Number;
                model.Year = (scheme.YearString.Contains("F")) ? scheme.YearString.Replace("F", "Forår ") : scheme.YearString.Replace("E", "Efterår ");
            }
            return PartialView("_SchemePartial", model);
        }
    }
}