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

            return View();
        }

        [HttpPost]
        public ActionResult Create(string stuff)
        {
            return null;
        }
	} 
}