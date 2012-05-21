using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.Reponsitories
{
    public class ProblemRepository
    {
        OnlineSPKTEntities db = new OnlineSPKTEntities();
        public List<Subject> GetListSubjectByLecturerID(string LecturerID)
        {
            List<Subject> list = new List<Subject>();
            foreach (Subject s in db.Subjects)
            {
                if (s.Users.Where(u => u.Username == LecturerID).Count() != 0)
                    list.Add(s);
            }
            return list;
        }
    }
}