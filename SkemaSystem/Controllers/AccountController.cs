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
            if (model.IsValid(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return Redirect("/");
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

        [Authorize(Roles = "Admin")]
        [Route("adminonly")]
        public ContentResult TestingAdminRole()
        {
            return Content("Admin,");
        }
    }
}