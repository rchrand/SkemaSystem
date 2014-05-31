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
        public virtual SubjectDistBlock Subject { get; set; }

        //Only the date!
        [Required]
        public DateTime Date { get; set; }

        //The block number of the day! (so, 8:30-10:00 = block 1, 10:30-12:00 = block 2
        [Required]
        public int BlockNumber { get; set; }

        /*[Required]
        public virtual Teacher Teacher { get; set; }*/

        [Required]
        public virtual Room Room { get; set; }
    }
}