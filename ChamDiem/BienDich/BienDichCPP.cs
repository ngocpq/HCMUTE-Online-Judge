using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using ChamDiem.BienDich;

namespace ChamDiem
{
    public class BienDichCPP : BienDichBase
    {
        //TODO: Get từ Config
        static string _IncludeDir = @"Include";
        static string _LibDir = @"Lib";

        static string _ApplicationFolder = @"D:\Working\ChamThi\ChamThi\TIENICH\VC6";
        static string _CompilerFile = @"BIN\CL.EXE";        
        public static string ApplicationFolder
        {
            get { return _ApplicationFolder; }
            set { _ApplicationFolder = value; }
        }
        public override string CompilerFile
        {
            get { return Path.Combine(ApplicationFolder, _CompilerFile); }
        }
        public string IncludeDir
        {
            get { return Path.Combine(ApplicationFolder, _IncludeDir); }
        }
        public string LibDir
        {
            get { return Path.Combine(ApplicationFolder, _LibDir); }
        }
        
        public override string GetCompileOption(string sourceFile, string exeFile)
        {
            string objFile = Path.ChangeExtension(exeFile,".obj");
            
            return String.Format("\"{0}\" /I\"{1}\" /Fo\"{2}\" /Fe\"{3}\" /link/LIBPATH:\"{4}\"", sourceFile, IncludeDir, objFile, exeFile, LibDir);
        }

        
        protected override string GetCompileErrorMessage(string originMessage)
        {
            string[] errs = originMessage.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string cppFileName = errs[0];
            string objFileName = Path.ChangeExtension(cppFileName, ".obj");
            string exeFileName = Path.ChangeExtension(cppFileName, ".exe");
            string message = "";
            for (int i = 1; i < errs.Length; i++)
            {
                int startIndex = -1;
                if (errs[i].IndexOf("\\" + cppFileName) != -1)
                {
                    startIndex = errs[i].IndexOf("\\" + cppFileName) + cppFileName.Length + 1;
                    if (errs[i].Substring(startIndex).Trim() != "")
                        message += "- cpp" + errs[i].Substring(startIndex) + "\r\n";
                }
                else if (errs[i].IndexOf("\\" + objFileName) != -1)
                {
                    startIndex = errs[i].IndexOf("\\" + objFileName) + objFileName.Length + 1;
                    if (errs[i].Substring(startIndex).Trim() != "")
                        message += "- obj" + errs[i].Substring(startIndex) + "\r\n";
                }
                else if (errs[i].IndexOf("\\" + exeFileName) != -1)
                {
                    startIndex = errs[i].IndexOf("\\" + exeFileName) + exeFileName.Length + 1;
                    if (errs[i].Substring(startIndex).Trim() != "")
                        message += "- obj" + errs[i].Substring(startIndex) + "\r\n";
                }
            }
            return message;
        }
        
    }
}
