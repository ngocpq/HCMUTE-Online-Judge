using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChamDiem;
using System.IO;
using System.Web.Configuration;

namespace SPKTOnline.Management
{
    public class FileComparerFactory
    {        
        public static IFileComparer GetComparer(string assemblyName, string className)
        {
            //string root = HttpContext.Current.Server.MapPath("~/");
            string comparersDir = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["ComparersDir"]);
            string asmPath = Path.Combine(comparersDir, assemblyName);

            if (!File.Exists(asmPath))
                throw new FileNotFoundException("Không tìm thấy file Assembly");
            Object obj = ReflectionUtility.CreateObject(asmPath, className);
            if (!(obj is IFileComparer))
                throw new TypeInitializationException("Class không phải là một IFileComparer", null);
            return (IFileComparer)obj;
        }
    }
}