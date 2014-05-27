using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;

namespace SkemaSystem.Controllers
{
    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("classes")]
    [Route("{action=index}/{id?}")]
    public class ClassController : BaseController
    {
        // GET: /Class/
        [Route("")]
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        // GET: /Class/Details/5
        [Route("details/{id?}")]
        public ActionResult Details(int? id)
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

        // GET: /Classes/Create
        [Route("create")]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> items = from s in db.Educations.ToList()
                                                select new SelectListItem { Text = s.Name, Value = s.Id.ToString() };

            ViewBag.Educations = items;

            return View(new ClassViewModel());
        }

        // POST: /Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(ClassViewModel result)
        {
            if (ModelState.IsValid)
            {
                result.ClassModel.Education = db.Educations.ToList().Where(e => e.Id.Equals(result.Education)).SingleOrDefault();

                db.Classes.Add(result.ClassModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            IEnumerable<SelectListItem> items = from s in db.Educations.ToList()
                                                select new SelectListItem { Text = s.Name, Value = s.Id.ToString() };

            ViewBag.Educations = items;

            return View(result);
        }

        [Route("edit/{id?}")]
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

            IEnumerable<SelectListItem> items = from s in db.Educations.ToList()
                                                select new SelectListItem { Text = s.Name, Value = s.Id.ToString() };

            ViewBag.Educations = items;

            var model = new ClassViewModel();
            model.ClassModel = classmodel;
            model.SelectedEducation = classmodel.Education.Id;

            return View(model);
        }

        // POST: /Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("edit/{id?}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassViewModel result)
        {
            if (ModelState.IsValid)
            {
                var classModel = db.Classes.Find(result.ClassModel.Id);
                classModel.ClassName = result.ClassModel.ClassName;
                classModel.Education = db.Educations.ToList().Where(e => e.Id.Equals(result.Education)).SingleOrDefault();

                db.Entry(classModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            IEnumerable<SelectListItem> items = from s in db.Educations.ToList()
                                                select new SelectListItem { Text = s.Name, Value = s.Id.ToString() };

            ViewBag.Educations = items;

            return View(result);
        }

        // GET: /Classes/Delete/5
        [Route("delete/{id?}")]
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

        // POST: /Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id?}")]
        public ActionResult DeleteConfirmed(int? id)
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
            return RedirectToActionPermanent("SubjectDistribution", new { id = id });
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
            if (add_blockscount > 0 && add_blockscount < 100 && theScheme.AddLessonBlock(db.Teachers.SingleOrDefault(x => x.Id == add_teacher), db.Subjects.SingleOrDefault(x => x.Id == add_subject), add_blockscount))
            {
                db.SaveChanges();
            }
            else
            {
                // Didn't succeed!
                ViewBag.add_subject = add_subject;
                ViewBag.add_teacher = add_teacher;
                ViewBag.add_blockscount = add_blockscount;
                if (add_blockscount < 0 || add_blockscount > 100)
                {
                    ViewBag.Error = "- Antal blokke skal være mellem 0 og 100.";
                }
                else
                {
                    ViewBag.Error = "- Der er ikke nok ledige blokke på semestret til, at udføre denne handling.";
                }
            }
            return PartialView("_SchemeSubjectDistribution", theScheme);
        }

        [HttpGet]
        public ActionResult CreateSemester()
        {
            var semester = db.Educations.Where(e => e.Name.Equals("DMU")).Select(s => s.Semesters).FirstOrDefault();

            List<SemesterViewModel> list = new List<SemesterViewModel>();

            foreach (var item in semester)
            {
                list.Add(new SemesterViewModel { semester = item });
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult CreateSemester(string[] semesterId, string[] start, string[] finish)
        //public ActionResult CreateSemester(string[] semesterId, string[] start, string[] finish, Education education)
        {
            Services.ConflictService service = new Services.ConflictService();

            var classes = from c in db.Classes
                           where c.ActiveSchemes.Count < c.Education.Semesters.Count
                           select c;

            foreach (var item in classes)
            {
                List<Semester> semesters = (from s in item.Education.Semesters
                                            select s).ToList();

                int semesterNumber = item.ActiveSchemes.Count;

                service.setNewSemesterForClass(item, semesters[semesterNumber], Convert.ToDateTime(start[semesterNumber]), Convert.ToDateTime(finish[semesterNumber]));
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
