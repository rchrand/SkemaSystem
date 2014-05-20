using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SkemaSystem.Controllers
{
    [RouteArea("admin")]
    [Route("{action=Login}")]
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsValid(model.UserName, model.Password)){
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return Redirect("/");
                }
                else {
                    ModelState.AddModelError("", "Login is invalid");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        public ContentResult TestingAdminRole()
        {
            return Content("Admin,");
        }
	}
}