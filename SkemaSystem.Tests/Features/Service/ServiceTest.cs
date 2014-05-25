using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkemaSystem.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Service;

namespace SkemaSystem.Tests.ServiceTest
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void setNewSemesterForClassTest()
        {
            Semester semester1 = new Semester { Id = 1, Number = 1 };
            Semester semester2 = new Semester { Id = 2, Number = 2 };
            Education education = new Education { Name = "DMU", Semesters = new List<Semester> { semester1, semester2 }, NumberOfSemesters = 2 };
            ClassModel model = new ClassModel { Education = education, ClassName = "12T" };

            DateTime start = DateTime.Today;
            DateTime finish = DateTime.Today.AddDays(30);

            SkemaSystem.Service.Service service = new SkemaSystem.Service.Service();

            service.setNewSemesterForClass(model, semester1, start, finish);

            Assert.AreEqual(start, model.ActiveSchemes[0].SemesterStart);
            Assert.AreEqual(finish, model.ActiveSchemes[0].SemesterFinish);
        }
    }
}
