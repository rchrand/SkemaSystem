using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class SubjectDistBlock
    {
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public int BlocksCount { get; set; }
    }
}