using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QualitestProject.Models;

namespace QualitestProject.Helpers
{
    public class UserLoginSecurity
    {
        public static bool LogIn(string username, string password)
        {
            using (QualitestProjDBEntities userEntities = new QualitestProjDBEntities())
            {
                return userEntities.UsersTables.Any(user => user.Username.Equals(username,
                    StringComparison.OrdinalIgnoreCase) &&
                user.Password == password);
            }
        }//show user by its id
    }
}