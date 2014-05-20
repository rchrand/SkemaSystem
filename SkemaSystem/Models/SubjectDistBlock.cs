using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class SubjectDistBlock
    {
        [Required]
        public int Id { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }
        public int BlocksCount { get; set; }
    }
}