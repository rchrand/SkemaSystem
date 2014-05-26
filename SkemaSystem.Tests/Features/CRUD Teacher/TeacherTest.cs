using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;

namespace SkemaSystem.Tests.Features.CRUD_Teacher
{
    [TestClass]
    public class TeacherTest
    {
        [TestInitialize]
        public void Init()
        {

        }

        //[TestMethod]
        public void TestIsVaild()
        {
            var t1 = new Teacher() { Name = "Erik Jacobsen", UserName = "eaaej", Password = "russia" };
            var t2 = new Teacher() { Name = "Karsten", UserName = "eaakk", Password = "skak" };

            // Does not work, since IsValid looks in the db
            //Assert.IsTrue(t1.IsValid("eaaej", "russia"));
            //Assert.IsFalse(t2.IsValid("eaakk", "go"));
        }
    }
}
