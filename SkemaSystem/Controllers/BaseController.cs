using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{
    public class BaseController : Controller
    {
        SkeamSystemDb _db;
        public IEnumerable<Education> _educationModel;

        public BaseController()
        {
            _db = new SkeamSystemDb();
            if (ConfigurationManager.ConnectionStrings["skeamsysdb"] != null)
            {
                _educationModel = _db.Educations.ToList();
                ViewBag.EducationModel = _educationModel;
            }
        }
	}
}