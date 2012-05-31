using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;
using System.Data.Common;

namespace SPKTOnline.BussinessLayer
{
    public interface IBusinessBase
    {        
        /// <summary>
        /// Begin new transaction
        /// </summary>
        void BeginChange();
        /// <summary>
        /// Commit last Transaction
        /// </summary>
        void CommitChange();
        /// <summary>
        /// Rollback last transaction
        /// </summary>
        void RollbackChange();
        /// <summary>
        /// Save data to database
        /// </summary>
        void SaveChange();
    }
    public abstract class BusinessBase:IBusinessBase
    {
        protected OnlineSPKTEntities db;
        protected Stack<DbTransaction> TransactionStack = new Stack<DbTransaction>();
        protected BusinessBase(OnlineSPKTEntities entities)
        {
            db = entities;            
        }
        public void BeginChange()
        {
            if (db.Connection.State == System.Data.ConnectionState.Closed || db.Connection.State == System.Data.ConnectionState.Broken)
                db.Connection.Open();
            DbTransaction trans = db.Connection.BeginTransaction();
            TransactionStack.Push(trans);
        }
        public void CommitChange()
        {
            if (TransactionStack.Count == 0)
                return;
            TransactionStack.Pop().Commit();
        }
        public void RollbackChange()
        {
            if (TransactionStack.Count == 0)
                return;
            TransactionStack.Pop().Rollback();
        }        
        public void SaveChange()
        {
            db.SaveChanges();
        }
        
    }
}