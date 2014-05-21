using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
    public class TeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Education> AvailableEducations { get; set; }
        public IEnumerable<Education> SelectedEducations { get; set; }
        public PostedEducations PostedEducations { get; set; }
    }

    public class PostedEducations
    {
        public string[] EducationIds { get; set; }
    }
}