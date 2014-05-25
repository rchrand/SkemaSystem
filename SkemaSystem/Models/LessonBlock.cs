using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class LessonBlock
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Subject Subject { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int BlockNumber { get; set; }

        [Required]
        public Teacher Teacher { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}