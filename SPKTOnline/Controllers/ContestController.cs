using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;

namespace SPKTOnline.Controllers
{
    public class ContestController : Controller
    {
        //
        // GET: /Contest/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyContest()
        {
            return View();
        }
        public ActionResult CreateContest(int ID=0)
        {
            if (ID > 0)
            {
                Contest c = new Contest();
                c.ExamID = ID;
                return View(c);
            }
            return View();
        }
    }
}
