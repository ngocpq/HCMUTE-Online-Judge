using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SPKTOnline.Models;
using System.Data.Entity;

namespace SPKTOnline.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        AccountModels account = new AccountModels();
        //public User IsUser(string username, string pass)
        //{
        //    var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == pass);
        //    return user;
        //}
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logon(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (account.IsUser(model.UserName, model.Password)!=null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }

            }
            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateUser()
        {
            ViewBag.ListRole = new MultiSelectList(db.ListRole, "ID", "Name");
            List<bool> listIsLock = new List<bool>() { true, false };
            ViewBag.ListLock = new MultiSelectList(listIsLock);
            return View();
        }
        
        //admin
        [Authorize]
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Users.AddObject(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MaPhanQuyen = new SelectList(db.Roles, "MaPhanQuyen", "TenPhanQuyen", user.Roles);
                return View(user);
            }
            else
                return View("Logon");
            
        }
    }
}
