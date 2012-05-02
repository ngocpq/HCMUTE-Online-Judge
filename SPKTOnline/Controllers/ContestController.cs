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
        public ActionResult CreateContest(int ID = 0)
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "ID");
            Contest c = new Contest();
            c.ExamID = ID;
            return View(c);


        }
        [HttpPost]
        public ActionResult CreateContest(Contest contest)
        {
            db.Contests.AddObject(contest);
            db.SaveChanges();
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "ID",contest.ClassID);
            return View(contest);

        }

    }
}
