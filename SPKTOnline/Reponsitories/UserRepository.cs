using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.Reponsitories
{
    public class UserRepository
    {
        OnlineSPKTEntities1 db = new OnlineSPKTEntities1();
        
        public List<User> GetAllLecturer()
        {
            Role r=db.Roles.FirstOrDefault(p=>p.ID==2);
            var l = from i in db.Users
                    where i.Roles.FirstOrDefault(m=>m.ID==2)!=null
                    select i;
            List<User> list = new List<User>();
            foreach (var i in l)
            {
                list.Add(i);
            }
            return list;
        }
    }
}