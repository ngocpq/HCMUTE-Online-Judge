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
       
        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult CreateContest(int ID = 0)
        {
            ViewBag.ExamID = new SelectList(db.Exams, "ID", "ID");
            Contest c = new Contest();
          
            c.ExamID = ID;
                
            Exam exam = db.Exams.FirstOrDefault(e => e.ID == ID);
            if (exam != null)
            {
                c.ClassID = exam.ID;
            }
            return View(c);
          
        }
        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult CreateContest(Contest contest)
        {
            Exam exam = db.Exams.FirstOrDefault(e => e.ID ==contest.ExamID);
            contest.ClassID = exam.ClassID;
            db.Contests.AddObject(contest);
            db.SaveChanges();
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "ID",contest.ClassID);
            return View(contest);

        }

    }
}
