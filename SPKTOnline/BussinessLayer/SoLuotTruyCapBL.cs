using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Management;
using System.IO;

namespace SPKTOnline.BussinessLayer
{
    public interface ISoLuotTruyCapBL
    {
        long Read();
        void Write(long value);
    }
    public class SoLuotTruyCapBL:ISoLuotTruyCapBL
    {
        public long Read()
        {
            SPKTOnline.Models.OnlineSPKTEntities db = new Models.OnlineSPKTEntities();
            SPKTOnline.Models.Parameter para = db.Parameters.FirstOrDefault(p => p.Ma == WebsiteParameters.SO_LUOT_TRUY_CAP);
            if (para == null)
                return 0;
            long kq;
            if (long.TryParse(para.GiaTri, out kq))
                return kq;
            return 0;
        }

        public void Write(long value)
        {
            SPKTOnline.Models.OnlineSPKTEntities db = new Models.OnlineSPKTEntities();
            SPKTOnline.Models.Parameter para = db.Parameters.FirstOrDefault(p => p.Ma == WebsiteParameters.SO_LUOT_TRUY_CAP);
            if (para == null)
            {
                para = new SPKTOnline.Models.Parameter { Ma = WebsiteParameters.SO_LUOT_TRUY_CAP, KieuDuLieu = "System.Int64",  GiaiThich = "Số lượt người truy cập website" };
                db.Parameters.AddObject(para);
            }
            para.GiaTri = value.ToString();
            db.SaveChanges();
        }
    }

    public class SoLuotTruyCapSuDungFileBL : ISoLuotTruyCapBL
    {
        string FilePath;
        public SoLuotTruyCapSuDungFileBL(string countFilePath)
        {
            FilePath = countFilePath;
        }
        public long Read()
        {            
            if (!File.Exists(FilePath))
                return 0;
            System.IO.StreamReader sw = null;
            try
            {
                FileStream fi = File.Open(FilePath, FileMode.Open);
                sw = new System.IO.StreamReader(fi);
                return long.Parse(sw.ReadLine());
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        public void Write(long value)
        {
            System.IO.StreamWriter sw;            
            FileStream fi = File.Open(FilePath, FileMode.Create);
            sw = new System.IO.StreamWriter(fi);
            sw.Write(value.ToString());
            sw.Close();
        }

    }
}