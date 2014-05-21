using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SkemaSystem.Models;
using System.Diagnostics;
using SkemaSystem.Models.ViewModels;

namespace SkemaSystem.Controllers
{
    //add rooms
    //add semesters (wait)

    [RouteArea("Default", AreaPrefix="")]
    public class EducationController : BaseController
    {
        private SkeamSystemDb db;

        public EducationController()
        {
            db = new SkeamSystemDb();
        }
        
        //[Route("~/")]
        public ActionResult Index()
        {
            return View(db.Educations);
        }

        // GET: /Education/Details/5
        //[Route("{education}")]
        public ActionResult Details(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Education _education = db.Educations.FirstOrDefault(e => e.Name.Equals(name));
            if (_education == null)
            {
                return HttpNotFound();
            }
            return View(_education);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="Id,Name,NumberOfSemesters")] Education education)
        {
            if (ModelState.IsValid && CheckIfNameIsAvailable(education.Name) && CheckIfIdIsAvailable(education.Id))
            {
                db.Educations.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(education);
        }

        private bool CheckIfIdIsAvailable(int id)
        {
            if (db.Educations.SingleOrDefault(x => x.Id == id) != null)
            {
                return false;
            }
            return true;
        }

        private bool CheckIfNameIsAvailable(string name)
        {
            if (db.Educations.SingleOrDefault(x => x.Name.Equals(name)) != null)
            {
                return false;
            }
            return true;
        }

        // GET: /Education/Edit/5
        //[Route("{education}/edit")]
        public ActionResult Edit(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Education education = db.Educations.Find(id);
            Education _education = db.Educations.First(e => e.Name.Equals(name));
            if (_education == null)
            {
                return HttpNotFound();
            }
            return View(_education);
        }

        // POST: /Education/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,NumberOfSemesters")] Education education)
        {
            //needs to check if the new name is already used
            if (ModelState.IsValid)
            {
                db.Entry(education).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(education);
        }


        // GET
        [HttpGet]
        public ActionResult ModifyTeachers(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EducationViewModel education = new EducationViewModel()
            {
                Education = db.Educations.FirstOrDefault(e => e.Name.Equals(name)),
                AvailableTeachers = db.Teachers
            };

            education.SelectedTeachers = education.Education.Teachers;    

            if (education == null)
            {
                return HttpNotFound();
            }

            return View(education);

        }

        //POST: 
        [HttpPost]
        public ActionResult ModifyTeachers([Bind(Include = "Education,PostedTeachers")] EducationViewModel result)
        {
            Education education = result.Education;
            List<Teacher> _teachers = GetTeachers(result.PostedTeachers);

            if (ModelState.IsValid)
            {
                Education _education = db.Educations.FirstOrDefault(e => e.Id == education.Id);
                _education.Name = education.Name;
                if (_education.Teachers != null)
                {
                    _education.Teachers.Clear();
                }
                _education.Teachers = _teachers;
                Debug.WriteLine("educations=" + _education.Teachers.Count);
                db.Entry(_education).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        private List<Teacher> GetTeachers(PostedTeachers postedTeachers)
        {
            var selectedTeachers = new List<Teacher>();
            var postedTeacherIds = new string[0];

            if (postedTeachers != null && postedTeachers.TeacherIds != null && postedTeachers.TeacherIds.Any())
            {
                postedTeacherIds = postedTeachers.TeacherIds;
            }

            if (postedTeacherIds.Any())
            {
                IEnumerable<Teacher> teachers = db.Teachers;
                selectedTeachers = teachers
                 .Where(x => postedTeacherIds.Any(s => x.Id.ToString().Equals(s)))
                 .ToList();
            }

            return selectedTeachers;
        }

        // GET
        [HttpGet]
        public ActionResult ModifyRooms(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EducationViewModel education = new EducationViewModel()
            {
                Education = db.Educations.FirstOrDefault(e => e.Name.Equals(name)),
                AvailableRooms = db.Rooms
            };

            

            if (education == null)
            {
                return HttpNotFound();
            }

            education.SelectedRooms = education.Education.Rooms;

            return View(education);

        }

        

        //POST
        [HttpPost]
        public ActionResult ModifyRooms([Bind(Include = "Education,PostedRooms")] EducationViewModel result)
        {
            Education education = result.Education;
            List<Room> _rooms = GetRooms(result.PostedRooms);

            if (ModelState.IsValid)
            {
                Education _education = db.Educations.FirstOrDefault(e => e.Id == education.Id);
                _education.Name = education.Name;
                if (_education.Rooms != null)
                {
                    _education.Rooms.Clear();
                }
                _education.Rooms = _rooms;
                
                db.Entry(_education).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        private List<Room> GetRooms(PostedRooms postedRooms)
        {
            var selectedRooms = new List<Room>();
            var postedRoomIds = new string[0];

            if (postedRooms != null && postedRooms.RoomIds != null && postedRooms.RoomIds.Any())
            {
                postedRoomIds = postedRooms.RoomIds;
            }

            if (postedRoomIds.Any())
            {
                IEnumerable<Room> rooms = db.Rooms;
                selectedRooms = rooms
                 .Where(x => postedRoomIds.Any(s => x.Id.ToString().Equals(s)))
                 .ToList();
            }

            return selectedRooms;
        }

        // GET: /Education/Delete/5
        public ActionResult Delete(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Education education = db.Educations.Find(id);
            Education _education = db.Educations.First(e => e.Name.Equals(name));
            if (_education == null)
            {
                return HttpNotFound();
            }
            return View(_education);
        }

        // POST: /Education/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name)
        {
            //Education education = db.Educations.Find(id);
            Education _education = db.Educations.First(e => e.Name.Equals(name));
            db.Educations.Remove(_education);
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
