using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPKTOnline.BussinessLayer
{
    public interface IParameterBL
    {
        T GetParameterValue<T>(string keyName, T defaultValue);
        void WriteParameterValue<T>(string keyName, T value,bool createParaNeuChuaCo);
        long SoLuotTruyCap { get; set; }
        DateTime NgayBatDauTinhSoLuotTruyCap { get; set; }
    }
    public class ParameterBL:BusinessBase,IParameterBL
    {
        public static string SO_LUOT_TRUY_CAP { get { return "SO_LUOT_TRUY_CAP"; } }
        public static string SO_LUOT_TRUY_CAP_NGAY_BAT_DAU_TINH { get { return "SO_LUOT_TRUY_CAP_STARTDATE"; } }

        public ParameterBL()
            : base(new Models.OnlineSPKTEntities())
        { }
        public T GetParameterValue<T>(string keyName,T defaultValue)
        {
            SPKTOnline.Models.Parameter para = db.Parameters.FirstOrDefault(p => p.Ma == keyName);
            if (para == null)
                return defaultValue;
            Type type = Type.GetType(para.KieuDuLieu);
            return (T)Convert.ChangeType(para.GiaTri, type);            
        }
        public void WriteParameterValue<T>(string keyName, T value, bool createParaNeuChuaCo)
        {
            SPKTOnline.Models.Parameter para = db.Parameters.FirstOrDefault(p => p.Ma == keyName);
            if (para == null && !createParaNeuChuaCo)            
                throw new SPKTOnline.Exceptions.SPKTOnlineException("Tham số không tồn tại: " + keyName);            
            if (para == null)
            {
                para = new SPKTOnline.Models.Parameter { Ma = keyName, KieuDuLieu = typeof(T).FullName, GiaiThich = "" };
                db.Parameters.AddObject(para);
            }
            para.GiaTri = value.ToString();
            db.SaveChanges();
        }
      
        public long SoLuotTruyCap
        {
            get
            {
                return GetParameterValue<long>(SO_LUOT_TRUY_CAP,0);
            }
            set
            {
                WriteParameterValue<long>(SO_LUOT_TRUY_CAP, value,true);
            }
        }
        public DateTime NgayBatDauTinhSoLuotTruyCap
        {
            get
            {
                return GetParameterValue<DateTime>(SO_LUOT_TRUY_CAP_NGAY_BAT_DAU_TINH, DateTime.Now);
            }
            set
            {
                WriteParameterValue<DateTime>(SO_LUOT_TRUY_CAP_NGAY_BAT_DAU_TINH, value, true);
            }
        }
    }
}
