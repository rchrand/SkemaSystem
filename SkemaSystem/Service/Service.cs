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


        /// <summary>
        /// Finds up to 3 days, where mainScheme and conflictSchemes does not conflict
        /// *mainScheme is the main Scheme for a class
        /// *conflictSchemes are the schemes which both class and teacher have
        /// *blocks are the blocks which you want to find a replacement for
        /// </summary>
        /// <param name="mainScheme"></param>
        /// <param name="conflictLessons"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>

        public List<DateTime> FindAHoleInScheme(Scheme mainScheme, List<LessonBlock> conflictLessons, List<LessonBlock> blocks, DateTime Date)
        {
            //List the blocknumbers to be removed
            List<int> blockNumbersToBeMoved = getBlockNumbersToBeMoved(blocks);

            DateTime currentDay = Date;

            //Lists for the mainscheme's lessons and for available blocks to be returned
            List<LessonBlock> occupiedBlocks = getLessonBlocks(mainScheme.LessonBlocks, currentDay);
            List<DateTime> availableBlocks = new List<DateTime>();
            
            int found = 0;
            while (found != 3)
            {
                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    List<LessonBlock> currentBlocks = new List<LessonBlock>();

                    //Gets the lesson blocks of currentDay
                    foreach (LessonBlock item in occupiedBlocks)
                    {
                        if (item.Date == currentDay)
                        {
                            currentBlocks.Add(item);
                        }
                    }

                    if (currentBlocks.Count == 0)
                    {
                        bool valid = true;
                        //Checks for conflicts between mainscheme and other schemes
                        foreach (var item in conflictLessons)
                        {
                            if (item.Date == currentDay)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (valid)
                        {
                            DateTime dt = currentDay;
                            availableBlocks.Add(dt);
                            found++;
                        }
                    }
                }
                currentDay = currentDay.AddDays(1);
                //Stops the while loop if currentDay = 
                if (currentDay == mainScheme.SemesterFinish)
                {
                    found = 3;
                }
            }
            return availableBlocks;
        }

        /// <summary>
        /// Finds up to 3 days, where mainScheme and conflictSchemes does not conflict
        /// *mainScheme is the main Scheme for a class
        /// *conflictSchemes are the schemes which both class and teacher have
        /// *blocks are the blocks which you want to find a replacement for
        /// </summary>
        /// <param name="mainScheme"></param>
        /// <param name="conflictLessons"></param>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public List<DateTime> setLessonBehindOwnLesson(Scheme mainScheme, List<LessonBlock> conflictLessons, List<LessonBlock> blocks, DateTime date)
        {
            //List the blocknumbers to be removed
            List<int> blockNumbersToBeMoved = getBlockNumbersToBeMoved(blocks);

            DateTime currentDay = date;

            //Lists for the mainscheme's lessons and for available blocks to be returned
            List<LessonBlock> occupiedBlocks = getLessonBlocks(mainScheme.LessonBlocks, currentDay);
            List<DateTime> availableBlocks = new List<DateTime>();
            
            int found = 0;
            while (found != 3)
            {
                List<LessonBlock> currentBlocks = new List<LessonBlock>();

                //Gets the blocks with match for currentDay and have the right teacher. If it's the wrong teacher list gets cleared
                foreach (LessonBlock item in occupiedBlocks)
                {
                    if (item.Date == currentDay && item.Teacher == blocks[0].Teacher)
                        currentBlocks.Add(item);
                    else if (item.Date == currentDay && item.Teacher != blocks[0].Teacher)
                        currentBlocks.Clear();
                }

                if (currentBlocks.Count != 0)
                {
                    //Check if total blocks is less or equal with 4
                    if (currentBlocks.OrderBy(x => x.BlockNumber).Last().BlockNumber + blockNumbersToBeMoved.Count <= 4)
                    {
                        List<int> blocksToBeOccupied = new List<int>();

                        //Lists the blocknumbers which is free for moving lessonblocks
                        int z = 1;
                        foreach (var item2 in blockNumbersToBeMoved)
                        {
                            blocksToBeOccupied.Add(currentBlocks.Last().BlockNumber + z);
                            z++;
                        }

                        //Check the blocknumbers which was free for moving lessonblock with the conflict lessons
                        bool valid = true;
                        foreach (var item in conflictLessons)
                        {
                            if (item.Date == currentDay && blocksToBeOccupied.Contains(item.BlockNumber))
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (valid)
                        {
                            DateTime dt = currentDay;
                            availableBlocks.Add(dt);
                            found++;
                        }
                    }
                }
                currentDay = currentDay.AddDays(1);
                if (currentDay == mainScheme.SemesterFinish)
                {
                    found = 3;
                }
            }
            return availableBlocks;
        }

        /// <summary>
        /// Get the blocknumbers of the blocks
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        private List<int> getBlockNumbersToBeMoved(List<LessonBlock> blocks)
        {
            List<int> blockNumbersToBeMoved = new List<int>();
            foreach (LessonBlock item in blocks)
            {
                blockNumbersToBeMoved.Add(item.BlockNumber);
            }
            return blockNumbersToBeMoved;
        }

        /// <summary>
        /// Gets valid lessonblocks which correspond with currentDay and teacher
        /// </summary>
        /// <param name="list"></param>
        /// <param name="currentDay"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        private List<LessonBlock> getLessonBlocks(List<LessonBlock> list, DateTime currentDay)
        {
            List<LessonBlock> freeBloacks = new List<LessonBlock>();

            foreach (LessonBlock item in list)
            {
                if (item.Date >= currentDay)
                    freeBloacks.Add(item);
            }
            return freeBloacks;
        }
    }
}