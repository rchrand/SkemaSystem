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
    [RouteArea("admin")]
    [RoutePrefix("class")]
    [Route("{action=index}")]
    public class ClassController : BaseController
    {
        private SkeamSystemDb db;

        public ClassController()
        {
           this.db = new SkeamSystemDb();
        }

        // GET: /Class/
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }



        // GET: /Class/Details/5
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
        public ActionResult Create([Bind(Include = "Id,ClassName")] ClassModel classmodel)
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
        public ActionResult Edit([Bind(Include = "Id,ClassName")] ClassModel classmodel)
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

        public ActionResult SubjectDistribution(int id)
        {
            ClassModel classmodel = db.Classes.Single(x => x.Id == id);
            if (classmodel == null)
            {
                return HttpNotFound();
            }
            if (classmodel.ActiveSchemes.Count > 0)
            {
                var latestScheme = classmodel.ActiveSchemes[classmodel.ActiveSchemes.Count() - 1];

                IEnumerable<SelectListItem> items = from s in classmodel.ActiveSchemes
                                                    select new SelectListItem { Text = s.Semester.Number + ". Semester", Value = "" + s.Id };
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
            int schemeId = Int32.Parse(scheme);
            return PartialView("_SchemeSubjectDistribution", db.Schemes.Single(x => x.Id == schemeId));
        }

        [HttpPost]
        public PartialViewResult AddSubjectDistBlock(int scheme, int add_subject, int add_teacher, int add_blockscount)
        {
            Scheme theScheme = db.Schemes.Single(x => x.Id == scheme);
            if (theScheme.AddLessonBlock(db.Teachers.SingleOrDefault(x => x.Id == add_teacher), db.Subjects.SingleOrDefault(x => x.Id == add_subject), add_blockscount))
            {
                db.SaveChanges();
            }
            else
            {
                // Didn't succeed!
                ViewBag.add_subject = add_subject;
                ViewBag.add_teacher = add_teacher;
                ViewBag.add_blockscount = add_blockscount;
                ViewBag.Error = "- Der er ikke nok ledige blokke på semestret til, at udføre denne handling.";
            }
            
            return PartialView("_SchemeSubjectDistribution", theScheme);
        }

    }
}
