using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;
using SPKTOnline.Reponsitories;

namespace SPKTOnline.Controllers
{
    public class ExamController : Controller
    {
        //
        // GET: /Exam/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        //CheckRoles checkRole = new CheckRoles();
        ProblemRepository ProblemRep = new ProblemRepository();
        UserRepository userRep = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult CreateExam(int classID=0)
        {
            Class c = db.Classes.FirstOrDefault(cl => cl.ID == classID);
            Exam ex = new Exam();
            //ex.Class = c;//Da bo exam
            ViewBag.ClassID = classID;
            return View(ex);
        }
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public ActionResult CreateExam(Exam exam)
        {
            string username = User.Identity.Name;
            exam.LecturerID = username;
            db.Exams.AddObject(exam);
            db.SaveChanges();
            return RedirectToAction("CreateExamCont", "Exam", new { ID = exam.ID });

        }

        [Authorize(Roles = "Lecturer")]
        public ActionResult CreateExamCont(int ID)
        {
            string username = User.Identity.Name;
            Exam exam = db.Exams.FirstOrDefault(p => p.ID == ID);
            return View(exam);
        }
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public ActionResult CreateExamCont(Exam exam)
        {
            Exam ex = db.Exams.FirstOrDefault(p => p.ID == exam.ID);
            string username = User.Identity.Name;
            return RedirectToAction("ClassDetailOfLecturer", "Class", new { ID = ex.ClassID });

        }

        public ActionResult ExamDetail(int examID)
        {
            Exam ex = db.Exams.FirstOrDefault(e => e.ID == examID);
            return View(ex);
        }
        public ActionResult EditExam(int examID)
        {
            Exam ex = db.Exams.FirstOrDefault(e => e.ID == examID);
            return View(ex);
        }
        public ActionResult DeleteExam(int examID)
        {
            Exam ex = db.Exams.FirstOrDefault(e => e.ID == examID);
            return View(ex);
        }

    }
}
