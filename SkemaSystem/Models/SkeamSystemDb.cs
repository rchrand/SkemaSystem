using SkemaSystem.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    // Do not remove or rename this class without premission!
    public interface ISkemaSystemDb : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
    }

    public class SkeamSystemDb : DbContext, ISkemaSystemDb
    {
        public SkeamSystemDb() : base("name=skeamsysdb")
        {

        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SkeamSystemDb, Configuration>());
        }

        IQueryable<T> ISkemaSystemDb.Query<T>()
        {
            return Set<T>();
        }

            
        
    }
}