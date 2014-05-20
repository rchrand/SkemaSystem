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

namespace SkemaSystem.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("classes")]
    [Route("{action=index}")]
    public class ClassesController : BaseController
    {
        private SkeamSystemDb db;

        public ClassesController()
        {
           this.db = new SkeamSystemDb();
        }

        // GET: /Classes/
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        public ActionResult Index(string className)
        {
            return View(db.Classes.ToList());
        }

        // GET: /Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classmodel = db.Classes.SingleOrDefault(x => x.Id.Equals(id));
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            return View(classmodel);
        }

        // GET: /Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Classes/Create
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

        // GET: /Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classmodel = db.Classes.SingleOrDefault(x => x.Id.Equals(id));
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            return View(classmodel);
        }

        // POST: /Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ClassName")] ClassModel classmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classmodel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classmodel);
        }

        // GET: /Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassModel classmodel = db.Classes.SingleOrDefault(x => x.Id.Equals(id));
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            return View(classmodel);
        }

        // POST: /Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassModel classmodel = db.Classes.Single(x => x.Id.Equals(id));
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
    }
}
