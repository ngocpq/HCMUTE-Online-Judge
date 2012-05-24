using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Extensions
{
    public static class StringExtensions
    {
        public static String ToHTML(this String str)
        {
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\r\n", "<br/>");
            str = str.Replace("\n", "<br/>");
            return str;
        }
    }
}