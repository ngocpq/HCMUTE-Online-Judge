using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;
using SPKTOnline.Reponsitories;
using SPKTOnline.BussinessLayer;

namespace SPKTOnline.Controllers
{
    public class ClassController : Controller
    {
        //
        // GET: /Class/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        CheckRoles checkRole = new CheckRoles();

        ProblemRepository ProblemRep = new ProblemRepository();
        UserRepository userRep = new UserRepository();
        INotificationBL notificationBL;
        public ClassController()
        {
            notificationBL = new NotificationBL(db);
        }
        public ActionResult Index()
        {
            string currentYear = DateTime.Now.Year.ToString();
            var cl = db.Classes.Where(c => c.SchoolYear.Contains(currentYear) == true);
            List<Class> l = new List<Class>();
            foreach (var i in cl)
            {
                l.Add(i);
            }
            return View(l);
        }

        [Authorize]
        public ActionResult MyClass()
        {
            string username = User.Identity.Name;
            List<Class> l = new List<Class>();
            if (checkRole.IsStudent(username))
            {
                var cl = db.Classes.Where(c => c.User.Username == username);

                foreach (var i in cl)
                {
                    l.Add(i);
                }

            }
            if (checkRole.IsLecturer(username))
            {
                var cl = db.Classes.Where(c => c.LecturerID == username);
                foreach (var i in cl)
                {
                    l.Add(i);
                }

            }
            return View("Index", l);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateClass()
        {
            string username = User.Identity.Name;
            ViewBag.LecturerID = new SelectList(userRep.GetAllLecturer(), "UserName", "UserName");

            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name");
            ViewBag.SchoolYear = new SelectList(new MyClass[]{new MyClass{Value=1, Text="2011-2012"},
                                                    new MyClass{Value=2,Text="2012-2013"},
                                                    new MyClass{Value=3,Text="2013-2014"}}, "Text", "Text");
            ViewBag.Term = new SelectList(new MyClass[]{new MyClass{Value=1, Text="HK I"},
                                                new MyClass{Value=2, Text="HK II"},
                                                new MyClass{Value=3, Text="HK III"}}, "Text", "Text");

            return View();
        }

        [HttpPost]
        #region Don'tneed

        //public ActionResult CreateClass(ClassModels newClassModel)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        string username = User.Identity.Name;
        //        if (username != "")
        //        {
        //            Class newClass = new Class();
        //            newClass.SubjectID = newClassModel.SubjectID;
        //            newClass.Term = newClassModel.Term;
        //            newClass.Group = newClassModel.Group;
        //            newClass.SchoolYear = newClassModel.SchoolYear;
        //            db.Classes.AddObject(newClass);
        //            db.SaveChanges();
        //            return View();
        //        }
        //    }
        //    return RedirectToAction("Logon", "Account");
        //}
        #endregion
        [Authorize(Roles = "Admin")]
        public ActionResult CreateClass(Class newClass)
        {
            string username = User.Identity.Name;

            db.Classes.AddObject(newClass);
            db.SaveChanges();
            ViewBag.LecturerID = new SelectList(userRep.GetAllLecturer(), "UserName", "UserName", newClass.LecturerID);

            ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", newClass.SubjectID);
            ViewBag.SchoolYear = new SelectList(new MyClass[]{new MyClass{Value=1, Text="2011-2012"},
                                                    new MyClass{Value=2,Text="2012-2013"},
                                                    new MyClass{Value=3,Text="2013-2014"}}, "Text", "Text", newClass.SchoolYear);
            ViewBag.Term = new SelectList(new MyClass[]{new MyClass{Value=1, Text="HK I"},
                                                new MyClass{Value=2, Text="HK II"},
                                                new MyClass{Value=3, Text="HK III"}}, "Text", "Text", newClass.Term);
            return View();

        }
        [Authorize(Roles = "Lecturer")]
        public ActionResult ClassDetailOfLecturer(int ID = 0)
        {
            Session["CurrentUrl"] = Request.Url.ToString(); 
            Class cl = db.Classes.FirstOrDefault(c => c.ID == ID);
            return View(cl);
        }

        
        public ActionResult ClassDetail(int ID)
        {
            Session["CurrentUrl"] = Request.Url.ToString(); 
            Class cl = db.Classes.FirstOrDefault(c => c.ID == ID);
            return View(cl);
        }
        public ActionResult DsProblemPartial(Class cla)
        {
            List<Problem> ds = new List<Problem>();
            foreach (var i in db.Problems)
            {
                foreach (var j in i.Classes)
                {
                    if (j.ID == cla.ID)
                        ds.Add(i);
                }
            }
            return PartialView("DSProblemPartial", ds);
        }
        [ChildActionOnly]
        public ActionResult UploadProblemForClass(int classID)
        {
            Class cl = db.Classes.FirstOrDefault(c => c.ID == classID);

            return PartialView();
        }

        public ActionResult ViewNotificationByPartial(int ClassID)
        {
            return PartialView("ViewNotificationPartial", notificationBL.GetNotificationOfClass(ClassID));
        }

    }
}
