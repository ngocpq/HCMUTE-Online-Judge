using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.BussinessLayer
{

    public interface IContestBL : IBusinessBase
    {
        /// <summary>
        /// Lay tat ca danh sach ky thi
        /// </summary>
        /// <returns>Danh sach ky thi</returns>
        IEnumerable<Contest> LayDanhSach();
        /// <summary>
        /// Lay ky thi theo Ma
        /// </summary>
        /// <param name="maKyThi">ma ky thi</param>
        /// <returns>ky thi</returns>
        Contest LayTheoMa(int maKyThi);
        /// <summary>
        /// Lay danh sach ky thi cua Lop
        /// </summary>
        /// <param name="classID">ma Lop</param>
        /// <returns>Danh sach ky thi</returns>
        IEnumerable<Contest> LayDanhSachKyThiCuaLop(int classID);
        /// <summary>
        /// Xoa ky thi theo Ma
        /// </summary>
        /// <param name="ma">Ma ky thi muon xoa</param>
        void XoaKyThi(int ma);
        /// <summary>
        /// Them sinh vien vao ky thi
        /// </summary>
        /// <param name="mssv"></param>
        /// <param name="contestID"></param>
        void ThemSinhVienThi(string mssv, int contestID);
        /// <summary>
        /// Lay danh sach ky thi cua SinhVien
        /// </summary>
        /// <param name="studentID">Ma sinh vien</param>
        /// <returns>Danh sach cac ky thi cua sinh vien</returns>
        IEnumerable<Contest> LayDanhSachKyThiCuaSinhVien(string studentID);
        IEnumerable<Contest> LayDanhSachKyThiCuaGiaoVien(string LecturerID);
        /// <summary>
        /// Them ky thi cho mot Lop
        /// </summary>
        /// <param name="contest"> ky thi</param>
        /// <param name="classID">ma lop</param>
        void ThemKyThiChoLop(Contest contest, int classID);
        void UpdateScoreForContest(int ContestID, string StudentID);
        bool IsRegisterContest(int ContestID, string Username);
        bool IsLecturerOfClass(int ContestID, string Username);
    }
    public class ContestBL : BusinessBase, IContestBL
    {

        public ContestBL(OnlineSPKTEntities entities)
            : base(entities)
        {

        }
        public IEnumerable<Contest> LayDanhSach()
        {
            return db.Contests.Where(c => !c.IsDeleted);
        }

        public IEnumerable<Contest> LayDanhSachKyThiCuaLop(int classID)
        {
            return db.Contests.Where(c => !c.IsDeleted && c.ClassID == classID);
        }
        public IEnumerable<Contest> LayDanhSachKyThiCuaSinhVien(string studentID)
        {
            User us = db.Users.FirstOrDefault(s => s.Username == studentID);
            return us.Contest_Student.Where(s => s.StudentID == studentID).Select(ct => ct.Contest);
        }

        public void ThemSinhVienThi(string mssv, int contestID)
        {
            try
            {
                BeginChange();
                Contest_Student item = new Contest_Student();
                item.ContestID = contestID;
                item.StudentID = mssv;
                db.Contest_Student.AddObject(item);
                CommitChange();
            }
            catch (Exception ex)
            {
                RollbackChange();
                throw ex;
            }
        }


        public void ThemKyThiChoLop(Contest contest, int classID)
        {
            try
            {
                BeginChange();
                contest.ClassID = classID;
                db.Contests.AddObject(contest);
                CommitChange();
            }
            catch (Exception ex)
            {
                RollbackChange();
                throw ex;
            }
        }
        public Contest LayTheoMa(int maKyThi)
        {
            return db.Contests.Where(c => c.ID == maKyThi && c.IsDeleted == false).FirstOrDefault();
        }

        public void XoaKyThi(int ma)
        {

            try
            {
                BeginChange();
                Contest ct = LayTheoMa(ma);
                if (ct != null)
                    ct.IsDeleted = true;
                CommitChange();
            }
            catch (Exception ex)
            {
                RollbackChange();
                throw ex;
            }
        }


        public void UpdateScoreForContest(int ContestID, string StudentID)
        {
            var varProblems = db.Problems.Where(p => p.ContestID == ContestID);
            List<Student_Submit> student_submits = new List<Student_Submit>();
            Double TongDiem = 0;
            foreach (var i in varProblems)
            {
                 int count = i.Student_Submit.Where(s => s.StudentID == StudentID && s.ContestID == ContestID).Count();
                 if (count != 0)
                 {
                     Double score = i.Student_Submit.Where(s => s.StudentID == StudentID && s.ContestID == ContestID).Max(s => s.TongDiem);
                     TongDiem += score - 0.2 * (count - 1);
                 }
            }
            Contest_Student cs = db.Contest_Student.FirstOrDefault(c => c.ContestID == ContestID && c.StudentID == StudentID);
            cs.Score = TongDiem;
            db.SaveChanges();
        }


        public IEnumerable<Contest> LayDanhSachKyThiCuaGiaoVien(string LecturerID)
        {
            User us = db.Users.FirstOrDefault(s => s.Username == LecturerID);
            return db.Contests.Where(c => c.Class.LecturerID == us.Username);
        }


        public bool IsRegisterContest(int ContestID, string Username)
        {
            Contest_Student cs = db.Contest_Student.FirstOrDefault(p => p.ContestID == ContestID && p.StudentID == Username);
            if (cs != null)
                return true;
            return false;
        }


        public bool IsLecturerOfClass(int ContestID, string Username)
        {
            Contest cs = db.Contests.FirstOrDefault(p => p.ID == ContestID && p.Class.LecturerID == Username);
            if (cs != null)
                return true;
            return false;
        }
    }
}