using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.IO;
using System.Diagnostics;
using System.Web.Configuration;

namespace SPKTOnline.Management
{
    public interface ILogger
    {
        void WriteLog(string message);
        void WriteLog(Exception ex);
    }


    public class DatabaseLogger : ILogger
    {
        void Write(string message, string logType)
        {
            SPKTOnline.Models.OnlineSPKTEntities db = new Models.OnlineSPKTEntities();
            SPKTOnline.Models.Logger log = new Models.Logger();
            log.Time = DateTime.Now;
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.User != null)
                log.UserName = System.Web.HttpContext.Current.User.Identity.Name;            
            log.Message = message;
            log.Type = logType;
            db.Loggers.AddObject(log);
            db.SaveChanges();

        }
        public void WriteLog(string message)
        {
            try
            {
                Write(message, "LOG");
            }
            catch
            {
                FileLogger logger = new FileLogger();
                logger.WriteLog(message);
            }
        }

        public void WriteLog(Exception e)
        {
            try
            {
                string logType = e.GetType().FullName;
                Exception temp = e;
                string message = "";
                if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                    message = "Path: " + System.Web.HttpContext.Current.Request.Path + "\r\n";
                string stackTrace = "";
                while (temp != null)
                {
                    message += "* Exception : " + temp.Message + "\r\n";
                    if (temp.StackTrace != null)
                        stackTrace += "* StackTrace : " + temp.StackTrace.ToString() + "\r\n";
                    temp = temp.InnerException;
                }
                message = message + stackTrace;
                Write(message, logType);
            }
            catch
            {
                FileLogger logger = new FileLogger();
                logger.WriteLog(e);
            }
        }
    }
    public class FileLogger : ILogger
    {
        public void WriteLog(string message)
        {
            IOFile mIOFile = new IOFile();
            mIOFile.WriteLogErrorToFile(message);
        }

        public void WriteLog(Exception e)
        {
            string message = "";
            if (e == null) return;

            Exception temp = e;
            if (System.Web.HttpContext.Current != null)
                if (System.Web.HttpContext.Current.User != null)
                    message += "User :" + System.Web.HttpContext.Current.User.Identity.Name + "\r\n";

            while (temp != null)
            {
                message += "* Exception : " + temp.Message + "\r\n";
                temp = temp.InnerException;
            }

            temp = e;
            while (temp != null)
            {
                if (temp.StackTrace != null)
                    message += "* StackTrace : " + temp.StackTrace.ToString() + "\r\n";
                temp = temp.InnerException;
            }
            WriteLog(message);
        }

    }
    public static class LogUtility
    {
        static ILogger _Logger;
        public static void SetLogger(ILogger logger)
        {
            _Logger = logger;
        }
        public static void WriteLog(string message)
        {            
            if (_Logger != null)
                _Logger.WriteLog(message);
        }
        public static void WriteLog(string message, int lever)
        {
            if (lever >= LogLever)
                WriteLog(message);
        }
        public static void WriteLog(Exception e, int lever)
        {
            if (lever >= LogLever)
                WriteLog(e);
        }
        public static void WriteLog(Exception e)
        {
            if (_Logger != null)
            {
                _Logger.WriteLog(e);
            }
        }
        public static void WriteDebug(string message)
        {
            if (_Logger != null)
                _Logger.WriteLog(message);
        }
        public const int LEVER_MIN = 0;
        public const int LEVER_NORMAL = 5;
        public const int LEVER_MAX = 10; 
       
        public const int LEVER_DEBUG = LEVER_MIN;
        public const int LEVER_MESSAGE = LEVER_NORMAL;
        public const int LEVER_LOG = LEVER_NORMAL+1;
        public const int LEVER_ERROR = LEVER_MAX;
        
        static int LogLever = LEVER_MIN;
        public static void SetLogLever(int lever)
        {
            LogLever = lever;
        }
    }
    public class IOFile
    {
        public IOFile()
        {

        }

        public void WriteDebugToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            string filePath = Path.Combine(appPath, Path.Combine(WebConfigurationManager.AppSettings["ErrorLogDir"], "debug" + s + ".txt"));
            StreamWriter ss = File.AppendText(filePath);

            ss.Write(message);
            ss.Close();
        }

        public void TaoThuMuc(String dirPath)
        {
            if (Directory.Exists(dirPath))
                return;
            Directory.CreateDirectory(dirPath);
        }
        public void WriteLogErrorToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;
            string errDirPath = Path.Combine(appPath, WebConfigurationManager.AppSettings["ErrorLogDir"]);
            TaoThuMuc(errDirPath);
            string filePath = Path.Combine(errDirPath, s + ".txt");
            StreamWriter ss = File.AppendText(filePath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogTaoUserToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogTaoUser\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogSuaUserToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogSuaUser\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogXoaUserToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogXoaUser\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogTaoRoleToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogTaoRole\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogXoaRoleToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogXoaRole\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogThemUserVaoRoleToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogThemUserVaoRole\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteLogXoaUserKhoiRoleToFile(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogXoaUserKhoiRole\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }
        public void WriteLogResetPassword(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogResetPassword\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }
        public static void WriteLogImportData(String filePath, string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

            appPath += "Log\\" + filePath;
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }
        public void WriteLogDoiPassword(string message)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string dd = DateTime.Now.Day.ToString();
            string mm = DateTime.Now.Month.ToString();
            string yy = DateTime.Now.Year.ToString();
            string s = yy + "_" + mm + "_" + dd;

            string time = DateTime.Now.ToLongTimeString();
            message = "\r\n----------------------------------" + time + "------------------------------------\r\n" + message;

            appPath += "Log\\LogDoiPassword\\" + s + ".txt";
            StreamWriter ss = File.AppendText(appPath);

            ss.Write(message);
            ss.Close();
        }
    }
}