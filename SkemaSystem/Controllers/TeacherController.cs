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
        [Route("edit")]
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
            //List<SelectListItem> educations = new List<SelectListItem>();
            //educations.Add(new SelectListItem { Text = "DMU", Value = "1", Selected = false });

            //teacher.Educations.Add(db.Educations.First());
            //ViewBag.Educations = db.Educations.ToList();
            return View(GetFruitsInitialModel(teacher));
            //return View(new Test(teacher, educations));
        }

        // POST: /admin/teachers/edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("edit")]
        public ActionResult Edit([Bind(Include="Teacher,PostedEducations")] TeacherViewModel result)
        {
            Teacher teacher = result.Teacher;
            if (result.PostedEducations != null && result.PostedEducations.EducationIds.Any())
            {
                IEnumerable<Education> educations = db.Educations;
                teacher.Educations = educations
                 .Where(x => result.PostedEducations.EducationIds.Any(s => x.Id.ToString().Equals(s)))
                 .ToList();
            }
            else
            {
                teacher.Educations = new List<Education>();
            }

            if (ModelState.IsValid)
            {
                    Teacher _teacher = db.Teachers.FirstOrDefault(t => t.Id == teacher.Id);
                    _teacher.Name = teacher.Name;
                    _teacher.Educations = teacher.Educations;
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

        public class TeacherViewModel
        {
            public Teacher Teacher { get; set; }
            public IEnumerable<Education> AvailableEducations { get; set; }
            public IEnumerable<Education> SelectedEducations { get; set; }
            public PostedEducations PostedEducations { get; set; }
        }

        public class PostedEducations
        {
            //this array will be used to POST values from the form to the controller
            public string[] EducationIds { get; set; }
        }

        private TeacherViewModel GetFruitsModel(Teacher teacher, PostedEducations postedEducations)
        {
            // setup properties
            var model = new TeacherViewModel();
            var selectedEducations = new List<Education>();
            var postedEducationIds = new string[0];
            if (postedEducations == null) postedEducations = new PostedEducations();

            // if a view model array of posted fruits ids exists
            // and is not empty,save selected ids
            if (postedEducations.EducationIds != null && postedEducations.EducationIds.Any())
            {
                postedEducationIds = postedEducations.EducationIds;
            }

            // if there are any selected ids saved, create a list of fruits
            if (postedEducationIds.Any())
            {
                IEnumerable<Education> educations = db.Educations;
                selectedEducations = /*FruitRepository.GetAll(db)*/educations
                 .Where(x => postedEducationIds.Any(s => x.Id.ToString().Equals(s)))
                 .ToList();
            }

            //setup a view model
            model.Teacher = teacher;
            model.AvailableEducations = db.Educations;//FruitRepository.GetAll(db).ToList();
            model.SelectedEducations = selectedEducations;
            model.PostedEducations = postedEducations;

            return model;
        }

        /// <summary>
        /// for setup initial view model for all fruits
        /// </summary>
        private TeacherViewModel GetFruitsInitialModel(Teacher teacher)
        {
            //setup properties
            var model = new TeacherViewModel();
            var selectedEducations = teacher.Educations;// new List<Education>();

            //setup a view model
            model.Teacher = teacher;
            model.AvailableEducations = db.Educations;//FruitRepository.GetAll(db).ToList();
            model.SelectedEducations = selectedEducations;

            return model;
        }

        /*public static class FruitRepository
        {
            /// <summary>
            /// for get fruit for specific id
            /// </summary>
            public static Education Get(SkeamSystemDb db, int id)
            {
                return GetAll(db).FirstOrDefault(x => x.Id.Equals(id));
            }

            /// <summary>
            /// for get all fruits
            /// </summary>
            public static IEnumerable<Education> GetAll(SkeamSystemDb db)
            {
                return db.Educations;
                //    new List<Education> {
                //    new Education {Name = "Apple", Id = 1 },
                //    new Education {Name = "Banana", Id = 2},
                //    new Education {Name = "Cherry", Id = 3},
                //    new Education {Name = "Pineapple", Id = 4},
                //    new Education {Name = "Grape", Id = 5},
                //    new Education {Name = "Guava", Id = 6},
                //    new Education {Name = "Mango", Id = 7}
                //};
            }
        }*/
    }
}
