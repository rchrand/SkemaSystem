using SkemaSystem.Models;
using SkemaSystem.Models.Enum;
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
        /*
         * ONLY FOR DEMO PURPOSE
         */

        [Route("")]
        public ActionResult Demo()
        {
            return View();// Content("<a href='teachers'>Lærere</a><br /><a href='classes'>Klasser</a><br />");
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
                return RedirectToAction("Demo");
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

        [Route("adminonly")]
        public ContentResult TestingAdminRole()
        {
            if (IsRole(UserRoles.Admin))
            {
                return Content("Admin,");
            }

            return Content("Teacher,");
        }
    }
}