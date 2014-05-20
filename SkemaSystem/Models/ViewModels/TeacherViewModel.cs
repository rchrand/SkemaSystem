using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models.ViewModels
{
        public class TeacherViewModel
        {
            public Teacher Teacher { get; set; }
            public IEnumerable<Education> AvailableEducations { get; set; }
            public IEnumerable<Education> SelectedEducations { get; set; }
            public PostedEducations PostedEducations { get; set; }

        private TeacherViewModel GetFruitsModel(Teacher teacher, PostedEducations postedEducations, ISkemaSystemDb db)
        {
            // setup properties
            var model = new TeacherViewModel();
            var selectedEducations = new List<Education>();
            var postedEducationIds = new string[0];
            if (postedEducations == null) postedEducations = new PostedEducations();

            // if a view model array of posted fruits ids exists
            // and is not empty,save selected ids
            if (postedEducations.EducationIds != null && postedEducations.EducationIds.Any())
            {
                postedEducationIds = postedEducations.EducationIds;
            }

            // if there are any selected ids saved, create a list of fruits
            if (postedEducationIds.Any())
            {
                IEnumerable<Education> educations = db.Educations;
                selectedEducations = /*FruitRepository.GetAll(db)*/educations
                 .Where(x => postedEducationIds.Any(s => x.EducationId.ToString().Equals(s)))
                 .ToList();
            }

            //setup a view model
            model.Teacher = teacher;
            model.AvailableEducations = db.Educations;//FruitRepository.GetAll(db).ToList();
            model.SelectedEducations = selectedEducations;
            model.PostedEducations = postedEducations;

            return model;
        }

        /// <summary>
        /// for setup initial view model for all fruits
        /// </summary>
        private TeacherViewModel GetFruitsInitialModel(Teacher teacher)
        {
            //setup properties
            var model = new TeacherViewModel();
            var selectedEducations = teacher.Educations;// new List<Education>();

            //setup a view model
            model.Teacher = teacher;
            model.AvailableEducations = db.Educations;//FruitRepository.GetAll(db).ToList();
            model.SelectedEducations = selectedEducations;

            return model;
        }
        }

        public class PostedEducations
        {
            //this array will be used to POST values from the form to the controller
            public string[] EducationIds { get; set; }
        }

    }