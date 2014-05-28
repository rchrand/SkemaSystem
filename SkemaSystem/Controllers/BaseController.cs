using SkemaSystem.Models;
using SkemaSystem.Models.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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

        protected virtual new Teacher User
        {
            get { return HttpContext.User as Teacher; }
        }

        protected bool IsRole(UserRoles role)
        {
            return true;
            /*var user = User.Identity.Name;
            var username = db.Teachers.SingleOrDefault(t => t.Username == user && t.Role == role);

            if (username != null)
            {
                return true;
            }

            return false;*/
        }

        protected bool IsTeacher()
        {
            return IsRole(UserRoles.Teacher) || IsAdmin();
        }

        protected bool IsAdmin()
        {
            return IsRole(UserRoles.Admin) || IsMaster();
        }

        protected bool IsMaster()
        {
            return IsRole(UserRoles.Master);
        }

        protected ActionResult Deny()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }
	}
}