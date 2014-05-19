using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkemaSystem.Models;
using System.Diagnostics;

namespace SkemaSystem.Controllers
{
    public class EducationController : BaseController
    {
        private SkeamSystemDb db = new SkeamSystemDb();

        // GET: /Education/Details/5
        public ActionResult Details(string name)
        {
            Debug.WriteLine(name);

            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Education education = db.Educations.Find(name);
            Education education = db.Educations.First(e => e.Name == name);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // GET: /Education/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Education/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Educations.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(education);
        }

        // GET: /Education/Edit/5
        public ActionResult Edit(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Education education = db.Educations.Find(id);
            Education education = db.Educations.First(e => e.Name == name);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: /Education/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Entry(education).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(education);
        }

        // GET: /Education/Delete/5
        public ActionResult Delete(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Education education = db.Educations.Find(id);
            Education education = db.Educations.First(e => e.Name == name);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: /Education/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name)
        {
            //Education education = db.Educations.Find(id);
            Education education = db.Educations.First(e => e.Name == name);
            db.Educations.Remove(education);
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
    }
}
