using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPKTOnline.Controllers
{
    public class NotbuildController : Controller
    {
        //
        // GET: /Notbuild/

        public ActionResult Index(string view)
        {
            ViewBag.ViewName = view;
            return View();
        }

    }
}
