using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace SPKTOnline.Management
{
    public class PathUtility
    {
        public static string GetPhysicalPath(string absPath)
        {
            string appPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            return Path.Combine(appPath, absPath);
            
        }
    }
}