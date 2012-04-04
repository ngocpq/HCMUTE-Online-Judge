using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPKTOnline.Controllers
{
    public class ContestController : Controller
    {
        //
        // GET: /Contest/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyContest()
        {
            return View();
        }
        public ActionResult CreateContest()
        {
            return View();
        }
    }
}
