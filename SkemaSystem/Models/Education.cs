using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkemaSystem.Models
{
    public class Education
    {
        [Required]
        public int EducationId { get; set; }

        [Required]
        public string Name { get; set; }

        public int TeacherId { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }

        public Education()
        {
            //Teachers = new HashSet<Teacher>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}