using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Management;

namespace SPKTOnline.Controllers
{
    public class ProblemController : Controller
    {
        //
        // GET: /Problem/
        CheckRoles checkRole = new CheckRoles();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateProblem()
        {
            if (checkRole.IsLecturer(HttpContext.User.Identity.Name))
            {
                
                return View();
            }
            else
                return RedirectToAction("Index","Home");
        }

    }
}
