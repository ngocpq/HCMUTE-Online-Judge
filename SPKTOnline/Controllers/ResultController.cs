using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;

namespace SPKTOnline.Controllers
{
    public class ResultController : Controller
    {
        //
        // GET: /Result/

        public ActionResult Index()
        {
            return View();
        }
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        //CheckRoles checkRole = new CheckRoles();
        [ValidateInput(false)]
        [Authorize(Roles = "Student")]
        public ActionResult TryTestAllResult(string Message)
        {
            string StudentName = User.Identity.Name;
            var st = db.Student_Submit.Where(p => p.StudentID == StudentName);
            List<Student_Submit> list = new List<Student_Submit>();
            foreach (var s in st)
            {
                list.Add(s);
            }
            ViewBag.Message = Message;
            return View(list);
        }

        [ValidateInput(false)]        
        [Authorize(Roles = "Student,Lecturer")]
        public ActionResult TryTestResult(int? ID, string Message)
        {
            string Name = User.Identity.Name; 
            Student_Submit st = db.Student_Submit.Where(p => p.ID == ID).FirstOrDefault();
            ViewBag.Message = Message;
            return View(st);

        }

        public ActionResult ContestResult()
        {
            return View();
        }

        public ActionResult LecturerViewTryResult(int? ProblemID)
        {
            var student_submit = db.Student_Submit.Where(p => p.ProblemID == ProblemID);
            List<Student_Submit> list = new List<Student_Submit>();
            foreach (var i in student_submit)
            {
                list.Add(i);
            }

            return View(list);

        }
        [Authorize(Roles = "Lecturer")]
        public ActionResult LecturerViewProblem()
        {
            var listPro = db.Problems.Where(s => s.LecturerID == HttpContext.User.Identity.Name);
            List<Problem> list = new List<Problem>();
            foreach (var i in listPro)
            {
                list.Add(i);
            }

            return View(list);
        }

    }
}
