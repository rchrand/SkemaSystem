using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{
    public class BaseController : Controller
    {
        SkeamSystemDb _db = new SkeamSystemDb();
        public IEnumerable<Education> _educationModel;

        public BaseController()
        {
            _educationModel = _db.Educations.ToList();
             ViewBag.EducationModel = _educationModel;
        }
	}
}