﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SPKTOnline.Management;
using System.ComponentModel;
using System.Data.Objects;

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

    public  class UserModels
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

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
        public string ConfirmPassword { get; set; }


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