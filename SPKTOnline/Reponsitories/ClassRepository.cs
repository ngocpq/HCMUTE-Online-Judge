using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.Reponsitories
{
    public class ClassRepository
    {
        OnlineSPKTEntities db = new OnlineSPKTEntities();

        public bool IsInClass(Class cl, string username)
        {
            foreach(var i in cl.Users)
            {
                if (i.Username == username)
                    return true;
            }
            return false;
        }

    }
}