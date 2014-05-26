using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
    public class TableViewModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime StartDate { get; set; }
        public Dictionary<int, List<TableCellViewModel>> TableCells { get; set; }
        public int Week
        {
            get { return GetWeekOfYear(); }
        }
        
        private int GetWeekOfYear()
        {
            DateTime date = StartDate;
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public string AddDay()
        {
            StartDate = StartDate.AddDays(1);
            return "";
        }
    }
    public class TableCellViewModel
    {
        public string SubjectName { get; set; }
        public Teacher Teacher { get; set; }
        public Room Room { get; set; }
        public int BlockNumber { get; set; }
    }
}