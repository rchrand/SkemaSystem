using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkemaSystem.Models;

namespace SkemaSystem.Service
{
    public class Service
    {
        public void setNewSemesterForClass(ClassModel model, Semester semester, DateTime start, DateTime finish)
        {
            Scheme scheme = new Scheme { ClassModel = model, Semester = semester, SemesterStart = start, SemesterFinish = finish };

            model.ActiveSchemes.Add(scheme);
        }
    }
}