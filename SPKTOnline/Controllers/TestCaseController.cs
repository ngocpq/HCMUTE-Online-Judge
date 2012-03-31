using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using System.Data;
using SPKTOnline.Management;

namespace SPKTOnline.Controllers
{
    public class TestCaseController : Controller
    {
        //
        // GET: /TestCase/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult CreateTestCase(int ProblemID)
        {           
            string name=HttpContext.User.Identity.Name;
            if(name!=null)
            {
                if (checkRole.IsLecturer(name))
                {
                    ViewBag.MaDB = ProblemID;
                    Problem p=db.Problems.FirstOrDefault(m=>m.ID==ProblemID);
                    TestCaseModels testcaseModel = new TestCaseModels();
                    testcaseModel.problem = p;
                    return View(testcaseModel);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult CreateTestCase(TestCaseModels testcaseModel )
        {
            string name=HttpContext.User.Identity.Name;
            if(name!=null)
            {
                if (checkRole.IsLecturer(name))
                {
                    TestCas testcase = new TestCas();
                    testcase.MaDB = testcaseModel.MaDB;
                    testcase.Input = testcaseModel.Input;
                    testcase.Output = testcaseModel.Output;
                    testcase.Diem = testcaseModel.Diem;
                    testcase.MoTa = testcaseModel.MoTa;
                    db.TestCases.AddObject(testcase);
                    db.SaveChanges();
                    return RedirectToAction("CreateTestCase", new { ProblemID = testcase.MaDB });
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");

        }
        public ActionResult EditTestCase(int ID=0)
        {
            string name=HttpContext.User.Identity.Name;
            if(name!=null)
            {
                if (checkRole.IsLecturer(name))
                {
                    if (ID > 0)
                    {
                        TestCas Testcase = db.TestCases.FirstOrDefault(m => m.MaTestCase == ID);
                        return View(Testcase);

                    }
                    else
                        return View();
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [Authorize]
        public ActionResult EditTestCase(TestCas Testcase)
        {
            string name=HttpContext.User.Identity.Name;
            if(name!=null)
            {
                if (checkRole.IsLecturer(name))
                {
                    db.TestCases.Attach(Testcase);
                    db.ObjectStateManager.ChangeObjectState(Testcase, EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("CreateTestCase", new { ProblemID = Testcase.MaDB });
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
