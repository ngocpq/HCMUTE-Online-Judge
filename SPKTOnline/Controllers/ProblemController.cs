using System.Web;
using System.Web.Mvc;
using SPKTOnline.Management;
using SPKTOnline.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;


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
                return View(dsProblem);

            }
            else
                return View("Index", "Home");

            
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
        public ActionResult AllProblemForUpDate()
        {
             string name=HttpContext.User.Identity.Name;
             if (name != null)
             {
                 if (checkRole.IsLecturer(name))
                 {
                     List<Problem> dsProblem = new List<Problem>();
                     foreach (Problem p in db.Problems)
                     {
                         if (p.LecturerID == name)
                             dsProblem.Add(p);
                     }
                     return View(dsProblem);
                 }
                 else
                     return RedirectToAction("Index", "Home");
             }
             else
                 return RedirectToAction("Logon", "Account");
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
                    ViewBag.ComparerID = new SelectList(db.Comparers, "ID", "Name");

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
                    problem.ComparerID = problemModel.ComparerID;
                    problem.MemoryLimit = problemModel.MemoryLimit;
                    problem.TimeLimit = problemModel.TimeLimit;
                    db.Problems.AddObject(problem);
                    db.SaveChanges();
                    Problem_Subject ps = new Problem_Subject();
                    ps.SubjectID = problemModel.SubjectID;
                    ps.ProblemID = problem.ID;
                    ps.DificultLevel = problemModel.DifficultyID;
                    db.Problem_Subject.AddObject(ps);
                    db.SaveChanges();
                    ViewBag.DifficultyID = new SelectList(db.Difficulties, "DifficultyID", "Name", problemModel.DifficultyID);
                    return RedirectToAction("CreateTestCase", "TestCase", new { ProblemID = problem.ID });
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Logon","Home");
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
                        ViewBag.SubjectID = new SelectList(db.Subjects, "ID", "Name", p.Problem_Subject.FirstOrDefault());
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
        public ActionResult EditProblem(AddProblemModels problemModel)
        {
          if (Request.IsAuthenticated)
            {
                if (checkRole.IsLecturer(HttpContext.User.Identity.Name))
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

                    /*//Subject s = db.Subjects.FirstOrDefault(m => m.ID == problemModel.SubjectID);
                    Problem_Subject ps = problem.Problem_Subject.FirstOrDefault();//= new Problem_Subject();
                    ps.SubjectID = problemModel.SubjectID;
                    ps.Problem = problem;
                    problem.Problem_Subject.Add(ps); //db.Problem_Subject.FirstOrDefault(m => m.ProblemID == problem.ID);
                    ps.SubjectID = problemModel.SubjectID;*/
                    db.SaveChanges();
                    return RedirectToAction("AllProblemForUpDate", "Problem");
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
              return RedirectToAction("Logon", "Home");
        }

    }
}
