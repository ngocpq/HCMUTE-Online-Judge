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
using System.Data.Objects.DataClasses;
using SPKTOnline.DKMHServices;
using SPKTOnline.Management.EnCrypt;

namespace SPKTOnline.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        CheckRoles checkRole = new CheckRoles();
        string Message;
        public bool UserNameAvailable(string username)
        {
            if (checkRole.UserNameExists(username))
            {
                return false;
            } 
            return true; 
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logon(string message)
        {
            ViewBag.Message = message;
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

        public ActionResult CreateUser(string Message)
        {            
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsAdmin(HttpContext.User.Identity.Name))
                {
                    ViewBag.Roles = new MultiSelectList(db.Roles, "ID", "Name");
                    //ViewBag.Options = new MyClass[] { new MyClass{ Value = 1, Text = "Tèo" }
                    //                        , new MyClass{ Value = 2, Text = "Tí" }
                    //                        , new MyClass{ Value = 3, Text = "Tủn" }
                    //                        , new MyClass{ Value = 4, Text = "Tom" }
                    //                        , new MyClass{ Value = 5, Text = "Chụt" } };
                    ViewBag.Options = new MultiSelectList(db.Roles, "ID", "Name");
                    ViewBag.Subjects = new MultiSelectList(db.Subjects, "ID", "Name");
                    ViewBag.Message = Message;
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
            if (Request.IsAuthenticated)
            {
                if (checkRole.IsAdmin(HttpContext.User.Identity.Name))
                {
                    if (checkRole.UserNameExists(userModel.Username) == false)
                    {
                        String[] kq = userModel.MyOption;
                        user.Username = userModel.Username;
                        user.Password = Cryptography.CreateMD5Hash(userModel.Password);
                        user.LastName = userModel.LastName;
                        user.FirstName = userModel.FirstName;
                        user.IsLocked = userModel.IsLocked;
                        user.Email = userModel.Email;
                        if (kq.Count() != 0)
                        {
                            foreach (String s in kq)
                            {
                                foreach (Role r in db.Roles)
                                {
                                    if (r.ID.ToString() == s)
                                        user.Roles.Add(r);
                                }
                            }
                        }
                        db.Users.AddObject(user);

                        //---------------
                        foreach (Role r in user.Roles)
                        {
                            if (r.ID == 2)
                            {
                                String[] ls = userModel.OptionSubject;
                                if (userModel.OptionSubject.Count() != 0)
                                {
                                    foreach (String str in ls)
                                    {
                                        foreach (Subject s in db.Subjects)
                                        {
                                            if (s.ID == str)
                                                user.Subjects.Add(s);
                                        }
                                    }
                                }

                            }

                        }

                        db.SaveChanges();
                        string Message = "Đã tạo User có tên đăng nhập là: " + user.Username + " thành công";
                        return RedirectToAction("CreateUser", "Account", new { Message = Message });
                    }
                    else
                    {
                        Message = "Username đã tồn tại. Vui lòng nhập lại!";
                        return RedirectToAction("CreateUser", "Account", new { Message = Message }); //TODO: báo ra username đã tồn tại. 
                    }
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

        public ActionResult Import(string message)
        {

            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public ActionResult Import(ImportModels import)
        {
            if (db.Users.FirstOrDefault(m => m.Username == import.Username) == null)
            {
                DKMHServices.UsrSerSoapClient service = new UsrSerSoapClient();
                if (service.ValidateUser(import.Username, import.Password))
                {
                    if (!service.IsStudent(import.Username))
                    {
                        ViewBag.Warning = "Khong phai tai khoan sinh vien";
                        return View("Import");//TODO: bao ra
                    }
                    sinhvien s = service.GetStudentByUserName(import.Username);
                    users u = service.GetUserByUserName(import.Username);
                    //import user
                    SPKTOnline.Models.User user = new Models.User();
                    user.Username = import.Username;
                    user.Password = Cryptography.CreateMD5Hash(import.Password);
                    user.LastName = s.Ho;
                    user.FirstName = s.Ten;
                    if (u.Email != null)
                    {
                        user.Email = u.Email;
                    }
                    else
                        user.Email = user.Username + "@student.hcmute.edu.vn";
                    user.IsActived = true;
                    user.IsLocked = false;
                    db.Users.AddObject(user);
                    user.Roles.Add(db.Roles.FirstOrDefault(m => m.ID == 3));//3=role: student
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    user.LastLoginTime = DateTime.Now;
                    string message = "Bạn đã kích hoạt thành công tài khoản. Chào mừng bạn là thành viên của Thi Lập Trình Online!";
                    return RedirectToAction("Index", "Home", new { Message = message });
                }
                else
                {
                    string message = "Bạn đã nhập sai tên đăng nhập hoặc mật khẩu không đúng! Hảy kiểm tra lại ở trang Đăng ký môn học.";
                    return RedirectToAction("Import", "Account", new { Message = message });//TODO: bao sai
                }
            }
            else
            {
                string Message = "Bạn đã kích hoạt tài khoản trong trang này. Hảy đăng nhập bằng tài khoản bạn đã kích hoạt ở đây!";
                return RedirectToAction("Logon", "Account", new { Message = Message });//TODO: đã kích hoạt tài khoản.

            }
                    
            
        }
        public ActionResult SetPassword(string username)
        {
            User u = db.Users.FirstOrDefault(m => m.Username == username);
            return View(u);
        }
        [HttpPost]
        public ActionResult SetPassword(User user)
        {
            db.Users.Attach(user);

            return View();
        }
             
    }
}


