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
    public class HomeController : Controller
    {
        SkeamSystemDb _db = new SkeamSystemDb();

        public ActionResult Index()
        {
            var model =
                from t in _db.Teachers
                orderby t.Name ascending
                select t;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a quite awesome about page!";

            return View();
        }
    }
}