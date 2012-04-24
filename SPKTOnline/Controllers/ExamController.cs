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
        CheckRoles checkRole = new CheckRoles();
        ProblemRepository ProblemRep = new ProblemRepository();
        UserRepository userRep = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateExam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateExam(Exam exam)
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (checkRole.IsLecturer(username))
                {
                    exam.LecturerID = username;
                    db.Exams.AddObject(exam);
                    db.SaveChanges();
                    return RedirectToAction("CreateExamCont", "Exam", new { ID = exam.ID });
                }
            }
            return RedirectToAction("Logon", "Home");//Tra ra không có quyền
        }
        public ActionResult CreateExamCont(int ID)
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (checkRole.IsLecturer(username))
                {
                    Exam exam = db.Exams.FirstOrDefault(p => p.ID == ID);
                    return View(exam);
                }
            }
            return RedirectToAction("Logon", "Home");//Tra ra không có quyền
        }
        [HttpPost]
        public ActionResult CreateExamCont(Exam exam)
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (checkRole.IsLecturer(username))
                {
                    
                    return View(exam);
                }
            }
            return RedirectToAction("Logon", "Home");//Tra ra không có quyền
        }

    }
}
