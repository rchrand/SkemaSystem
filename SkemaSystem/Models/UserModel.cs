using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkemaSystem.Models
{
    public class UserModel 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsValid(string _username, string _password)
        {
            var db = new SkeamSystemDb();

            if (db.Users.Any(u => (u.UserName == _username && u.Password == _password)))
            {
                return true;
            }
            return false;
        }
    }
}