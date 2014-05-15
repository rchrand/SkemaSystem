using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem;
using SkemaSystem.Controllers;

namespace SkemaSystem.Tests.Controllers
{
    [TestClass]
    public class RoomControllerTest
    {
        [TestMethod]
        public void Rooms()
        {
            //Arrange
            RoomController controller = new RoomController();

            //Act
            ViewResult result = controller.Rooms() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateRoom()
        {
            //Arrange
            RoomController controller = new RoomController();

            //Act
            ViewResult result = controller.CreateRoom() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteRoom()
        {
            //Arrange
            RoomController controller = new RoomController();

            //Act
            ViewResult result = controller.DeleteRoom() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateRoom()
        {
            //Arrange
            RoomController controller = new RoomController();

            //Act
            ViewResult result = controller.UpdateRoom() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
