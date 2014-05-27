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

namespace SkemaSystem.Models
{
    public class SkeamSystemDb : DbContext
    {
        public SkeamSystemDb()
            : base("name=skeamsysdb")
        {

        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<ClassModel> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<SemesterSubjectBlock> SemesterSubjectBlocks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<LessonBlock> LessonBlocks { get; set; }

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

        
    }
}
