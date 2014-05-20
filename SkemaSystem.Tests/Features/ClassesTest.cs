using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SkemaSystem.Models;

namespace SkemaSystem.Tests.Features
{
    [TestClass]
    public class ClassesTest
    {

        public List<Teacher> Teachers { get; set; }
        public List<Subject> Subjects { get; set; }
        public Semester Semester { get; set; }

        //public FakeSkemaSystemDb db { get; set; }

        [TestInitialize]
        public void Init() {
            /*db = new FakeSkemaSystemDb();
            db.Teachers.Add(new Teacher { Name = "Hanne" });
            db.Teachers.Add(new Teacher { Name = "Karsten" });
            db.Teachers.Add(new Teacher { Name = "Jorn" });
            db.Teachers.Add(new Teacher { Name = "Kaj" });

            db.Subjects.Add(new Subject { Name = "ITO" });
            db.Subjects.Add(new Subject { Name = "SK" });
            db.Subjects.Add(new Subject { Name = "SD" });
            db.Subjects.Add(new Subject { Name = "CNDS" });

            
            Semester semester = new Semester { Number = 1 };

            Dictionary<Subject, int> blocks = new Dictionary<Subject, int>();
            blocks.Add(Subjects[0], 20);
            blocks.Add(Subjects[1], 40);
            blocks.Add(Subjects[2], 35);
            semester.Blocks = blocks;

            db.Semesters.Add(semester);
            */
            Teachers = new List<Teacher>();
            Subjects = new List<Subject>();

            Subjects.Add(new Subject { Name = "ITO" });
            Subjects.Add(new Subject { Name = "SK" });
            Subjects.Add(new Subject { Name = "SD" });
            Subjects.Add(new Subject { Name = "CNDS" });

            Teachers.Add(new Teacher { Name = "Hanne" });
            Teachers.Add(new Teacher { Name = "Karsten" });
            Teachers.Add(new Teacher { Name = "Jorn" });
            Teachers.Add(new Teacher { Name = "Kaj" });

            Semester = new Semester { Number = 1 };
            
            List<SemesterSubjectBlock> blocks = new List<SemesterSubjectBlock>();
            blocks.Add(new SemesterSubjectBlock { Subject = Subjects[0], BlocksCount = 20 });
            blocks.Add(new SemesterSubjectBlock { Subject = Subjects[1], BlocksCount = 40 });
            blocks.Add(new SemesterSubjectBlock { Subject = Subjects[2], BlocksCount = 35 });

            Semester.Blocks = blocks;
        }

        [TestMethod]
        public void SubjectDistribution()
        {
            // Test creation of Scheme (containing list of: LessonDistBlock) with a Semester (containing: Dictionary<Subject, BlocksCount>, number)
            // Test adding block of LessonDistBlock (containing: Teacher, Subjects, BlocksCount)
            ClassModel aClass = new ClassModel { ClassName = "12t" };

            Scheme scheme = new Scheme { ClassModel = aClass, Semester = Semester};

            Assert.IsTrue(scheme.NeededSubjects().Contains(Subjects[0])); // Scheme should contain the needed subjects from the Semester
            Assert.IsTrue(scheme.NeededSubjects().Contains(Subjects[1]));
            Assert.IsTrue(scheme.NeededSubjects().Contains(Subjects[2]));
            Assert.IsFalse(scheme.NeededSubjects().Contains(Subjects[3])); // But not this one, because this subject is NOT in the given semester!

            Assert.IsTrue(scheme.AddLessonBlock(Teachers[0], Subjects[2], 30));

            Assert.IsFalse(scheme.IsSubjectFull(Subjects[2], 5)); // Is the subject full if I was to try and add an additional 5? Should not be!
            
            Assert.IsTrue(scheme.IsSubjectFull(Subjects[2], 6)); // What I was to try and add 6? Should be!

            Assert.IsTrue(scheme.AddLessonBlock(Teachers[1], Subjects[0], 20));

            Assert.IsFalse(scheme.IsSubjectFull(Subjects[1]));

            Assert.IsTrue(scheme.AddLessonBlock(Teachers[2], Subjects[1], 15));

            Assert.IsFalse(scheme.IsSubjectFull(Subjects[1]));

            Assert.IsTrue(scheme.AddLessonBlock(Teachers[3], Subjects[1], 25));

            Assert.IsTrue(scheme.IsSubjectFull(Subjects[1]));

            Assert.IsFalse(scheme.AddLessonBlock(Teachers[3], Subjects[1], 25)); // This lesson subject should be full - return false!
        }
    }
}
