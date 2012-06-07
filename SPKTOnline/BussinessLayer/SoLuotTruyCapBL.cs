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
        void Read(out long soLuong,out DateTime ngayBatDau);
        void Write(long value,DateTime ngayBatDau);        
    }
    public class SoLuotTruyCapBL : ISoLuotTruyCapBL
    {
        IParameterBL blParameter=new ParameterBL();
        public void Read(out long soLuong,out DateTime ngayBatDau)
        {
            soLuong= blParameter.SoLuotTruyCap;
            ngayBatDau= blParameter.NgayBatDauTinhSoLuotTruyCap;            
        }
        public void Write(long soLuot,DateTime ngayBatDau)
        {
            blParameter.SoLuotTruyCap = soLuot;
            blParameter.NgayBatDauTinhSoLuotTruyCap = ngayBatDau;
        }
    }

    public class SoLuotTruyCapSuDungFileBL : ISoLuotTruyCapBL
    {
        string FilePath;
        public SoLuotTruyCapSuDungFileBL(string countFilePath)
        {
            FilePath = countFilePath;
        }
        public void Read(out long soLuot,out DateTime ngayBatDau)
        {
            soLuot = 0;
            ngayBatDau = DateTime.Now;
            try
            {                
                String[] buff = File.ReadAllLines(FilePath);
                soLuot= long.Parse(buff[0]);
                ngayBatDau= DateTime.ParseExact(buff[1], "dd/MM/yyyy HH:mm", null);                
            }
            catch(Exception ex)
            {
                LogUtility.WriteLog(ex);                                
            }
        }

        public void Write(long soLuot,DateTime ngayBatDau)
        {
            System.IO.StreamWriter sw = null;
            try
            {                
                FileStream fi = File.Open(FilePath, FileMode.Create);
                sw = new System.IO.StreamWriter(fi);
                sw.WriteLine(soLuot);
                sw.WriteLine(ngayBatDau.ToString("dd/MM/yyyy HH:mm"));
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

    }
}