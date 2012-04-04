using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace SPKTOnline.Management
{
    public class ReflectionUtility
    {
        public static object CreateObject(string assemblyPath, string className)
        {
            Assembly asm = Assembly.LoadFile(assemblyPath);
            Type t = asm.GetType(className);
            ConstructorInfo conInfo = t.GetConstructor(new Type[]{});
            object obj = conInfo.Invoke(null);
            return obj;
        }
    }
}