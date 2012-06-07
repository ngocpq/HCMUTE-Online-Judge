using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SPKTOnline.Management.MultiLanguage;
using SPKTOnline.Management;
using System.IO;
using System.Reflection;

namespace SPKTOnline
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static string Authorizepath = "";
        public static string Menupath = "";
        //public static string ToeicScorepath = "";
        public static string Englishpath = "";
        public static string VietNamesepath = "";
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        public static Dictionary<string, ILanguage> Language { get; set; }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            //ghi log xuong file
            //LogUtility.SetLogger(new FileLogger());
            LogUtility.SetLogger(new DatabaseLogger());
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Authorizepath = Server.MapPath("\\XML\\Memberships.xml");
            Menupath = Server.MapPath("\\XML\\menu.xml");
            //ToeicScorepath = Server.MapPath("\\XML\\ToeicScore.xml");
            Englishpath = Server.MapPath("\\XML\\English.xml");
            VietNamesepath = Server.MapPath("\\XML\\VietNamese.xml");

            ILanguage English = new English();
            ILanguage VietNamese = new VietNamese();
            MultiLanguage.CreateLanguageKey(English, LanguageEnum.English);
            MultiLanguage.CreateLanguageKey(VietNamese, LanguageEnum.VietNamese);

            Language = new Dictionary<string, ILanguage>();


            WebContext.SoNguoiOnline = 0;
            long soLuot;
            DateTime ngayBatDau;
            DocSoLuotTruyCap(out soLuot,out ngayBatDau);
            WebContext.SoLuotTruycap = soLuot;
            WebContext.NgayBatDauTinhSoLuotTruyCap = ngayBatDau;
            
        }
        
        void Session_Start(object sender, EventArgs e)
        {
            WebContext.SoNguoiOnline++;
            WebContext.SoLuotTruycap++;
            //Lưu số lượt truy cập
            LuuSoLuotTruyCap(WebContext.SoLuotTruycap, WebContext.NgayBatDauTinhSoLuotTruyCap);
        }
        void Session_End(object sender, EventArgs e)
        {
            WebContext.SoNguoiOnline--;
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError();
            try
            {
                string ip = "";
                ip = Request.ServerVariables["REMOTE_ADDR"];
                LogUtility.WriteLog(new Exception("User IP: " + ip, ex));
            }
            catch
            {
                Response.Redirect("~/Error");
            }
        }
        void Application_End(object sender, EventArgs e)
        {
            try
            {
                string message = ": Application_End. \r\n";
                // LogShutdown Event
                #region LogShutdown Event

                HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime",
                                                                                            BindingFlags.NonPublic
                                                                                            | BindingFlags.Static
                                                                                            | BindingFlags.GetField,
                                                                                            null,
                                                                                            null,
                                                                                            null);
                if (runtime == null)
                    return;
                string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                                                                                 BindingFlags.NonPublic
                                                                                 | BindingFlags.Instance
                                                                                 | BindingFlags.GetField,
                                                                                 null,
                                                                                 runtime,
                                                                                 null);
                string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                                                                               BindingFlags.NonPublic
                                                                               | BindingFlags.Instance
                                                                               | BindingFlags.GetField,
                                                                               null,
                                                                               runtime,
                                                                               null);
                message += String.Format("\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}\r\n",
                                             shutDownMessage,
                                             shutDownStack);

                #endregion
                LogUtility.WriteLog(message);
                
            }
            catch (Exception ex)
            {
                LogUtility.WriteLog(new Exception("Application_End error", ex));
            }

        }

        const string _CounterFileName = "SLTruyCap.txt";
        string CounterFilePath
        {
            get { return Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, _CounterFileName); }
        }
        private void LuuSoLuotTruyCap(long soLuot, DateTime ngayBatDau)
        {
            
            SPKTOnline.BussinessLayer.ISoLuotTruyCapBL bl= new SPKTOnline.BussinessLayer.SoLuotTruyCapSuDungFileBL(CounterFilePath);
            bl.Write(soLuot, ngayBatDau);
        }
        private void DocSoLuotTruyCap(out long soLuot, out DateTime ngayBatDau)
        {
            SPKTOnline.BussinessLayer.ISoLuotTruyCapBL bl = new SPKTOnline.BussinessLayer.SoLuotTruyCapSuDungFileBL(CounterFilePath);
            bl.Read(out soLuot, out ngayBatDau);
        }
        
    }
}