using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
    public class OptionalSubjectViewModel
    {
        [Required]
        public ClassModel classModel { get; set; }

        [Required]
        public Subject subject { get; set; }

    }
}