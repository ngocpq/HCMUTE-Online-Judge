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
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
        public ActionResult TryTestResult(string Message)
        {
            string StudentName = User.Identity.Name;
            if (User.Identity.IsAuthenticated)
            {
                if (StudentName != null && checkRole.IsStudent(StudentName))
                {
                    var st = db.Student_Summit.Where(p => p.StudentID == StudentName);
                    List<Student_Summit> list = new List<Student_Summit>();
                    foreach (var s in st)
                    {
                        list.Add(s);
                    }
                    ViewBag.Message = Message;
                    return View(list);
                }
                
            }
            return RedirectToAction("Logon", "Account");//trả ra bạn không được xem trang này
        }
        public ActionResult ContestResult()
        {
            return View();
        }
    }
}
