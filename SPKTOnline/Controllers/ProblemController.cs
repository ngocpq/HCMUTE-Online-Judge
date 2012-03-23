using System.Web;
using System.Web.Mvc;
using SPKTOnline.Management;
using SPKTOnline.Models;
using System.Collections.Generic;
using System;

namespace SPKTOnline.Controllers
{
    public class ProblemController : Controller
    {
        //
        // GET: /Problem/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();

        public ActionResult Index()
        {
            IEnumerable<SPKTOnline.Models.Subject> sb = db.Subjects;
            ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name");
            return View(sb);
        }

        public ActionResult Browse(string ID)
        {
            if (ID != null)
            {
                var ds = db.Problem_Subject;
                List<Problem> dsProblem = new List<Problem>();
                foreach (var s in ds)
                {
                    if (s.SubjectID == ID)
                        dsProblem.Add(s.Problem);
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateProblem()
        {
            string name=HttpContext.User.Identity.Name;
            if(name!=null)
            {
                if (checkRole.IsLecturer(name))
                {
                    ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name");
                    ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name");
                    ViewBag.MaFileSS = new SelectList(db.Comparers, "ID", "Name");

                    //List<SelectListItem> list = new List<SelectListItem>();
                    //SelectListItem item;
                    //foreach (Difficulty dokho in db.Difficulties )
                    //{
                    //    item = new SelectListItem { Text = dokho.Name, Value = dokho.ID.ToString() };
                    //    list.Add(item);
                    //}

                    //ViewBag.MaDoKho = (IEnumerable<SelectListItem>)list; 


                    return View();
                }
                else
                    return RedirectToAction("Index","Home");
            }
             return RedirectToAction("Index","Home");
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateProblem(AddProblemModels problemModel)
        {
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsLecturer(HttpContext.User.Identity.Name))
                {
                    Problem problem = new Problem();
                    problem.Name = problemModel.Name;
                    problem.Content = problemModel.Content;
                    problem.IsHiden = problemModel.IsHiden;
                    problem.DifficultyID = problemModel.DifficultyID;
                    problem.LecturerID = HttpContext.User.Identity.Name;
                    db.Problems.AddObject(problem);
                    Problem_Subject ps = new Problem_Subject();
                    ps.SubjectID = problemModel.SubjectID;
                    ps.ProblemID = problem.ID;
                    ps.DificultLevel = problemModel.DifficultyID;
                    db.Problem_Subject.AddObject(ps);
                    db.SaveChanges();
                    ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name", problem.DifficultyID);
                    return View(problem);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return View("Logon");
        }

    }
}
