using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;

namespace SkemaSystem.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var db = new FakeSkemaSystemDb();

        }
    }
}
