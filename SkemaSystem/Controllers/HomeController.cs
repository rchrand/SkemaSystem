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

            SchemeViewModel model = new SchemeViewModel();

            Scheme scheme = edu.Schemes.FirstOrDefault();

            ICollection<Dictionary<int, List<LessonBlock>>> tableCellsList = SchedulingService.AllSchemes(scheme);

            DateTime currentWeekStartDate = SchedulingService.CalculateStartDate(scheme.SemesterStart);
            foreach (Dictionary<int, List<LessonBlock>> tableCells in tableCellsList)
            {
                TableViewModel tvm = new TableViewModel() { StartDate = currentWeekStartDate, TableCells = tableCells };
                model.Schemes.Add(tvm);
                currentWeekStartDate = currentWeekStartDate.AddDays(7);
            }

            model.Classname = scheme.ClassModel.ClassName;

            return View(model);
        }
    }
}