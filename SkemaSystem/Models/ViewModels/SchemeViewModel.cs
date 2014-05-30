using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
    public class SchemeViewModel
    {
        public ICollection<TableViewModel> Schemes { get; set; }
        public string Classname { get; set; }
        public int SemesterNumber { get; set; }
        public string Year { get; set; }

        public SchemeViewModel()
        {
            Schemes = new List<TableViewModel>();
        }
    }
}