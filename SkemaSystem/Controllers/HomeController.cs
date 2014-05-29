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
                                    where s.ClassModel != null
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
            }
            return PartialView("_SchemePartial", model);
        }
    }
}