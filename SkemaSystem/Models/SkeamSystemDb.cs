using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace SkemaSystem.Models
{
    
    // Do not remove or rename this class without premission!
    public interface ISkemaSystemDb : IDisposable
    {
        IDbSet<Teacher> Teachers { get; set; }
        IDbSet<ClassModel> Classes { get; set; }
        IDbSet<Education> Educations { get; set; }
        IDbSet<Subject> Subjects { get; set; }
        IDbSet<Semester> Semesters { get; set; }
        IDbSet<Scheme> Schemes { get; set; }
        IDbSet<SemesterSubjectBlock> SemesterSubjectBlocks { get; set; }
        IDbSet<Room> Rooms { get; set; }
        //IDbSet<ConflictScheme> ConflictSchemes { get; set; }

        int SaveChanges();
        //DbEntityEntry Entry(object entity);
        void StateModified(object entity);
    }

    public class SkeamSystemDb : DbContext, ISkemaSystemDb
    {
        public SkeamSystemDb()
            : base("name=skeamsysdb")
        {

        }

        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Education> Educations { get; set; }
        public IDbSet<ClassModel> Classes { get; set; }
        public IDbSet<Subject> Subjects { get; set; }
        public IDbSet<Semester> Semesters { get; set; }
        public IDbSet<Scheme> Schemes { get; set; }
        public IDbSet<SemesterSubjectBlock> SemesterSubjectBlocks { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        //public IDbSet<ConflictScheme> ConflictSchemes { get; set; }

        public void StateModified(object entity) 
        {
            this.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SkeamSystemDb, Configuration>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SkeamSystemDb, Configuration>());
            /*modelBuilder.Entity<Teacher>().HasMany(t => t.Educations).WithMany(e => e.Teachers).Map(m =>
            {
                m.MapLeftKey("TeacherId");
                m.MapRightKey("EducationId");
                m.ToTable("EducationTeachers");
            });
            base.OnModelCreating(modelBuilder);*/
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        
    }
}
