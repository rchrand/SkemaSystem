using SkemaSystem.Models;
using SkemaSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{

    [RouteArea("Admin", AreaPrefix = "admin")]
    [RoutePrefix("optionalSubjects")]
    [Route("{action=index}/{id?}")]
    public class OptionalSubjectsController : BaseController
    {

        [Route("")]
        public ActionResult Index()
        {
            return View(db.Schemes.Where(x => x.ClassModel == null)); // Show a list of schemes without classes => that means it is optional subjects! (valgfag in danish)
        }

        public ActionResult Create()
        {
            HashSet<string> years = new HashSet<string>();
            foreach (Scheme scheme in db.Schemes)
            {
                years.Add(scheme.YearString);
            }

            ViewBag.Years = years;

            ViewBag.Education = (from e in db.Educations
                                 where e.Name.Equals("DMU")
                                 select e).SingleOrDefault();

            ViewBag.Subjects = from s in db.Subjects
                               where s.OptionalSubject
                               select s;

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)//string semesterId, string name, string[] subjectId, string[] subjectBlockCount, string[] subjectUse)
        {
            if (string.IsNullOrEmpty(form["semester"]) || string.IsNullOrEmpty(form["name"]) || string.IsNullOrEmpty(form["year"]))
            {
                // If any of the required fields are empty!

            }

            string[] subjectBlocksCountArray = form["subjectBlockCount"].Split(',');
            string[] subjectIdArray = form["subjectId"].Split(',');
            string[] subjectUses = form["subjectUse"].Split(',');

            for (int i = 0; i < subjectBlocksCountArray.Count(); i++)
            {
                if (subjectBlocksCountArray[i].Equals("") && subjectUses.Any(x => x.Equals(subjectIdArray[i])))
                {
                    // A chosen subject has no block count value!
                }
            }

            // If no errors occurs - convert the arrays to int arrays, now we know that every index has correct values!
            int[] subjectIds = ConvertStringArraytoInt(subjectIdArray);
            int[] subjectBlockCounts = ConvertStringArraytoInt(subjectBlocksCountArray);

            int semesterId = Int32.Parse(form["semester"]);
            string name = form["name"];
            string year = form["year"];

            Semester semester = db.Semesters.Where(x=> x.Id == semesterId).SingleOrDefault();


            // Get all the subjects chosen along with it's values!
            List<SemesterSubjectBlock> optionalSubjectBlocks = new List<SemesterSubjectBlock>();
            for (int i = 0; i < subjectIds.Count(); i++)
            {
                int subjectId = subjectIds[i];
                if (subjectUses.Contains("" + subjectId))
                {
                    Subject su = db.Subjects.Where(x=>x.Id == subjectId).SingleOrDefault();
                    int blocksCount = subjectBlockCounts[i];
                    // The subject has been chosen from the list, and has to be created!
                    optionalSubjectBlocks.Add(new SemesterSubjectBlock { BlocksCount = blocksCount, Subject = su });
                }
            }

            // Get all schemes from the conflict list, and make sure this new scheme has those schemes in it and that the other schemes has this one in their conflicts-list as well!
            int[] conflictSchemeIds = ConvertStringArraytoInt(form["conflictScheme"].Split(','));
            List<Scheme> conflictSchemes = new List<Scheme>();
            foreach (int schemeId in conflictSchemeIds) {
                Scheme s = db.Schemes.Where(x => x.Id == schemeId).SingleOrDefault();
                conflictSchemes.Add(s);
            }

            // random scheme to get the startdate and enddate of this particular semester!
            // If the randomScheme is empty, then we aren't able to start an optionalSubject! Error message has to be shown!
            Scheme randomScheme = db.Schemes.Where(x => x.YearString.Equals(year)).Where(x => x.Semester.Id == semester.Id).SingleOrDefault();
            if (randomScheme != null)
            {
                Scheme newScheme = new Scheme { Name = name, OptionalSubjectBlockList = optionalSubjectBlocks, Semester = semester, ConflictSchemes = conflictSchemes, SemesterStart = randomScheme.SemesterStart, SemesterFinish = randomScheme.SemesterFinish};

                foreach (Scheme otherScheme in conflictSchemes)
                {
                    otherScheme.ConflictSchemes.Add(newScheme);
                }
                
                db.Schemes.Add(newScheme);
                db.SaveChanges();
            }
            else
            {
                // ERROR: No start and end date!
            }

            return RedirectToAction("Index");
        }

        private int[] ConvertStringArraytoInt(string[] item)
        {
            int[] result = new int[item.Length];
            for (int i = 0; i < item.Length; i++)
            {
                result[i] = (item[i].Equals("")) ? 0 : Int32.Parse(item[i]);
            }
            return result;
        }

        public ActionResult UpdateConflictsWith(string year, string semester)
        {
            int semesterId = Int32.Parse(semester);
            var schemes = from s in db.Schemes
                          where s.Semester.Id == semesterId && s.YearString.Equals(year)
                          select s;

            return PartialView("_ConflictSchemes", schemes);
        }

        public ActionResult CreateOptionalSubject(string name)
        {

            Subject su = new Subject { Name = name, OptionalSubject = true, Education = db.Educations.Where(x => x.Name.Equals("DMU")).SingleOrDefault() };
            db.Subjects.Add(su);
            db.SaveChanges();

            ViewBag.Subjects = from s in db.Subjects
                               where s.OptionalSubject
                               select s;

            return PartialView("_OptionalSubjectsList");
        }
	}
}