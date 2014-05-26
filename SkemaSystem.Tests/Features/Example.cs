using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using SkemaSystem.Controllers;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using SkemaSystem.Tests.Features;
using System.Web.Routing;
using SkemaSystem;

// Skriv UserStory og tasks her!

namespace Tests.Features
{
    [TestClass]
    public class Example
    {
        SkeamSystemDb db;

        [TestInitialize]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData(
              "DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            db = new SkeamSystemDb();

            db.Classes.ToList().Clear();
            db.Teachers.ToList().Clear();

            db.Teachers.Add(new Teacher { Id = 1, Name = "Kaj", UserName = "eaakaj" });
            db.Teachers.Add(new Teacher { Id = 2, Name = "Hanne", UserName = "eaasommer" });

            db.Classes.Add(new ClassModel { Id = 1, ClassName = "12.t"});
            db.Classes.Add(new ClassModel { Id = 2, ClassName = "12.i"});
        }

        [TestCleanup]
        public void CleanUp()
        {
            db.Teachers.ToList().Clear();
            db.Classes.ToList().Clear();
        }

        [TestMethod]
        public void DoesKajExist()
        {
            var _object = db.Teachers.Any(t => t.Name == "Kaj");

            Assert.IsNotNull(_object);
        }

        [TestMethod]
        public void Does12Exists()
        {
            var _object = db.Classes.Any(t => t.ClassName.Contains("12"));

            Assert.IsTrue(_object);
        }

        [TestMethod]
        public void TestingIndex()
        {
            var controller = new ClassController();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
