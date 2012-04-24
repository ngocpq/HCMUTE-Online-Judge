using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;

namespace SPKTOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        public ActionResult Index(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        public ActionResult Upload1()
        {
            return View();
        }
        public ActionResult ShowFile()
        {
            File f = db.Files.FirstOrDefault(p => p.ID == 21);
          
            Response.ContentType = "application/ms-word";
            Response.BinaryWrite(f.Content.ToArray());
            return View();
        }
        public string Upload(HttpPostedFileBase fileData)
        {
            var fileName = this.Server.MapPath("~/uploads/" + System.IO.Path.GetFileName(fileData.FileName));
            fileData.SaveAs(fileName);
            //Unzipfile(fileData.InputStream,"~\\uploads");
            return "ok";
        }

    }
}
