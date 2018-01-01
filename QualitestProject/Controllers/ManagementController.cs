using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;

namespace QualitestProject.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        [HttpPost]
        public string AddNewUser()
        {
            return "this is adduser func";
        }
    }
}