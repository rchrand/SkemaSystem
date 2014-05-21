using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class SemesterSubjectBlock
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public virtual Subject Subject { get; set; }

        [Required]
        public int BlocksCount { get; set; }
    }
}