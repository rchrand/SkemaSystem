using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
    public class SemesterViewModel
    {
        public Semester semester { get; set; }

        [DataType(DataType.Date)]
        public DateTime start { get; set; }

        [DataType(DataType.Date)]
        public DateTime finish { get; set; }


    }
}