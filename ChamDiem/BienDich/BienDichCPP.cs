using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ChamDiem
{
    public class BienDichCPP : IBienDich
    {
        //TODO: Get từ Config
        static string _ApplicationFolder = @"D:\Working\ChamThi\ChamThi\TIENICH\VC6";
        static string _CompilerFile = @"BIN\CL.EXE";
        static string _IncludeDir = @"Include";
        static string _LibDir = @"Lib";

        public static string ApplicationFolder
        {
            get { return _ApplicationFolder; }
            set { _ApplicationFolder = value; }
        }
        public string CompilerFile
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

        FileManager _FileManager;

        string GetCompileOption(string sourceFile, string exeFile)
        {
            string objFile = Path.ChangeExtension(exeFile,".obj");
            
            return String.Format("\"{0}\" /I\"{1}\" /Fo\"{2}\" /Fe\"{3}\" /link/LIBPATH:\"{4}\"", sourceFile, IncludeDir, objFile, exeFile, LibDir);
        }

        //public KetQuaBienDich BienDichCode(string sourceCode,string exeFilePath)
        //{
        //    _FileManager = new FileManager();
        //    String codeFilePath= _FileManager.GetNextTempFile(".cpp");
        //    StreamWriter f = File.CreateText(codeFilePath);
        //    f.Write(sourceCode);
        //    f.Close();
        //    return BienDich(codeFilePath, exeFilePath);
        //}
        
        #region IBienDich Members
        public KetQuaBienDich BienDich(string sourceFile, string exeFile)
        {
            String originDir = Directory.GetCurrentDirectory();
            KetQuaBienDich ketQua = new KetQuaBienDich();
            RunResult rs = RunUtility.RunFile(CompilerFile, GetCompileOption(sourceFile, exeFile), MaxCompileTime);
            switch (rs.Result)
            { 
                case RunResult.ResultState.Timeout:
                    ketQua.BienDichThanhCong = false;
                    ketQua.FilePath = null;
                    ketQua.Message = "Biên dịch quá thời gian " + MaxCompileTime + "ms";
                    break;
                case RunResult.ResultState.Error:
                    ketQua.BienDichThanhCong = false;
                    ketQua.FilePath = null;
                    ketQua.Message = String.Format("Error: {0}", rs.Error);
                    break;
                case RunResult.ResultState.Success:
                    if (rs.ExitCode == 0)
                    {
                        ketQua.BienDichThanhCong = true;
                        ketQua.FilePath = exeFile;
                        ketQua.Message = rs.Output;
                    }
                    else
                    {
                        ketQua.BienDichThanhCong = false;
                        ketQua.Message= String.Format("Lỗi biên dịch: {0}\n{1}",rs.Error, rs.Output);
                    }
                    break;
            }
            return ketQua;
        }
        #endregion
        //TODO: _MaxCompileTime gán từ Config
        int _MaxCompileTime = 5000;
        public int MaxCompileTime
        {
            get { return _MaxCompileTime; }
            set { _MaxCompileTime = value; }
        }
    }
}
