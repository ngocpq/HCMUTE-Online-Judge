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
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        //CheckRoles checkRole = new CheckRoles();
        ProblemRepository ProblemRep = new ProblemRepository();
        UserRepository userRep = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Lecturer,Admin")]
        public ActionResult CreateExam()
        {
            return View();
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
            string username = User.Identity.Name;
            return View(exam);

        }

    }
}
