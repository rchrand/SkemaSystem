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
    [Authorize(Roles="Admin,Master")]
    [RouteArea("Admin", AreaPrefix="admin")]
    [RoutePrefix("{education}/rooms")]
    [Route("{action=index}/{id?}")]
    public class RoomController : BaseController
    {
        [Route("~/admin/rooms")]
        public ActionResult Redirect()
        {
            Teacher teacher = db.Teachers.SingleOrDefault(t => t.Id.Equals(User.Id));

            Education education = teacher.Educations.FirstOrDefault();

            return RedirectToAction("Index", new { education = education.Name.ToLower() });
        }

        public ActionResult Index()
        {
            return View(db.Rooms.ToList());
        }

        [Route("details/{id?}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("create"), HttpPost]
        public ActionResult Create([Bind(Include="ID,RoomName")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        [Route("edit/{id?}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id?}")]
        public ActionResult Edit([Bind(Include="ID,RoomName")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        [Route("delete/{id?}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id?}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
