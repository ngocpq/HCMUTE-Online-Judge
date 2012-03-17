using System.Web;
using System.Web.Mvc;
using SPKTOnline.Management;
using SPKTOnline.Models;

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
                    ViewBag.MaMonHoc = new SelectList(db.Subjects, "ID", "Name");
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
        public ActionResult CreateProblem(Problem problem)
        {
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsLecturer(HttpContext.User.Identity.Name))
                {
                    problem.LecturerID = HttpContext.User.Identity.Name;
                    db.Problems.AddObject(problem);
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
