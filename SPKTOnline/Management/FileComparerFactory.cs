using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChamDiem;

namespace SPKTOnline.Management
{
    public class FileComparerFactory
    {
        public static IFileComparer GetComparer()
        {
            return null;
        }

        internal static IFileComparer GetComparer(string p, string p_2)
        {
            throw new NotImplementedException();
        }
    }
}