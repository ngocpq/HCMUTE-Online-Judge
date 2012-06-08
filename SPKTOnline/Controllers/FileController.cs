using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.BussinessLayer;

namespace SPKTOnline.Controllers
{
    public class FileController : Controller
    {
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        //
        // GET: /File/
        IFileBL fileBL;
        public FileController()
        {
            fileBL = new FileBL(db);
        }
       
        public ActionResult Download(int? ID)
        {
            File f = fileBL.GetFileByID((int)ID);
            if (f == null)
                Response.Redirect("FileNotFound");
            fileBL.UpdateDownloadCount(f.ID);
            fileBL.SaveChange();
            Response.Clear();
            Response.ContentType = "application/" + f.Type;
            Response.AddHeader("content-disposition", "attachment; filename=\"" + f.Name + "\"");
            Response.BinaryWrite(f.Content.ToArray());
            return null;
        }

        //
        // GET: /File/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /File/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /File/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /File/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /File/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /File/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
