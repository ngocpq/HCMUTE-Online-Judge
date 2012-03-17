using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SPKTOnline.Models;
using System.Data.Entity;
using SPKTOnline.Management;
using System.Data;

namespace SPKTOnline.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
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
                User user = new User();
                user = checkRole.IsUser(model.UserName, model.Password);
                if (user!=null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        user.LastLoginTime = DateTime.Now;
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        user.LastLoginTime = DateTime.Now;
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
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsAdmin(HttpContext.User.Identity.Name))
                {
                    ViewBag.ListRole = new MultiSelectList(db.Roles, "ID", "Name");
                    return View();
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return View("Logon");
        }
        
        //admin
        [Authorize]
        [HttpPost]
        public ActionResult CreateUser(UserModels userModel)
        {                        
            
            User user = new User();
            Student st = new Student();
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsAdmin(HttpContext.User.Identity.Name))
                {
                    user.Username = userModel.Username;
                    user.Password = userModel.Password;
                    user.LastName = userModel.LastName;
                    user.FirstName = userModel.FirstName;
                    user.IsLocked = userModel.IsLocked;
                    user.Email = userModel.Email;
                    user.Roles = userModel.MyRole;

                    //ViewBag.Checks = MyCheckListRole;
                    db.Users.AddObject(user);
                    db.SaveChanges();
                    return View(userModel);
                }
                else
                    return RedirectToAction("Index", "Home");
                //ViewBag.MaPhanQuyen = new SelectList(db.Roles, "MaPhanQuyen", "TenPhanQuyen", user.Roles);
                //return View(userModel);
            }
            else
                return View("Logon");
            
        }
        public ActionResult EditUser()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditUser(User user)
        {
  
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsAdmin(HttpContext.User.Identity.Name))
                {

                    db.Users.Attach(user);
                    db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                    db.SaveChanges();
                    return View(user);
                }
                else
                    return RedirectToAction("Index", "Home");
                //ViewBag.MaPhanQuyen = new SelectList(db.Roles, "MaPhanQuyen", "TenPhanQuyen", user.Roles);
                //return View(userModel);
            }
            else
                return View("Logon");
        }
             
    }
}


