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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyClass()
        {
            return View();
        }
        public ActionResult CreateClass()
        {
            if(User.Identity.IsAuthenticated)
            {
                string username=User.Identity.Name;
                if(username!="")
                {
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
        public ActionResult CreateClass(Class newClass)
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (username != "")
                {
                    //Class newClass = new Class();
                    //newClass.SubjectID = newClassModel.SubjectsID;
                    //newClass.Group = newClassModel.Group;
                    //newClass.SchoolYear = newClassModel.SchoolYear;
                    db.Classes.AddObject(newClass);
                    db.SaveChanges();
                    return View();
                }
            }
            return RedirectToAction("Logon", "Account");
        }

    }
}
