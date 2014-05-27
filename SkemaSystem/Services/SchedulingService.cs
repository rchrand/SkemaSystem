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

        public static Dictionary<int, List<LessonBlock>> buildScheme(DateTime startDate, Scheme scheme)
        {
            SkeamSystemDb db = new SkeamSystemDb();

            Dictionary<int, List<LessonBlock>> dic = new Dictionary<int, List<LessonBlock>>();

            startDate = CalculateStartDate(startDate);

            DateTime endDate = startDate.AddDays(4);

            IEnumerable<LessonBlock> blocks = scheme.LessonBlocks.Where(l => l.Date >= startDate && l.Date <= endDate);

            foreach (var block in blocks)
            {
                int key = block.BlockNumber;

                if (dic.ContainsKey(key))
                {
                    List<LessonBlock> cells = dic[key];

                    cells[((int)block.Date.Date.DayOfWeek) - 1] = block;/*new LessonBlock()
                    {
                        BlockNumber = block.BlockNumber,
                        Room = block.Room,
                        Subject = block.Subject,
                        Teacher = block.Teacher
                    };*/
                    dic[key] = cells;
                }
                else
                {
                    List<LessonBlock> cells = new List<LessonBlock>() { 
                        null,
                        null,
                        null,
                        null,
                        null
                    };
                    cells[((int)block.Date.Date.DayOfWeek) - 1] = block;/*new LessonBlock()
                    {
                        Id = block.Id,
                        BlockNumber = block.BlockNumber,
                        Room = block.Room,
                        Subject = block.Subject,
                        Teacher = block.Teacher,
                        Date = block.Date
                    };*/
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

        public static Boolean IsConflicting(Scheme scheme, LessonBlock lessonBlock, IEnumerable<Room> rooms, IEnumerable<Scheme> schemes)
        {
            if (scheme.LessonBlocks.FirstOrDefault(l => l.Date.Equals(lessonBlock.Date) && l.BlockNumber == lessonBlock.BlockNumber) != null)
            {
                // block already existing on current scheme
                throw new Exception("Der ligger allerede en blok på denne plads.");
                //return true;
            }

            // find blocks in all schemes, where date, blocknumber and teacher match with lessonBlock
            IEnumerable<LessonBlock> blocks = schemes.SelectMany(s => s.LessonBlocks).Where(l => l.Date.Equals(lessonBlock.Date) && l.BlockNumber.Equals(lessonBlock.BlockNumber) && l.Teacher.Id.Equals(lessonBlock.Teacher.Id));
            
            // if we found any block, conflict
            if (blocks.Count() > 0)
            {
                //return true;
                throw new Exception("Underviseren er ikke ledig på det pågældende tidspunkt. (" + schemes.First(s => s.LessonBlocks.Contains(blocks.First())).ClassModel.ClassName + ")");
            }

            /*blocks = schemes.SelectMany(s => s.LessonBlocks).Where(l => l.Date.Equals(lessonBlock.Date) && l.BlockNumber.Equals(lessonBlock.BlockNumber) && l.Room.Id.Equals(lessonBlock.Room.Id));

            // if we found any block, conflict
            if (blocks.Count() > 0)
            {
                //return true;
                throw new Exception("Lokalet er ikke ledigt på det pågældende tidspunkt.");
            }*/

            if (!IsRoomAvailable(schemes, lessonBlock, null))
            {
                throw new Exception("Lokalet er ikke ledigt på det pågældende tidspunkt.");
            }
            return false;
        }

        public static bool IsRoomAvailable(IEnumerable<Scheme> schemes, LessonBlock lessonBlock, Scheme ignoreScheme)
        {
            IEnumerable<Scheme> _schemes;

            if (ignoreScheme != null)
            {
                _schemes = schemes.Where(s => s.Id != ignoreScheme.Id);
            }
            else{
                _schemes = schemes;
            }

            var blocks = _schemes.SelectMany(s => s.LessonBlocks).Where(l => l.Date.Equals(lessonBlock.Date) && l.BlockNumber.Equals(lessonBlock.BlockNumber) && l.Room.Id.Equals(lessonBlock.Room.Id));

            return blocks.Count() == 0;
        }
    }
}