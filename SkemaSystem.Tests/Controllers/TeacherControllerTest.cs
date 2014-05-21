using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem;
using SkemaSystem.Controllers;
using SkemaSystem.Models;

namespace SkemaSystem.Tests.Controllers
{
    [TestClass]
    public class TeacherControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var db = new FakeSkemaSystemDb();
            db.AddSet(TestData.Teachers);

            TeacherController controller = new TeacherController(db);

            var teachers =
                from t in db.Query<Teacher>()
                select t;

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(controller.ViewResult));

            Assert.Equals(teachers.Count(), ((IOrderedQueryable<Teacher>)((ViewResult)result).Model).Count());
        }

        [TestMethod]
        public void Edit()
        {
            var db = new FakeSkemaSystemDb();
            db.AddSet(TestData.Teachers);

            TeacherController controller = new TeacherController(db);

            var teacher =
                from t in db.Query<Teacher>()
                where t.Id == 1
                select t;

            Teacher _teacher = teacher.First();
            _teacher.Name = "Martin";

            var result = controller.Edit(_teacher);

            Assert.IsInstanceOfType(result, typeof(controller.View));

            var teacher1 =
                from t in db.Query<Teacher>()
                where t.Id == 1
                select t;

            Teacher _teacher1 = teacher1.First();

            Assert.Equals(_teacher.Name, _teacher1.Name);
        }

        [TestMethod]
        public void Create()
        {
            var db = new FakeSkemaSystemDb();
            db.AddSet(TestData.Teachers);

            TeacherController controller = new TeacherController(db);

            Teacher teacher = new Teacher() { Name = "Martin" };

            var result = controller.Create(teacher);

            Assert.IsInstanceOfType(result, typeof(controller.View));

            var teachers =
                from t in db.Query<Teacher>()
                select t;

            Teacher _teacher = teachers.Last();

            Assert.Equals(_teacher.Name, "Martin");
        }

        [TestMethod]
        public void Delete()
        {
            var db = new FakeSkemaSystemDb();
            db.AddSet(TestData.Teachers);

            TeacherController controller = new TeacherController(db);

            var teachers =
                from t in db.Query<Teacher>()
                select t;

            Teacher teacher = teachers.First();

            var result = controller.Delete(teacher);

            Assert.IsInstanceOfType(result, typeof(controller.RedirectToAction));

            var teachers1 =
                from t in db.Query<Teacher>()
                select t;

            Teacher _teacher = teachers.Last();

            Assert.Equals(teachers.Count(), teachers1.Count());
        }
    }
}
