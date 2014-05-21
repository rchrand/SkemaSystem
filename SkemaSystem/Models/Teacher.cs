using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SkemaSystem.Models.Enum;

namespace SkemaSystem.Models
{
    public class Teacher
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public UserRoles Role { get; set; }

        public virtual ICollection<Education> Educations { get; set; }

        public bool IsValid(string _username, string _password)
        {
            var db = new SkeamSystemDb();

            if (db.Teachers.Any(u => (u.UserName == _username && u.Password == _password)))
            {
                return true;
            }
            return false;
        }
    }
}