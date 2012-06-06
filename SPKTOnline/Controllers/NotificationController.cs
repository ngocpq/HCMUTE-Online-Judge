using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPKTOnline.Models;
using SPKTOnline.BussinessLayer;

namespace SPKTOnline.Controllers
{
    public class NotificationController : Controller
    {
        //
        // GET: /Notification/
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        INotificationBL notificationBL;
        public NotificationController()
        {
            notificationBL = new NotificationBL(db);
        }
        public ActionResult Index()
        {
            return View(notificationBL.GetAllNotification());
        }
        public ActionResult Details(int NotifiID)
        {
            Notification noti=notificationBL.GetNotificationByID(NotifiID);
            if (noti != null)
                return View(noti);
            else
                return View("Lỗi");

        }
        [Authorize(Roles="Lecturer,Admin")]
        public ActionResult CreateNotification(int ClassID=0)
        {
            Notification noti = new Notification();
            noti.ClassID = ClassID;
            return View(noti);
        }
        [HttpPost]
        [Authorize(Roles = "Lecturer,Admin")]
        [ValidateInput(false)]
        public ActionResult CreateNotification(Notification noti)
        {
            if (noti.ClassID == 0)
                noti.ClassID = null;
            noti.CreateDate = DateTime.Now;
            noti.UserNameID = User.Identity.Name;
            notificationBL.SaveNotification(noti);
            db.SaveChanges();
            if(noti.ClassID!=null)
                return Redirect(Session["CurrentUrl"].ToString());
            return RedirectToAction("Index","Notification");
        }

    }
}
