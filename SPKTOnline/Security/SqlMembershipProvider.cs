﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SPKTOnline.Models;
using SPKTOnline.Management.EnCrypt;

namespace SPKTOnline.Security
{
    public class SqlMembershipProvider:MembershipProvider
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();

        string _ApplicationName;
        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false;
            if (!Cryptography.VerifyMD5Hash(oldPassword, user.Password))
                return false;

            user.Password = Cryptography.CreateMD5Hash(newPassword);
            db.SaveChanges();
            return true;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return null;
            MembershipUser us = new MembershipUser(this.Name
                                                    , user.Username
                                                    , user.Username
                                                    , user.Email
                                                    , null
                                                    , null
                                                    , (bool)!user.IsLocked
                                                    , true
                                                    , DateTime.Now
                                                    , DateTime.Now
                                                    , DateTime.Now
                                                    , DateTime.Now
                                                    , DateTime.Now);
            return us;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false;
            if (user.IsLocked != null && user.IsLocked == true)
                return false;
            if (user.IsActived != null && user.IsActived == false)
                return false;
            if (!Cryptography.VerifyMD5Hash(password, user.Password))                
                return false;
            return true;
        }
    }
}