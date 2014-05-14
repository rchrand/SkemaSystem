using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class SkeamSystemDb : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

    }
}