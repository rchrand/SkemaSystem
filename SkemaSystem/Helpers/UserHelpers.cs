using SkemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SkemaSystem.Helpers
{
    public static class UserHelpers
    {
        public static string Name(this IPrincipal user)
        {
            return ((Teacher) user).Name;
        }

        public static bool IsTeacher(this IPrincipal user)
        {
            return user.IsInRole("Teacher") || IsAdmin(user);
        }

        public static bool IsAdmin(this IPrincipal user)
        {
            return user.IsInRole("Admin") || IsMaster(user);
        }

        public static bool IsMaster(this IPrincipal user)
        {
            return user.IsInRole("Master");
        }
    }
}