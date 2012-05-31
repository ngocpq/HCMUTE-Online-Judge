using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SPKTOnline.Management.MultiLanguage;
using SPKTOnline.Management;

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


            Application.Lock();
            Application.Add("SLOnline", 1);
            System.IO.StreamReader sr;
            sr = new System.IO.StreamReader(Server.MapPath("SLTruyCap.txt"));
            string S = sr.ReadLine();
            sr.Close();
            Application.UnLock();
            Application.Add("SLTruyCap", S);
        }
        void Session_Start(object sender, EventArgs e)
        {

            Application["SLOnline"] = (int)Application["SLOnline"] + 1;
            Application.Contents["SLTruyCap"] =
            int.Parse(Application.Contents["SLTruyCap"].ToString()) + 1;

            System.IO.StreamWriter sw;
            sw = new System.IO.StreamWriter(Server.MapPath("SLTruyCap.txt"));
            sw.Write(Application.Contents["SLTruyCap"].ToString());
            sw.Close();
        }
        void Session_End(object sender, EventArgs e)
        {
            Application["SLOnline"] = (int)Application["SLOnline"] - 1;
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = Server.GetLastError();
            try
            {
                string ip = "";
                ip = Request.ServerVariables["REMOTE_ADDR"];
                //WriteToEventLog(new Exception("User IP: " + ip, ex));
                LogUtility.WriteLog(new Exception("User IP: " + ip, ex));
            }
            catch
            {
                Response.Redirect("~/Error");
            }
        }        
    }
}