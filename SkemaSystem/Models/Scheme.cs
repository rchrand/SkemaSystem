using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class Scheme
    {
        public ClassModel ClassModel { get; set; }
        public Semester Semester { get; set; }
        public List<SubjectDistBlock> SubjectDistBlocks { get; set; }

        public Scheme()
        {
            SubjectDistBlocks = new List<SubjectDistBlock>();
        }

        public List<Subject> NeededSubjects() {
            List<Subject> subjects = new List<Subject>();

            foreach (Subject s in Semester.Blocks.Keys) {
                subjects.Add(s);
            }

            return subjects;
        }

        public bool AddLessonBlock(Teacher t, Subject s, int blocksCount)
        {
            SubjectDistBlock ldb = new SubjectDistBlock { Teacher = t, Subject = s, BlocksCount = blocksCount };

            // Check whether or not this subject is full already.
            if (!IsSubjectFull(s,blocksCount)) {
                SubjectDistBlocks.Add(ldb);
                return true;
            }
            return false;
        }

        public bool IsSubjectFull(Subject s, int tryingToAdd = 1)
        {
            var subjectDistBlocks = from sdb in SubjectDistBlocks
                                    where sdb.Subject.Equals(s)
                                    select sdb;

            int highestBlocksCount = Semester.Blocks[s];
            int totalBlocksCount = 0;
            foreach (SubjectDistBlock sdb in subjectDistBlocks)
            {
                totalBlocksCount += sdb.BlocksCount;
            }
            Console.WriteLine(subjectDistBlocks.Count());
            return (totalBlocksCount + tryingToAdd) > highestBlocksCount;
        }
    }
}