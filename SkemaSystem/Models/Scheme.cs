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
        [Required]
        public int Id { get; set; }
        
        [Required]
        public virtual ClassModel ClassModel { get; set; }

        [Required]
        public virtual Semester Semester { get; set; }

        public virtual List<SubjectDistBlock> SubjectDistBlocks { get; set; }

        // public virtual List<LessonBlock> LessonBlocks { get; set; }

        public Scheme()
        {
            SubjectDistBlocks = new List<SubjectDistBlock>();
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

            int highestBlocksCount = Semester.Blocks.SingleOrDefault(x => x.Subject.Equals(s)).BlocksCount;
            int totalBlocksCount = 0;
            foreach (SubjectDistBlock sdb in subjectDistBlocks)
            {
                totalBlocksCount += sdb.BlocksCount;
            }
            Console.WriteLine(subjectDistBlocks.Count());
            return (totalBlocksCount + tryingToAdd) > highestBlocksCount;
        }


        [NotMapped]
        public TableViewModel TableViewModel { get; set; }
    }
}