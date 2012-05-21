using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SPKTOnline;
using SPKTOnline.Models;

namespace SPKTOnline.Security
{
    public class SqlRoleProvider:RoleProvider
    {
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string uName in usernames)
            {
                Models.User user = db.Users.FirstOrDefault(u => u.Username == uName);
                if (user == null)
                    continue;
                foreach (string rName in roleNames)
                {
                    Models.Role role = db.Roles.FirstOrDefault(r => r.Name == rName);
                    if (role == null) continue;
                    //user.Roles.Add(role);
                    role.Users.Add(user);
                }
            }
            db.SaveChanges();
        }

        public override string ApplicationName { get; set; }

        public override void CreateRole(string roleName)
        {
            if (db.Roles.FirstOrDefault(r => r.Name == roleName) != null)
                return;
            Models.Role role = new Models.Role();
            role.Name = roleName;
            db.Roles.AddObject(role);
            db.SaveChanges();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {            
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            return db.Roles.Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            return db.Users.First(u => u.Username == username).Roles.Select(r => r.Name).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return db.Roles.First(r=> r.Name== roleName).Users.Select(u => u.Username).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return db.Roles.First(u => u.Name == roleName).Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower()) != null;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return db.Roles.FirstOrDefault(r => r.Name == roleName) != null;
        }
    }
}