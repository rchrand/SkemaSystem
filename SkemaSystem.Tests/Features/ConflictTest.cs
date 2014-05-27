using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkemaSystem.Models;
using System.IO;

namespace SkemaSystem.Tests.Features
{
    [TestClass]
    public class ConflictTest
    {
        SkeamSystemDb db;

        [TestInitialize]
        public void Init()
        {
            AppDomain.CurrentDomain.SetData(
              "DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));

            db = new SkeamSystemDb();
        }

        [TestMethod]
        public void TeachersMatch ()
        {
            
        }

        [TestMethod]
        public void FindAHole()
        {

        }
    }
}
