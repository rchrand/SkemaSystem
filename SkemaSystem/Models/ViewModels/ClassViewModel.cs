using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Models.ViewModels
{
    public class ClassViewModel
    {
        [Required]
        public ClassModel ClassModel { get; set; }

        [Required]
        public int? Education { get; set; }

        public int? SelectedEducation { get; set; }
    }
}