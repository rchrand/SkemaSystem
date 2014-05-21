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
        public SkeamSystemDb db { get; set; }
        public IEnumerable<Education> _educationModel;

        public BaseController()
        {
            db = new SkeamSystemDb();
            if (ConfigurationManager.ConnectionStrings["skeamsysdb"] != null)
            {
                _educationModel = db.Educations.ToList();
                ViewBag.EducationModel = _educationModel;
            }
        }
	}
}