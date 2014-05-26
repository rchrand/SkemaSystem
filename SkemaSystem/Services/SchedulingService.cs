using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SkemaSystem.Services
{
    public class SchedulingService
    {
        /*
         * Dictionary<int, List<TableCellViewModel>> dic = new Dictionary<int,List<TableCellViewModel>>();
            dic.Add(0, new List<TableCellViewModel>() { new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { Teacher = db.Teachers.FirstOrDefault(), SubjectName = "SD", Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
            dic.Add(1, new List<TableCellViewModel>() { new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, null, null, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
            dic.Add(2, new List<TableCellViewModel>() { new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { Teacher = db.Teachers.FirstOrDefault(), SubjectName = "SD", Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
            dic.Add(3, new List<TableCellViewModel>() { null, new TableCellViewModel() { Teacher = db.Teachers.FirstOrDefault(), SubjectName = "SD", Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } }, new TableCellViewModel() { SubjectName = "SD", Teacher = db.Teachers.FirstOrDefault(), Room = new Room() { RoomName = "A1.1" } } });
         */

        public static Dictionary<int, List<TableCellViewModel>> buildScheme(DateTime startDate, Scheme scheme)
        {
            SkeamSystemDb db = new SkeamSystemDb();

            Dictionary<int, List<TableCellViewModel>> dic = new Dictionary<int, List<TableCellViewModel>>();

            startDate = CalculateStartDate(startDate);

            DateTime endDate = startDate.AddDays(4);

            IEnumerable<LessonBlock> blocks = scheme.LessonBlocks.Where(l => l.Date >= startDate && l.Date <= endDate);

            foreach (var block in blocks)
            {
                int key = block.BlockNumber;

                if (dic.ContainsKey(key))
                {
                    List<TableCellViewModel> cells = dic[key];

                    cells[((int)block.Date.Date.DayOfWeek) - 1] = new TableCellViewModel()
                    {
                        BlockNumber = block.BlockNumber,
                        Room = block.Room,
                        SubjectName = block.Subject.Name,
                        Teacher = block.Teacher
                    };
                    dic[key] = cells;
                }
                else
                {
                    List<TableCellViewModel> cells = new List<TableCellViewModel>() { 
                        null,
                        null,
                        null,
                        null,
                        null
                    };
                    cells[((int)block.Date.Date.DayOfWeek) - 1] = new TableCellViewModel()
                    {
                        BlockNumber = block.BlockNumber,
                        Room = block.Room,
                        SubjectName = block.Subject.Name,
                        Teacher = block.Teacher
                    };
                    dic.Add(key, cells);
                }
            }
            return dic;
        }

        public static DateTime CalculateStartDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = StartOfWeek(date.AddDays(2), DayOfWeek.Monday);
            }
            else if (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = StartOfWeek(date, DayOfWeek.Monday);
            }
            return date;
        }

        private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}