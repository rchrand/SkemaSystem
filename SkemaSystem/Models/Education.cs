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
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Teacher> Teachers { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}