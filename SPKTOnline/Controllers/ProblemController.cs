using System.Web;
using System.Web.Mvc;
using SPKTOnline.Management;
using SPKTOnline.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using SPKTOnline.Reponsitories;
using System.Collections;


namespace SPKTOnline.Controllers
{

    public class ProblemController : Controller
    {
        //
        // GET: /Problem/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        CheckRoles checkRole = new CheckRoles();
        ProblemRepository ProblemRep = new ProblemRepository();
        public List<Problem> list;

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
                List<Problem> dsProblem = new List<Problem>();
                var list = db.Problems.Where(p => (p.SubjectID == ID && p.ContestID==null));
                foreach (var i in list)
                {
                    dsProblem.Add(i);
                }
                return View(dsProblem);

            }
            else
                return View("Index", "Home");


        }

        public ActionResult GridviewPartial()
        {
            return PartialView("GridviewPartial", list);
        }
        public ActionResult Details(int ID)
        {
            if (ID > 0)
            {
                Problem p = new Problem();
                foreach (Problem pr in db.Problems)
                {
                    if (pr.ID == ID)
                        p = pr;

                }
                return View(p);
            }
            else
                return View("Index", "Home");


        }
        //Lecturer
        [Authorize(Roles = "Lecturer")]
        public ActionResult AllProblemForUpDate()
        {
            string name = HttpContext.User.Identity.Name;
            List<Problem> dsProblem = new List<Problem>();
            foreach (Problem p in db.Problems)
            {
                if (p.LecturerID == name)
                    dsProblem.Add(p);
            }
            return View(dsProblem);

        }
        [HttpGet]
        [Authorize(Roles="Lecturer")]
        public ActionResult CreateProblem(int ContestID = 0, int ClassID=0)
        {
                    string name = HttpContext.User.Identity.Name;
           
                    ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name");
                    ViewBag.SubjectID = new SelectList(ProblemRep.GetListSubjectByLecturerID(name), "ID", "Name");
                    ViewBag.ComparerID = new SelectList(db.Comparers, "ID", "Name");
                    AddProblemModels pro = new AddProblemModels();
                    pro.ContestID = ContestID;
                    if (ClassID == 0 || ClassID == null)
                    {
                        ViewBag.ClassID = new MultiSelectList(db.Classes, "ID", "SubjectID");

                    }
                    else
                    {
                        Class cl=db.Classes.FirstOrDefault(c=>c.ID==ClassID);
                        pro.cla = cl;
                        pro.SubjectID = cl.SubjectID;
                    }
                    return View(pro);

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
                    problem.SubjectID = problemModel.SubjectID;
                    problem.ComparerID = problemModel.ComparerID;
                    problem.MemoryLimit = problemModel.MemoryLimit;
                    problem.TimeLimit = problemModel.TimeLimit;
                    problem.SubjectID = problemModel.SubjectID;
                    problem.Score = problem.Score;
                    if (problemModel.ContestID > 0)
                    {
                        problem.ContestID = problemModel.ContestID;
                    }
                    else
                        problem.ContestID = null;
                    db.Problems.AddObject(problem);
                    db.SaveChanges();
                    #region AddClass

                    //String[] kqClass = problemModel.ClassID;
                    //if (kqClass.Count() > 0)
                    //{
                    //    foreach (String s in kqClass)
                    //    {
                    //        foreach (Class c in db.Classes)
                    //        {
                    //            if (c.ID == int.Parse(s))
                    //            {
                    //                problem.Classes.Add(c);
                    //            }
                    //        }
                    //    }
                    //} 
                    #endregion
                    db.SaveChanges();
                    ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name", problemModel.DifficultyID);
                    return RedirectToAction("CreateTestCase", "TestCase", new { ProblemID = problem.ID });
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Logon", "Home");
        }

        public ActionResult EditProblem(int ID = 0)
        {
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsLecturer(HttpContext.User.Identity.Name))
                {
                    if (ID > 0)
                    {

                        Problem p;
                        p = db.Problems.FirstOrDefault(m => m.ID == ID);
                        AddProblemModels pm = new AddProblemModels();
                        pm.ID = p.ID;
                        pm.Name = p.Name;
                        pm.TimeLimit = (int)p.TimeLimit;
                        pm.Content = p.Content;
                        pm.MemoryLimit = (int)p.MemoryLimit;
                        ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name", p.DifficultyID);
                        ViewBag.SubjectID = new SelectList(ProblemRep.GetListSubjectByLecturerID(HttpContext.User.Identity.Name), "ID", "Name", p.SubjectID);
                        ViewBag.ComparerID = new SelectList(db.Comparers, "ID", "Name", p.ComparerID);
                        return View(pm);

                    }
                    else
                        return View();
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Logon", "Home");
        }
        [HttpPost]
        [Authorize(Roles = "Lecturer")]
        public ActionResult EditProblem(AddProblemModels problemModel)
        {
            Problem problem = db.Problems.FirstOrDefault(m => m.ID == problemModel.ID);
            problem.Name = problemModel.Name;
            problem.Content = problemModel.Content;
            problem.IsHiden = problemModel.IsHiden;
            problem.DifficultyID = problemModel.DifficultyID;
            problem.LecturerID = HttpContext.User.Identity.Name;
            problem.ComparerID = problemModel.ComparerID;
            problem.MemoryLimit = problemModel.MemoryLimit;
            problem.TimeLimit = problemModel.TimeLimit;
            problem.SubjectID = problemModel.SubjectID;
            problem.Score = problemModel.Score;
            db.SaveChanges();
            return RedirectToAction("AllProblemForUpDate", "Problem");
        }
        [Authorize(Roles = "Lecturer")]
        public ActionResult Delete(int ID)
        {
            return View();
        }


    }
}
