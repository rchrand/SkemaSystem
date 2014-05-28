using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class ConflictScheme
    {
        [Required]
        public int Id { get; set; }

        public virtual ICollection<Scheme> scheme { get; set; }
    }
}