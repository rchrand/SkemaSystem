using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SkemaSystem.Controllers
{
    [RouteArea("Admin", AreaPrefix="admin")]
    public class AccountController : BaseController
    {
        [Route("{education?}")]
        public ActionResult Index(string education)
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(Teacher model)
        {
            if (model.IsValid(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                Teacher teacher = db.Teachers.SingleOrDefault(t => t.Username.Equals(model.Username));
                Session["UserRole"] = teacher.Role;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Login is invalid");
            }

            return View(model);
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}