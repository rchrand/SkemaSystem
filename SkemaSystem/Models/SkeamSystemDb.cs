using SkemaSystem.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    // Do not remove or rename this class without premission!
    public class SkeamSystemDb : DbContext
    {
        public SkeamSystemDb() : base("name=skeamsysdb")
        {

        }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SkeamSystemDb, Configuration>());
        }

    }
}