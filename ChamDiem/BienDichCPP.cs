using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ChamDiem
{
    public class BienDichCPP : IBienDich
    {
        string _ApplicationFolder = @"D:\Working\ChamThi\ChamThi";
        string _CompilerFile = @"TIENICH\VC6\BIN\CL.EXE";
        string _IncludeDir = @"TIENICH\VC6\Include";
        string _LibDir = @"TIENICH\VC6\Lib";

        public string ApplicationFolder
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

        string GetCompileOption(string sourceFile, string exeFile)
        {
            string fileNameWithowExt = Path.GetFileNameWithoutExtension(exeFile);
            string objFile = fileNameWithowExt + ".obj";
            return String.Format("\"{0}\" /I\"{1}\" /Fo\"{2}\" /Fe\"{3}\" /link/LIBPATH:\"{4}\"", sourceFile, IncludeDir, objFile, exeFile, LibDir);
        }
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
                        ketQua.FilePath = String.Format("Lỗi biên dịch: {0}", rs.Error);
                    }
                    break;
            }

            #region Bỏ
            //Process process;
            //ProcessStartInfo StartInfo = new ProcessStartInfo();
            //StartInfo.FileName = ComplilerFile;
            //String strCompileOption = GetCompileOption(sourceFile, exeFile);
            //StartInfo.Arguments = GetCompileOption(sourceFile, exeFile);
            //StartInfo.UseShellExecute = false;
            //StartInfo.CreateNoWindow = true;
            //StartInfo.RedirectStandardError = true;
            //StartInfo.RedirectStandardOutput = true;
            //process = Process.Start(StartInfo);
            //if (process.WaitForExit(MaxCompileTime))
            //{
            //    if (process.ExitCode == 0)
            //    {
            //        ketQua.BienDichThanhCong = true;
            //        ketQua.FilePath = exeFile;
            //        if (process.StandardOutput != null)
            //            ketQua.Message = process.StandardOutput.ReadToEnd();
            //    }
            //    else
            //    {
            //        ketQua.BienDichThanhCong = false;
            //        ketQua.FilePath = "Bien dich loi:" + Environment.NewLine;
            //        if (process.StandardError != null)
            //            ketQua.Message = process.StandardError.ReadToEnd();
            //    }
            //}
            //else
            //{
            //    if (!process.HasExited)
            //        process.Kill();
            //    ketQua.BienDichThanhCong = false;
            //    ketQua.FilePath = null;
            //    ketQua.Message = "Bien dich qua thoi gian" + Environment.NewLine;
            //    if (process.StandardError != null)
            //        ketQua.Message += process.StandardError.ReadToEnd();
            //} 
            #endregion
            return ketQua;
        }
        #endregion

        int _MaxCompileTime = 5000;

        public int MaxCompileTime
        {
            get { return _MaxCompileTime; }
            set { _MaxCompileTime = value; }
        }
    }
}
