using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SkemaSystem.Models.Enum;
using System.Security.Principal;
using SkemaSystem.Models.Interfaces;

namespace SkemaSystem.Models
{
    // http://stackoverflow.com/questions/1064271/asp-net-mvc-set-custom-iidentity-or-iprincipal
    // http://www.codeproject.com/Articles/578374/AplusBeginner-splusTutorialplusonplusCustomplusF

    public class Teacher : ITeacherPrincipal
    {
        public IIdentity Identity { get; /*private*/ set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public UserRoles Role { get; set; }

        public virtual ICollection<Education> Educations { get; set; }

        public Teacher()
        {
            //this.Identity = new GenericIdentity(Username);
        }

        public bool IsValid(string _username, string _password)
        {
            var db = new SkeamSystemDb();

            if (db.Teachers.Any(u => (u.Username == _username && u.Password == _password)))
            {
                return true;
            }
            return false;
        }

        public bool IsInRole(string role)
        {
            return Role.ToString() == role;
        }
    }
}