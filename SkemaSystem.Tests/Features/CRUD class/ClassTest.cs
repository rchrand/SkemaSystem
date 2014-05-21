using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using SkemaSystem.Controllers;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace SkemaSystem.Tests.Features.CRUD_class
{
    //[TestClass]
    //public class ClassTest
    //{

    //    [TestMethod]
    //    public void ClassCreate()
    //    {
    //        ISkemaSystemDb db = new FakeSkemaSystemDb();
    //        ClassesController controller = new ClassesController(db);

    //        db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
    //        db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

    //        Assert.AreEqual(2, db.Classes.Count());
    //        controller.Create(new ClassModel { Id = 3, ClassName = "ADO" });
    //        Assert.AreEqual("ADO", db.Classes.Single(x => x.Id.Equals(3)).ClassName);
    //        Assert.AreEqual(3, db.Classes.Last().Id);
    //        Assert.AreEqual(1, db.Classes.First().Id);
    //        Assert.AreEqual(3, db.Classes.Count());
    //    }
    //    [TestMethod]
    //    public void ClassRead()
    //    {
    //        ISkemaSystemDb db = new FakeSkemaSystemDb();
    //        ClassesController controller = new ClassesController(db);

    //        db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
    //        db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

    //        Assert.AreEqual("12T", db.Classes.Single(x => x.Id.Equals(1)).ClassName);
    //        Assert.AreEqual("13S", db.Classes.Single(x => x.Id.Equals(2)).ClassName);
    //    }

    //    [TestMethod]
    //    public void ClassUpdate()
    //    {
    //        ISkemaSystemDb db = new FakeSkemaSystemDb();
    //        ClassesController controller = new ClassesController(db);

    //        db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
    //        db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

    //        var testClass = db.Classes.Single(x => x.Id.Equals(2));
    //        testClass.ClassName = "YES";
    //        controller.Edit(testClass);
    //        Assert.AreEqual(2, db.Classes.Count());
    //        Assert.AreEqual("YES", db.Classes.Single(x => x.Id.Equals(2)).ClassName);
    //    }
    //    [TestMethod]
    //    public void ClassDelete()
    //    {
    //        ISkemaSystemDb db = new FakeSkemaSystemDb();
    //        ClassesController controller = new ClassesController(db);

    //        db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
    //        db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

    //        Assert.AreEqual(2, db.Classes.Count());

    //        var testClass = db.Classes.Single(x => x.Id.Equals(1));
    //        controller.DeleteConfirmed(testClass.Id);
    //        Assert.AreEqual(2, db.Classes.First().Id);
    //        Assert.AreEqual(1, db.Classes.Count());
    //    }
    //}
}
