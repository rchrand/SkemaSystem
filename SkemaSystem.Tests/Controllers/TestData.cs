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
                    var teacher = new Teacher() { Name = "Hanne" + i};
                    teachers.Add(teacher); 
                }
                return teachers.AsQueryable();
            }
        }

        public static IQueryable<Room> Rooms 
        { 
            get
            {
                var rooms = new List<Room>();
                rooms.Add(new Room { RoomName = "A1.1" });
                rooms.Add(new Room { RoomName = "A1.12" });
                rooms.Add(new Room { RoomName = "A1.13" });
                rooms.Add(new Room { RoomName = "A1.14" });
                rooms.Add(new Room { RoomName = "A1.15" });
                return rooms.AsQueryable();
            }
        }
    }
}
