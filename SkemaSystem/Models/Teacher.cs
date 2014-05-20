using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkemaSystem.Models
{
    public class Teacher
    {
        [Required]
        public int TeacherId { get; set; }

        [Required]
        public string Name { get; set; }

        public int EducationID { get; set; }
        public virtual ICollection<Education> Educations { get; set; }

        public Teacher()
        {
            Educations = new HashSet<Education>();
        }
    }
}