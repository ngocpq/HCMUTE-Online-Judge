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
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        CheckRoles checkRole = new CheckRoles();
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
                if (Membership.ValidateUser(model.UserName, model.Password))
                {

                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    var user = db.Users.FirstOrDefault(u => u.Username == model.UserName);
                    user.LastLoginTime = DateTime.Now;
                    db.SaveChanges();
                    FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
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

        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser(string Message)
        {
            ViewBag.Roles = new MultiSelectList(db.Roles, "ID", "Name");
            ViewBag.Options = new MultiSelectList(db.Roles, "ID", "Name");
            ViewBag.Subjects = new MultiSelectList(db.Subjects, "ID", "Name");
            ViewBag.Message = Message;
            return View();
        }

        [Authorize]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser(UserModels userModel)
        {
            User user = new User();

            if (Membership.GetUser(userModel.Username) == null)
            {
                String[] kq = userModel.MyOption;
                user.Username = userModel.Username;
                user.Password = Cryptography.CreateMD5Hash(userModel.Password);
                user.LastName = userModel.LastName;
                user.FirstName = userModel.FirstName;
                user.IsLocked = userModel.IsLocked;
                user.Email = userModel.Email;
                user.CreateDate = DateTime.Now;
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
                return RedirectToAction("Index", "Home", new { Message = Message });
            }
            else
            {
                string Message = "Username đã tồn tại. Vui lòng nhập lại!";
                return RedirectToAction("CreateUser", "Account", new { Message = Message }); //TODO: báo ra username đã tồn tại. 
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditUser(User user)
        {


            db.Users.Attach(user);
            db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
            db.SaveChanges();
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ManageRoles()
        {
            return View();
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
                    user.CreateDate = DateTime.Now;
                    user.LastLoginTime = DateTime.Now;
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

                    ViewBag.msg = "demo";
                    return RedirectToAction("EditAccount", "Account", new { Message = message });
                }
                else
                {
                    string message = "Bạn đã nhập sai tên đăng nhập hoặc mật khẩu không đúng! Hảy kiểm tra lại ở trang Đăng ký môn học.";
                    return RedirectToAction("Import", "Account", new { Message = message });//TODO: bao sai
                }
            }
            else
            {
                string Message = "Bạn đã kích hoạt tài khoản " + import.Username + " trong trang này. Hảy đăng nhập bằng tài khoản bạn đã kích hoạt ở đây!";
                return RedirectToAction("Logon", "Account", new { Message = Message });//TODO: đã kích hoạt tài khoản.

            }
        }

        [ValidateInput(false)]
        public ActionResult EditAccount(string message)
        {
            ViewBag.Message = message;
            if (User.Identity.IsAuthenticated)
            {
                User u = db.Users.FirstOrDefault(m => m.Username == User.Identity.Name);
                EditAccountModel account = new EditAccountModel();
                account.Username = u.Username;
                account.LastName = u.LastName;
                account.FirstName = u.FirstName;
                account.Email = u.Email;
                return View(account);
            }
            else
                return RedirectToAction("Logon", "Account");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditAccount(EditAccountModel EAccount)
        {
            User u = db.Users.FirstOrDefault(m => m.Username == EAccount.Username);
            u.LastName = EAccount.LastName;
            u.FirstName = EAccount.FirstName;
            u.Email = EAccount.Email;
            db.SaveChanges();
            if (u.Password == Cryptography.CreateMD5Hash(EAccount.Password))
            {
                u.Password = Cryptography.CreateMD5Hash(EAccount.NewPassword);
                db.SaveChanges();
                return RedirectToAction("EditAccount", "Account", new { Message = "Bạn đã thay đổi toàn khoản thành công!" });
            }
            else
                return RedirectToAction("EditAccount", "Account", new { Message = "Password nhập vào không đúng!" });

        }

    }
}


