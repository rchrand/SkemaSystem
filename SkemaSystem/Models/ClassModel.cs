using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkemaSystem.Models
{
    public class ClassModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ClassName { get; set; }

        public virtual List<Scheme> ActiveSchemes { get; set; }

        public ClassModel()
        {
            if (ActiveSchemes == null)
                ActiveSchemes = new List<Scheme>();
        }

        public bool CreateNewSemester() { // TODODODODODODODODODODODODODODODODODODODODODO
            var scheme = from s in ActiveSchemes
                         orderby s.Semester.Number descending
                         select s;
            Scheme theScheme = scheme.FirstOrDefault();

            if (CanCreateNewSemester())
            {
                ActiveSchemes.Add(new Scheme { Semester = new Semester { Number = 2 }, ClassModel = this });
                return true;
            }
            return false;
        }

        public bool CanCreateNewSemester()
        {
            var scheme = from s in ActiveSchemes
                         orderby s.Semester.Number descending
                         select s;
            Scheme theScheme = scheme.FirstOrDefault();

            return true;
        }
    }
}