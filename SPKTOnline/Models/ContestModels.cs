using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.BussinessLayer;

namespace SPKTOnline.Models
{
    public partial class Contest
    {
        ICommentBL commentBL;
        OnlineSPKTEntities db;
        public IEnumerable<Comment> ListComment
        {
            get
            {
                db = new OnlineSPKTEntities();
                commentBL = new CommentBL(db);
                return commentBL.GetCommentForContest(this.ID);
            }
        }
        public int RegisterCount
        {
            get
            {
                if (this.Contest_Student != null)
                    return this.Contest_Student.Count();
                else
                    return 0;
            }
        }
        public int SubmitCoDiemToiDaCount
        {
            get
            {
                if (this.Contest_Student != null)
                    return this.Contest_Student.Where(s => s.Score >=this.TotalScore).Count();
                else
                    return 0;
            }
        }
    }

}