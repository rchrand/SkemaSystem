using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
    public class EducationViewModel
    {
        public Education Education { get; set; }
        public IEnumerable<Teacher> AvailableTeachers { get; set; }
        public IEnumerable<Teacher> SelectedTeachers { get; set; }
        public PostedTeachers PostedTeachers { get; set; }
    }
}