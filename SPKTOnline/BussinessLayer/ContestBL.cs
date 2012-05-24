using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.BussinessLayer
{
    public interface IContestBL
    {
        IEnumerable<Contest> LayDanhSach();
        IEnumerable<Contest> LayDanhSach(int classID);
        void ThemSinhVienThi(string mssv,int contestID);
        IEnumerable<Contest> LayDanhSachForStudent(string studentID);
    }
    public class ContestBL:IContestBL
    {
        OnlineSPKTEntities db;
        public ContestBL(OnlineSPKTEntities entity)
        {
            db = entity;
        }
        public IEnumerable<Contest> LayDanhSach()
        {
            return db.Contests.Where(c => !c.IsDeleted);
        }

        public IEnumerable<Contest> LayDanhSach(int classID)
        {
            return db.Contests.Where(c => !c.IsDeleted && c.ClassID==classID);
        }
        public IEnumerable<Contest> LayDanhSachForStudent(string studentID)
        {
            User us = db.Users.FirstOrDefault(s => s.Username == studentID);
            return us.Contest_Student.Where(s => s.StudentID == studentID).Select(ct => ct.Contest);
        }

        public void ThemSinhVienThi(string mssv,int contestID)
        {            
            Contest_Student item = new Contest_Student();
            item.ContestID = contestID;
            item.StudentID = mssv;
            db.Contest_Student.AddObject(item);
            db.SaveChanges();
        }
    }
}