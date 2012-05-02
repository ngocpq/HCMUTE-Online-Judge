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
    public class ClassController : Controller
    {
        //
        // GET: /Class/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();

        ProblemRepository ProblemRep = new ProblemRepository();
        UserRepository userRep = new UserRepository();
        public ActionResult Index()
        {
            var cl = db.Classes.Where(c => c.SchoolYear.Contains(DateTime.Now.Year.ToString()));
            List<Class> l = new List<Class>();
            foreach (var i in cl)
            {
                l.Add(i);
            }
            return View(l);
        }
        public ActionResult MyClass()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (checkRole.IsStudent(username))
                {
                    var cl = db.Classes.Where(c => c.User.Username == username);
                    List<Class> l = new List<Class>();
                    foreach (var i in cl)
                    {
                        l.Add(i);
                    }
                    return View("Index", l);
                }
                if (checkRole.IsLecturer(username))
                {
                    var cl = db.Classes.Where(c => c.User.Username == username);
                    List<Class> l = new List<Class>();
                    foreach (var i in cl)
                    {
                        l.Add(i);
                    }
                    return View("Index", l);
                }
            }
            return RedirectToAction("Logon", "Home");
        }
        public ActionResult CreateClass()
        {
            if(User.Identity.IsAuthenticated)
            {
                string username=User.Identity.Name;
                if(checkRole.IsAdmin(username))
                {
                    ViewBag.LecturerID = new SelectList(userRep.GetAllLecturer(), "UserName", "UserName");

                    ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name");
                    ViewBag.SchoolYear = new SelectList(new MyClass[]{new MyClass{Value=1, Text="2011-2012"},
                                                    new MyClass{Value=2,Text="2012-2013"},
                                                    new MyClass{Value=3,Text="2013-2014"}}, "Text", "Text");
                    ViewBag.Term =  new SelectList(new MyClass[]{new MyClass{Value=1, Text="HK I"},
                                                new MyClass{Value=2, Text="HK II"},
                                                new MyClass{Value=3, Text="HK III"}},"Text","Text");
                                  
                    return View();
                }
            }
            return RedirectToAction("Logon", "Account");
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
        public ActionResult CreateClass(Class newClass)
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (username != "")
                {
                    
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
               
            }
            return RedirectToAction("Logon", "Account");
        }
        public ActionResult ClassDetailOfLecturer(int ID=0)
        {
            Class cl = db.Classes.FirstOrDefault(c => c.ID == ID);
            return View(cl);
        }

        public ActionResult ClassDetail(int ID)
        {
            Class cl = db.Classes.FirstOrDefault(c => c.ID == ID);
            return View(cl);
        }

    }
}
