using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkemaSystem.Models;
using System.Diagnostics;
using System.Text;
using SkemaSystem.Models.ViewModels;
using System.Web.Security;

namespace SkemaSystem.Controllers
{
    //[Authorize(Roles="teacher")]
    [RouteArea("admin")]
    [RoutePrefix("teachers")]
    [Route("{action=index}")]
    public class TeacherController : BaseController
    {
        private SkeamSystemDb db = new SkeamSystemDb();

        // GET: /admin/teachers/
        [Route("")]
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        // GET: /admin/teachers/details/5
        [Route("details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /admin/teachers/create
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /admin/teachers/create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create([Bind(Include="Id,Name")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: /admin/teachers/edit/5
        [Route("edit/{id?}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            var model = new TeacherViewModel();

            model.Teacher = teacher;
            model.AvailableEducations = db.Educations;
            model.SelectedEducations = teacher.Educations;

            return View(model);
        }

        // POST: /admin/teachers/edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Teacher,PostedEducations")] TeacherViewModel result)
        {
            // 
            Teacher teacher = result.Teacher;
            List<Education> _educations = GetEducations(result.PostedEducations);

            if (ModelState.IsValid)
            {
                    Teacher _teacher = db.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
                    _teacher.Name = teacher.Name;
                    Debug.WriteLine("_educations=" + _educations.Count);
                    if (_teacher.Educations != null)
                    {
                        _teacher.Educations.Clear();
                    }
                    _teacher.Educations = _educations;
                    Debug.WriteLine("educations=" + _teacher.Educations.Count);
                    db.Entry(_teacher).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
            }

                return RedirectToAction("index");
        }

        // GET: /admin/teachers/delete/5
        [Route("delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: /admin/teachers/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<Education> GetEducations(PostedEducations postedEducations)
        {
            var selectedEducations = new List<Education>();
            var postedEducationIds = new string[0];

            if (postedEducations != null && postedEducations.EducationIds != null && postedEducations.EducationIds.Any())
            {
                postedEducationIds = postedEducations.EducationIds;
            }

            if (postedEducationIds.Any())
            {
                IEnumerable<Education> educations = db.Educations;
                selectedEducations = educations
                 .Where(x => postedEducationIds.Any(s => x.Id.ToString().Equals(s)))
                 .ToList();
            }

            return selectedEducations;
        }

        [HttpGet]
        [Route("~/admin/login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("~/admin/login")]
        public ActionResult Login(Teacher model)
        {
            if (model.IsValid(model.UserName, model.Password)){
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return Redirect("/");
            }
            else {
                ModelState.AddModelError("", "Login is invalid");
            }

            return View(model);
        }

        [Route("~/admin/logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        [Authorize(Roles = "Admin")]
        [Route("~/admin/adminonly")]
        public ContentResult TestingAdminRole()
        {
            return Content("Admin,");
        }
    }
}
