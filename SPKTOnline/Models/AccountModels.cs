using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SPKTOnline.Management;
using System.ComponentModel;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Web.Mvc;

namespace SPKTOnline.Models
{
    public class AccountModels
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        public User IsUser(string username, string pass)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == pass);
            return user;
        }
    }
    public class ImportModels
    {
        [Required]
        [DisplayName("User name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
    public class UserModels
    {
        [Required]
        [DisplayName("User name")]
       // [Remote("UserNameAvailable","Account", "Username",ErrorMessage="UserName already!")]
        public string Username { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Is Lock")]
        public bool IsLocked { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
      
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Quyền")]
        public String[] MyOption { get; set; }
       
        [Required]
        [DisplayName("Môn Học")]
        public String[] OptionSubject { get; set; }


        [Required]
        [DisplayName("Quyền")]
        public EntityCollection<Role> Roles { get; set; }

        [Required]
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
    }
    public class LogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

}