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
    [Authorize(Roles="Admin,Master")]
    [RouteArea("Admin", AreaPrefix="admin")]
    [RoutePrefix("{education}/teachers")]
    [Route("{action=index}/{id?}")]
    public class TeacherController : BaseController
    {
        [Route("~/admin/teachers")]
        public ActionResult Redirect()
        {
            Teacher teacher = db.Teachers.SingleOrDefault(t => t.Id.Equals(User.Id));

            Education education = teacher.Educations.FirstOrDefault();

            return RedirectToAction("Index", new { education = education.Name.ToLower() });
        }
        // GET: /admin/teachers/
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        // GET: /admin/teachers/details/5
        [Route("details/{id?}")]
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
            var model = new TeacherViewModel();

            model.Teacher = null;
            model.AvailableEducations = db.Educations;
            model.SelectedEducations = null;

            return View(model);
        }

        // POST: /admin/teachers/create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [Route("create"), HttpPost]
        public ActionResult Create(TeacherViewModel result)
        {
            if (ModelState.IsValid)
            {
                List<Education> _educations = GetEducations(result.PostedEducations);
                result.Teacher.Educations = _educations;

                db.Teachers.Add(result.Teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(result);
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
        [Route("edit/{id?}")]
        public ActionResult Edit(TeacherViewModel result)
        {
            // 
            Teacher teacher = result.Teacher;
            List<Education> _educations = GetEducations(result.PostedEducations);

            if (ModelState.IsValid)
            {
                    Teacher _teacher = db.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
                    _teacher.Name = teacher.Name;
                    _teacher.Username = teacher.Username;
                    _teacher.Role = teacher.Role;
                    //_teacher.Password = teacher.Password; // TODO move to action ChangePassword
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
        [Route("delete/{id?}")]
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
        [Route("delete/{id}")]
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
    }
}
