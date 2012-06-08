using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;
using SPKTOnline.Management;

namespace SPKTOnline.BussinessLayer
{
    public interface ICommentBL:IBusinessBase
    {
        IEnumerable<Comment> GetCommentForProblem(int ProblemID);
        IEnumerable<Comment> GetCommentForContest(int ContestID);
        IEnumerable<Comment> GetCommentForStudentSubmit(int SubmitID);
        SystemObject GetSystemObject(int type);
        int ProblemCommentTotal(int ProblemID);
        int ContestCommentTotal(int ContestID);
        int SubmitCommentTotal(int SubmitID);
        bool SaveComment(Comment comment);

    }
    public class CommentBL :BusinessBase,ICommentBL
    {
        public CommentBL(OnlineSPKTEntities db):base(db)
        {}
        public IEnumerable<Comment> GetCommentForProblem(int ProblemID)
        {
            SystemObject systemObject=db.SystemObjects.FirstOrDefault(s=>s.Name=="Problems");
            var comments = db.Comments.Where(c => c.SystemObject.SystemObjectID == systemObject.SystemObjectID && c.SystemObjectRecordID == ProblemID);
            return comments;
        }

        public IEnumerable<Comment> GetCommentForContest(int ContestID)
        {
            SystemObject systemObject = db.SystemObjects.FirstOrDefault(s => s.Name == "Contests");
            var comments = db.Comments.Where(c => c.SystemObject.SystemObjectID == systemObject.SystemObjectID && c.SystemObjectRecordID == ContestID);
            return comments;
        }

        public IEnumerable<Comment> GetCommentForStudentSubmit(int SubmitID)
        {
            SystemObject systemObject = db.SystemObjects.FirstOrDefault(s => s.Name == "Student_Submit");
            var comments = db.Comments.Where(c => c.SystemObject.SystemObjectID == systemObject.SystemObjectID && c.SystemObjectRecordID == SubmitID);
            return comments;
        }

        public bool SaveComment(Comment comment)
        {
            try
            {
                BeginChange();
                comment.CreateDate = DateTime.Now;
                db.Comments.AddObject(comment);
                CommitChange();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(ex);
                RollbackChange();
                return false;
            }
        }


        public int ProblemCommentTotal(int ProblemID)
        {
            return db.Comments.Where(c => c.SystemObjectID == (int)EnumCommentType.Problems && c.SystemObjectRecordID == ProblemID).Count();
        }

        public int ContestCommentTotal(int ContestID)
        {
            return db.Comments.Where(c => c.SystemObjectID == (int)EnumCommentType.Contests && c.SystemObjectRecordID == ContestID).Count();
        }

        public int SubmitCommentTotal(int SubmitID)
        {
            return db.Comments.Where(c => c.SystemObjectID == (int)EnumCommentType.Student_Submit && c.SystemObjectRecordID == SubmitID).Count();
        }


        public SystemObject GetSystemObject(int type)
        {
            return db.SystemObjects.FirstOrDefault(s=>s.SystemObjectID==type);
        }
    }
}