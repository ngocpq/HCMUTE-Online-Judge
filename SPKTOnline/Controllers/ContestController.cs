using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.BussinessLayer;

namespace SPKTOnline.Controllers
{
    public class ContestController : Controller
    {
        //
        // GET: /Contest/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        IContestBL contestBL;
        public ContestController()
        {
            contestBL = new ContestBL(db);
        }
        public ActionResult Index()
        {
            return View(contestBL.LayDanhSach());
        }

        [Authorize]
        public ActionResult MyContest()
        {
            return View(contestBL.LayDanhSachForStudent(User.Identity.Name));
        }
       
        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult CreateContest(int ID = 0, int classID=0)
        {
            var exams = db.Exams.Where(e => e.LecturerID == User.Identity.Name);
            ViewBag.ExamID = new SelectList(exams, "ID", "ID");
            Contest c = new Contest();
            c.ExamID = ID;
            Exam exam = db.Exams.FirstOrDefault(e => e.ID == ID);
            if (exam == null && classID!=0)
            {
                c.ClassID = classID;
            }
            return View(c);
          
        }
        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult CreateContest(Contest contest)
        {
            Exam exam = db.Exams.FirstOrDefault(e => e.ID == contest.ExamID);
            if(contest.ClassID ==0 )
                contest.ClassID = exam.ClassID;
            db.Connection.Open();
            db.Contests.AddObject(contest);
            db.SaveChanges();
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "ID",contest.ClassID);
            return RedirectToAction("ClassDetailOfLecturer", "Class", new { ID = contest.ClassID });

        }
        public ActionResult ContestDetail(int contestID)
        {
            Contest ct = db.Contests.FirstOrDefault(c => c.ID == contestID);
            return View(ct);
        }
        public ActionResult EditContest(int contestID)
        {
            Contest ct = db.Contests.FirstOrDefault(c => c.ID == contestID);
            return View(ct);
        }
        public ActionResult DeleteContest(int contestID, string URL)
        {
            Contest ct = db.Contests.FirstOrDefault(c => c.ID == contestID);
            ct.IsDeleted = true;
            db.SaveChanges();
            return Redirect(Session["CurrentUrl"].ToString()); 

            //return View(ct);
        }
        
        [Authorize(Roles="Student")]
        public ActionResult RegisterContest(int contestID)
        {
            Contest ct = db.Contests.FirstOrDefault(c => c.ID == contestID);
            if (ct != null)
            {
                string studentID = User.Identity.Name;
                contestBL.ThemSinhVienThi(studentID, contestID);
                 return Redirect(Session["CurrentUrl"].ToString()); 
            }
            else
            {
                ViewBag.Error = "Lổi: không có kỳ thi này";
                return Redirect(Session["CurrentUrl"].ToString()); 
            }
        }

    }
}
