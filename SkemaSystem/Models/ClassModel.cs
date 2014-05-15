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
    }
}