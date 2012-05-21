using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using System.IO;

namespace SPKTOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
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
            #region ghifile

            //var csv = "<html><head></head><body><table>...</table></body></html>";
            //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            //byte[] result = encoding.GetBytes(csv);
            //return File(result, "application/vnd.ms-excel", "segnalazioni.csv"); 
            #endregion
            #region Bỏ

            //string filepath = Server.MapPath(@"\Content\HtmlEditor\SampleImportDocument.rtf");
            //string content = string.Empty;

            //try
            //{
            //    using (var stream = new StreamReader(filepath))
            //    {
            //        content = stream.ReadToEnd();
            //    }
            //}
            //catch (Exception exc)
            //{
            //    return Content("Uh oh!");
            //}

            //return Content(content); 
            #endregion

            var f = db.Files.FirstOrDefault(p => p.ID == 21);

            Response.ContentType = "application/ms-word";
            Response.BinaryWrite(f.Content.ToArray());
            return File(f.Content.ToArray(), "application/vnd.ms-word", "segnalazioni.csv"); 

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
