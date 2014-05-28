using SkemaSystem.Models;
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
            if (education != null)
            {
                //RedirectToAction("Index", "Education");
            }
            var model =
                from t in db.Teachers
                orderby t.Name ascending
                select t;

            return View(model);
        }
    }
}