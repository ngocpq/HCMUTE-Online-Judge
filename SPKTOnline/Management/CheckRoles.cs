using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.Management
{
    public class CheckRoles
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        public bool IsAdmin(string userName)
        {
            // TODO: Implement this method
          
            User nd = db.Users.FirstOrDefault(n => n.Username == userName);
            if (nd == null)
            {
                return false;
            }
            else
                foreach (Role rl in nd.Roles)
                {
                    if (rl.ID == 1)
                        return true;
                }
            return false;
        }
        public bool IsLecturer(string userName)
        {
            // TODO: Implement this method
       
            User nd = db.Users.FirstOrDefault(n => n.Username == userName);
            if (nd == null)
            {
                return false;
            }
            else
                foreach (Role rl in nd.Roles)
                {
                    if (rl.ID == 2)
                        return true;
                }
            return false;
        }
        public bool IsStudent(string userName)
        {
            // TODO: Implement this method
        
            User nd = db.Users.FirstOrDefault(n => n.Username == userName);
            if (nd == null)
            {
                return false;
            }
            else
                foreach (Role rl in nd.Roles)
                {
                    if (rl.ID == 3)
                        return true;
                }
            return false;
        }
        public User IsUser(string username, string pass)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == pass);
            return user;
        }
    }
}