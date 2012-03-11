﻿using System;
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
        }
    }
}