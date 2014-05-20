using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using System.Collections.Generic;
using System.Linq;
using SkemaSystem.Tests.Controllers;
using SkemaSystem.Controllers;

namespace Tests.Features.CRUD_educations
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void CreateWithNoComplications()
        //{
        //    //Arrange
        //    var db = new FakeSkemaSystemDb();
        //    db.Classes = this.Classes();

        //    ClassController controller = new ClassController(db);

        //    ClassModel classmodel = new ClassModel(){ Id = db.Classes.Count() + 1, ClassName = "Datamagiker"};
        //    //Act
        //    controller.Create(classmodel);

        //    //Assert
        //    Assert.AreEqual(db.Classes.Count(), 1);
        //}

        //private IDbSet<ClassModel> Classes()
        //{
        //    var classes = new HashSet<ClassModel>();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        var classModel = new ClassModel() { ClassName = "12T" + i };
        //        classes.Add(classModel);
        //    }
        //    return classes;
        //}
    }
}
