using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using SkemaSystem.Tests.Controllers;
using SkemaSystem.Controllers;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

// Skriv UserStory og tasks her!

namespace Tests.Features
{
    [TestClass]
    public class Example
    {
        

        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var db = new FakeSkemaSystemDb();
            db.Teachers.Add(new Teacher() { Id = 1, Name = "Haso" });
            
            HomeController controller = new HomeController(db);

            var model =
                from t in db.Teachers
                orderby t.Name ascending
                select t;

            //Act
            IQueryable<Teacher> result = (IQueryable<Teacher>)(controller.Index("DMU") as ViewResult).Model;

            //Assert
            Assert.AreEqual(model.Single(t => t.Name.Equals("Haso")), result.Single(t => t.Name.Equals("Haso")));
        }
    }
}
