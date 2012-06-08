using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.BussinessLayer
{
    public interface IFileBL
    {
        void UpdateDownloadCount(int FileID);
        File GetFileByID(int FileID);
        bool SaveChange();
    }
    public class FileBL:BusinessBase, IFileBL
    {
        public FileBL(OnlineSPKTEntities db)
            : base(db)
        {
        }
        public void UpdateDownloadCount(int FileID)
        {
            try
            {
                BeginChange();
                File f = GetFileByID(FileID);
                if (f != null)
                    f.DownloadCount++;
                CommitChange();
            }
            catch(Exception ex)
            {
                RollbackChange();
                throw ex;
            }

        }


        public File GetFileByID(int FileID)
        {
            return db.Files.FirstOrDefault(f => f.ID == FileID);
        }


        public new bool SaveChange()
        {
            db.SaveChanges();
            return true;
        }
    }
}