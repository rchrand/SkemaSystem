using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkemaSystem.Tests.Controllers
{
    class TestData
    {
        public static IQueryable<Teacher> Teachers
        {
            get
            {
                var teachers = new List<Teacher>();
                for (int i = 0; i < 100; i++)
                {
                    var teacher = new Teacher() { Id = i + 1, Name = "Hanne" + i};
                    teachers.Add(teacher); 
                }
                return teachers.AsQueryable();
            }
        }
    }
}
