using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkemaSystem.Models;

namespace SkemaSystem.Controllers
{
    public class ClassController : BaseController
    {
        private ISkemaSystemDb db;

        public ClassController()
        {
           this.db = new SkeamSystemDb();
        }

        public ClassController(ISkemaSystemDb db)
        {
            this.db = db;
        }

        // GET: /Class/
        public ActionResult Index()
        {
            return View(db.Classes);
        }

        // GET: /Class/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classmodel = db.Classes.Single(x => x.Id == id);
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            return View(classmodel);
        }

        // GET: /Class/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ClassName")] ClassModel classmodel)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(classmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classmodel);
        }

         //GET: /Class/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classmodel = db.Classes.Find(id);
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            return View(classmodel);
        }

        // POST: /Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ClassName")] ClassModel classmodel)
        {
            if (ModelState.IsValid)
            {
                db.StateModified(classmodel);
                //db.Entry(classmodel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classmodel);
        }

        //// GET: /Class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classmodel = db.Classes.Find(id);
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            return View(classmodel);
        }

        //// POST: /Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassModel classmodel = db.Classes.Find(id);
            db.Classes.Remove(classmodel);
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

        public ActionResult SubjectDistribution(int id)
        {
            ClassModel classmodel = db.Classes.Single(x => x.Id == id);
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            if (classmodel.ActiveSchemes.Count > 0)
            {
                IEnumerable<SelectListItem> items = from s in classmodel.ActiveSchemes
                                                    select new SelectListItem 
                                                    { Text = s.Semester.Number+". Semester", Value=""+s.Id, Selected = false};
                ViewBag.Schemes = items;
            }
            return View(classmodel);
        }

        [HttpPost]
        public ActionResult StartNewSemester(int id)
        {
            ClassModel classmodel = db.Classes.Single(x => x.Id == id);
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            classmodel.CreateNewSemester();
            db.SaveChanges();
            return RedirectToActionPermanent("SubjectDistribution", new { id = id});
        }

        [HttpGet]
        public PartialViewResult ChangeScheme(string scheme)
        {
            return PartialView("_SchemeSubjectDistribution", db.Schemes.Single(x => x.Id == Int32.Parse(scheme)));
        }

    }
}
