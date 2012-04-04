using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.Management;

namespace SPKTOnline.Controllers
{
    public class Student_SubmitController : Controller
    {
        //
        // GET: /Student_Submit/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TryTest(int ID)
        {
            if (User.Identity.IsAuthenticated == true)
            {
                if (checkRole.IsStudent(User.Identity.Name))
                {
                    Problem p = db.Problems.FirstOrDefault(m => m.ID == ID);
                    Student_Summit st = new Student_Summit();
                    st.Problem = p;
                    return View(st);
                }
            }
            return RedirectToAction("Logon", "Home");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TryTest(Student_Summit st)
        {
            if (User.Identity.IsAuthenticated == true)
            {
                if (checkRole.IsStudent(User.Identity.Name))
                {
                    st.StudentID = User.Identity.Name;
                    st.TrangThaiBienDich = 1;
                    st.TrangThaiCham = 1;
                    st.LanguageID = 1;
                    st.SubmitTime = DateTime.Now;
                    db.Student_Summit.AddObject(st);
                    
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");//trả ra thông tin ở trang kết quả.
                }
            }
            return RedirectToAction("Logon", "Home");
        }

    }
}
