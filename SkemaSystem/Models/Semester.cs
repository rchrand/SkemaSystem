using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class Semester
    {
        public int Number { get; set; }
        public Dictionary<Subject, int> Blocks { get; set; }
    }
}