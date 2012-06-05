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
    }

}