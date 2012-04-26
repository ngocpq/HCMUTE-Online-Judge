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
        void WriteException(Exception ex);
        void WriteDebug(string message);
    }
    public class LogUtility:ILogger
    {

        #region ILogger Members

        public void WriteLog(string message)
        {
            IOFile mIOFile = new IOFile();
            mIOFile.WriteLogErrorToFile(message);
        }

        public void WriteException(System.Exception ex)
        {
            IOFile mIOFile = new IOFile();
            mIOFile.WriteExceptionToFile(ex);
        }

        #endregion

        private static ILogger _Logger;
        private LogUtility()
        {
            //_Logger = this;
        }
        public static ILogger Logger
        {
            get
            {
                if (_Logger == null)
                    _Logger = new LogUtility();
                return _Logger;
            }
        }

        #region ILogger Members


        public void WriteDebug(string message)
        {
            IOFile mIOFile = new IOFile();
            mIOFile.WriteDebugToFile(message);
        }

        #endregion
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

            string filePath = Path.Combine(appPath, Path.Combine(WebConfigurationManager.AppSettings["ErrorLogDir"], "debug"+s + ".txt"));
            StreamWriter ss = File.AppendText(filePath);

            ss.Write(message);
            ss.Close();
        }

        public void WriteExceptionToFile(Exception e)
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

            WriteLogErrorToFile(message);
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

            string filePath = Path.Combine(appPath, Path.Combine(WebConfigurationManager.AppSettings["ErrorLogDir"], s + ".txt"));
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