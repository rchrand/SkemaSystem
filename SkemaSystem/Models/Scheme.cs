using SkemaSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class Scheme
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual ClassModel ClassModel { get; set; }

        [Required]
        public virtual Semester Semester { get; set; }

        public virtual List<SubjectDistBlock> SubjectDistBlocks { get; set; }

        public virtual DateTime SemesterStart { get; set; }

        public virtual DateTime SemesterFinish { get; set; }

        public virtual List<LessonBlock> LessonBlocks { get; set; }

        [InverseProperty("ConflictSchemes")]
        public virtual ICollection<Scheme> ParentConflictSchemes { get; set; }

        [InverseProperty("ParentConflictSchemes")]
        public virtual ICollection<Scheme> ConflictSchemes { get; set; }

        //public virtual ICollection<ConflictScheme> ConflictScheme { get; set; }

        public virtual List<SemesterSubjectBlock> OptionalSubjectBlockList { get; set; }

        [NotMapped]
        private string YString;

        public string YearString
        {
            get
            {
                return YString;
            }

            set {
                if (SemesterStart.Month >= 1 && SemesterStart.Month <= 6)
                {
                    YString = "F" + SemesterStart.Year;
                }
                else
                {
                    YString = "E" + SemesterStart.Year;
                }
            }
        }        

        public Scheme()
        {
            SubjectDistBlocks = new List<SubjectDistBlock>();
            YearString = "";

        }

        public List<Subject> NeededSubjects() {
            List<Subject> subjects = new List<Subject>();

            foreach (SemesterSubjectBlock s in Semester.Blocks) 
            {
                subjects.Add(s.Subject);
            }

            return subjects;
        }

        public bool AddLessonBlock(Teacher t, Subject s, int blocksCount)
        {
            // Check whether or not this subject is full already.
            if (!IsSubjectFull(s,blocksCount)) {
                // Check if there are a SubjectDistBlock with this subject AND this teacher already! If there are - add to the blockscount! :-)
                SubjectDistBlock sdb = (from sb in SubjectDistBlocks
                                       where sb.Subject.Equals(s) && sb.Teacher.Equals(t)
                                       select sb).FirstOrDefault();

                if (sdb != null)
                {
                    // Already exists!
                    sdb.BlocksCount += blocksCount;
                }
                else
                {
                    // Create new!
                    SubjectDistBlocks.Add(new SubjectDistBlock { Teacher = t, Subject = s, BlocksCount = blocksCount });
                }
                return true;
            }
            return false;
        }

        public bool IsSubjectFull(Subject s, int tryingToAdd = 1)
        {
            var subjectDistBlocks = from sdb in SubjectDistBlocks
                                    where sdb.Subject.Equals(s)
                                    select sdb;

            int highestBlocksCount = 0;
            if (ClassModel != null)
            {
                highestBlocksCount = Semester.Blocks.SingleOrDefault(x => x.Subject.Equals(s)).BlocksCount;
            }
            else
            {
                highestBlocksCount = OptionalSubjectBlockList.SingleOrDefault(x => x.Subject.Equals(s)).BlocksCount;
            }
            int totalBlocksCount = 0;
            foreach (SubjectDistBlock sdb in subjectDistBlocks)
            {
                totalBlocksCount += sdb.BlocksCount;
            }
            return (totalBlocksCount + tryingToAdd) > highestBlocksCount;
        }


        [NotMapped]
        public TableViewModel TableViewModel { get; set; }
    }
}