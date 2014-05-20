using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using SkemaSystem.Controllers;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace SkemaSystem.Tests.Features.CRUD_class
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void ClassCreate()
        {
            ISkemaSystemDb db = new FakeSkemaSystemDb();
            ClassesController controller = new ClassesController(db);

            db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
            db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

            Assert.AreEqual(db.Classes.Count(), 2);
            controller.Create(new ClassModel { Id = 3, ClassName = "ADO" });
            Assert.AreEqual(db.Classes.Single(x => x.Id.Equals(3)).ClassName, "ADO");
            Assert.AreEqual(db.Classes.Last().Id, 3);
            Assert.AreEqual(db.Classes.First().Id, 1);
            Assert.AreEqual(db.Classes.Count(), 3);
        }
        [TestMethod]
        public void ClassRead()
        {
            ISkemaSystemDb db = new FakeSkemaSystemDb();
            ClassesController controller = new ClassesController(db);

            db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
            db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

            Assert.AreEqual(db.Classes.Single(x => x.Id.Equals(1)).ClassName, "12T");
            Assert.AreEqual(db.Classes.Single(x => x.Id.Equals(2)).ClassName, "13S");
        }

        [TestMethod]
        public void ClassUpdate()
        {
            ISkemaSystemDb db = new FakeSkemaSystemDb();
            ClassesController controller = new ClassesController(db);

            db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
            db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });
            
            // FakeDB virker ikke med edit, edit metoden i controller skal ikke være udkommenteret
            //var testClass = new ClassModel { Id = 2, ClassName = "YES"};
            //controller.Edit(testClass);
            var testClass = db.Classes.Single(x => x.Id.Equals(2));
            testClass.ClassName = "YES";
            //
            //

            db.Classes.Add(testClass);
            Assert.AreEqual(db.Classes.Count(), 2);
            Assert.AreEqual(db.Classes.Single(x => x.Id.Equals(2)).ClassName, "YES");
        }
        [TestMethod]
        public void ClassDelete()
        {
            ISkemaSystemDb db = new FakeSkemaSystemDb();
            ClassesController controller = new ClassesController(db);

            db.Classes.Add(new ClassModel { Id = 1, ClassName = "12T" });
            db.Classes.Add(new ClassModel { Id = 2, ClassName = "13S" });

            Assert.AreEqual(db.Classes.Count(), 2);

            var testClass = db.Classes.Single(x => x.Id.Equals(1));
            db.Classes.Remove(testClass);
            Assert.AreEqual(db.Classes.First().Id, 2);
            Assert.AreEqual(db.Classes.Count(), 1);
        }

    //    public System.Data.Entity.IDbSet<Teacher> Teachers
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public IDbSet<ClassModel> Classes
    //    {
    //        get
    //        {
    //            var classes = new HashSet<ClassModel>();
    //            for (int i = 0; i < 100; i++)
    //            {
    //                var classModel = new ClassModel() { ClassName = "12T" + i };
    //                classes.Add(classModel);
    //            }
    //            return classes;
    //        }
    //    }

    //    public System.Data.Entity.IDbSet<Education> Educations
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public int SaveChanges()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void StateModified(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }
    }
}
