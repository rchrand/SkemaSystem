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
    public class HomeController : BaseController
    {
        ISkemaSystemDb _db;
        
        public HomeController()
        {
            _db = new SkeamSystemDb();
        }

        public HomeController(FakeSkemaSystemDb db)
        {
            _db = db;
        }

        public ActionResult Index(string education)
        {
            if (education != null)
            {
                //RedirectToAction("Index", "Education");
            }
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