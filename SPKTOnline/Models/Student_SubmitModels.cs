using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.BussinessLayer;

namespace SPKTOnline.Models
{
    public class Student_SubmitModels
    {
    }
    public partial class Student_Submit
    {
        ICommentBL commentBL;
        OnlineSPKTEntities db;
        public int CommentTotal
        {
            get
            {
                db = new OnlineSPKTEntities();
                commentBL = new CommentBL(db);
                return commentBL.SubmitCommentTotal(this.ID);
            }
        }
        public IEnumerable<Comment> ListComment
        {
            get
            {
                db = new OnlineSPKTEntities();
                commentBL = new CommentBL(db);
                return commentBL.GetCommentForStudentSubmit(this.ID);
            }
        }
        public TrangThaiCham TrangThai
        {
            get { return (Models.TrangThaiCham)TrangThaiCham; }
            set { TrangThaiCham = (int)value; }
        }
        public double TongDiem
        {
            get
            {
                double tong = 0;
                foreach (var tc in this.TestCaseResults)
                    tong += (double)(tc.Score == null ? 0 : tc.Score);
                
                return tong;
            }
        }
        public string KetQuaBienDich
        {
            get
            {
                if (this.TrangThai == Models.TrangThaiCham.ChuaCham)
                    return null;
                if (this.TrangThaiBienDich == (int)Models.TrangThaiBienDich.ThanhCong)
                    return "Thành công";
                return "Lỗi biên dịch";
            }
        }
    }
}