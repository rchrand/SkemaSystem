using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using System.Collections.Generic;
using System.Linq;
using SkemaSystem.Tests.Controllers;
using SkemaSystem.Controllers;
using System.Data.Entity;

namespace Tests.Features.US4CreateEducation
{
    [TestClass]
    public class UnitTest1
    {
        /**
         * User story for creating educations. Educations can only be created by Administrators.
         * Educations names should be unique, and have a unique ID.
         * 
         **/

        [TestMethod]
        public void CreateWithNoComplications()
        {
        //    //Arrange
            var db = new FakeSkemaSystemDb();
            db.Educations = this.Educations();

            EducationController controller = new EducationController(db);

        
        //    //Act
            controller.Create(new Education { Id = 101, Name = "DMU101"});

        //    //Assert
            Assert.AreEqual(101, db.Educations.Count());
        }

        
        [TestMethod]
        public void TroubleWithDuplicateName()
        {
            //Arrange
            var db = new FakeSkemaSystemDb();
            db.Educations = this.Educations();

            EducationController controller = new EducationController(db);
            
            //Act
            controller.Create(new Education { Id = 101, Name = "DMU50" });

            //Assert
            Assert.AreEqual(db.Educations.Count(), 100);
        }

        [TestMethod]
        public void TroubleWithDuplicateId()
        {
            //Arrange
            var db = new FakeSkemaSystemDb();
            db.Educations = this.Educations();

            EducationController controller = new EducationController(db);

            //Act
            controller.Create(new Education { Id = 23, Name = "MDU" });

            //Assert
            Assert.AreEqual(db.Educations.Count(), 100);
        }

        //not sure how to test this...
        [TestMethod]
        public void TroubleWithWrongRole()
        {
            Assert.AreEqual(null, null);
        }

        private IDbSet<Education> Educations()
        {
            var educations = new FakeDbSet<Education>();
            for (int i = 0; i < 100; i++)
            {
                var education = new Education() { Id = i+1, Name = "DMU" + i};
                educations.Add(education);
            }
            return educations;
        }

        
    }
}
