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
        public int GetSubmitCountInProblem(string username, int problemID)
        {
            int count = db.Student_Submit.Where(p => (p.ProblemID == problemID && p.User.Username == username)).Count();
            return count;
        }
        public double GetMaxScoreInProblem(int problemID)
        {
            double max = db.Student_Submit.Where(p => p.ProblemID == problemID).Max(p => p.TongDiem);
            return max;
        }

    }
}