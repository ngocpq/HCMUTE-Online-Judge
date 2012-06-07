using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Management
{
    public class WebContext
    {

        const string SLTruyCap_AppName = "SLTruyCap";
        const string SLOnline_AppName = "SLOnline";
        const string NgayTinh_AppName = "NgayBD";

        public static DateTime NgayBatDauTinhSoLuotTruyCap
        {
            get
            {
                if (HttpContext.Current.Application[NgayTinh_AppName] == null)
                    HttpContext.Current.Application.Add(NgayTinh_AppName, DateTime.Now);
                return (DateTime)HttpContext.Current.Application["NgayBD"];
            }
            set
            {
                HttpContext.Current.Application.Lock();
                if (HttpContext.Current.Application["NgayBD"] == null)
                    HttpContext.Current.Application.Add("NgayBD", value);
                else
                    HttpContext.Current.Application["NgayBD"] = value;
                HttpContext.Current.Application.UnLock();
            }
        }

        public static long SoLuotTruycap
        {
            get
            {
                if (HttpContext.Current.Application[SLTruyCap_AppName] == null)
                    HttpContext.Current.Application.Add(SLTruyCap_AppName, 0L);
                return (long)HttpContext.Current.Application[SLTruyCap_AppName];
            }
            set
            {
                HttpContext.Current.Application.Lock();
                if (HttpContext.Current.Application[SLTruyCap_AppName] == null)
                    HttpContext.Current.Application.Add(SLTruyCap_AppName, value);
                else
                    HttpContext.Current.Application[SLTruyCap_AppName] = value;
                HttpContext.Current.Application.UnLock();
            }
        }
        public static long SoNguoiOnline
        {
            get
            {
                if (HttpContext.Current.Application[SLOnline_AppName] == null)
                    HttpContext.Current.Application.Add(SLOnline_AppName, 0L);
                return (long)HttpContext.Current.Application[SLOnline_AppName];
            }
            set
            {
                HttpContext.Current.Application.Lock();
                if (HttpContext.Current.Application[SLOnline_AppName] == null)
                    HttpContext.Current.Application.Add(SLOnline_AppName, value);
                else
                    HttpContext.Current.Application[SLOnline_AppName] = value;
                HttpContext.Current.Application.UnLock();
            }
        }
        
        
    }
}