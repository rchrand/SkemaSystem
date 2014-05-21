using SkemaSystem.Models;
using System;
using System.Collections.Generic;
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
            var db = new SkeamSystemDb();

            IEnumerable<SelectListItem> educations = from s in db.Educations
                                                    select new SelectListItem { Text = s.Name, Value = s.Id };

            ViewBag._educations = educations;
            return View();
        }
	}
}