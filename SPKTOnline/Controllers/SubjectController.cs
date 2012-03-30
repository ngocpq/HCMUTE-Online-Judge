using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;

namespace SPKTOnline.Controllers
{
    public class SubjectController: Controller
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        public ActionResult Index()
        {
            return View(db.Subjects);
        }
        public ActionResult AddSubject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            db.Subjects.AddObject(subject);
            db.SaveChanges();
            return View("Index");
        }
        public ActionResult EditSubject(string subjectID)
        {
            Subject sb = db.Subjects.FirstOrDefault(s => s.ID == subjectID);
            //return View(sb);
            return RedirectToAction("NotBuilt", "Shared");
        }
    }
}