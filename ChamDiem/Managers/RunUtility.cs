using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ChamDiem
{
    public class RunUtility
    {
        public static RunResult RunFile(string fileName, string agr,int timeOut)
        {
            RunResult rs = new RunResult();
            Process process;
            ProcessStartInfo StartInfo = new ProcessStartInfo();
            StartInfo.FileName = fileName;
            StartInfo.Arguments = agr;
            StartInfo.UseShellExecute = false;
            StartInfo.CreateNoWindow = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.RedirectStandardOutput = true;
            try
            {
                process = Process.Start(StartInfo);
                if (process.WaitForExit(timeOut))
                {
                    rs.Result = RunResult.ResultState.Success;
                    rs.ExitCode = process.ExitCode;
                }
                else
                {
                    if (!process.HasExited)
                        process.Kill();
                    else
                        rs.ExitCode = process.ExitCode;
                }
                if (process.StandardOutput != null)
                    rs.Output = process.StandardOutput.ReadToEnd();
                if (process.StandardError != null)
                    rs.Error = process.StandardError.ReadToEnd();
            }
            catch (Exception ex)
            {
                rs.Result = RunResult.ResultState.Error;
                rs.Error = ex.Message;
            }
            return rs;
        }
        public static RunResult RunFile(string fileName, string agr, int timeOut,string Input)
        {
            RunResult rs = new RunResult();
            Process process;
            ProcessStartInfo StartInfo = new ProcessStartInfo();
            StartInfo.FileName = fileName;
            StartInfo.Arguments = agr;
            StartInfo.UseShellExecute = false;
            StartInfo.CreateNoWindow = true;
            StartInfo.RedirectStandardInput = true;
            StartInfo.RedirectStandardError = true;
            StartInfo.RedirectStandardOutput = true;
            process = Process.Start(StartInfo);
            process.StandardInput.Write(Input);
            process.StandardInput.Close();
            if (process.WaitForExit(timeOut))
            {
                rs.Result = RunResult.ResultState.Success;
                rs.ExitCode = process.ExitCode;
            }
            else
            {
                if (!process.HasExited)
                    process.Kill();
                else
                    rs.ExitCode = process.ExitCode;
            }
            if (process.StandardOutput != null)
                rs.Output = process.StandardOutput.ReadToEnd();
            if (process.StandardError != null)
                rs.Error = process.StandardError.ReadToEnd();
            return rs;
        }
        public static RunResult RunFile(string fileName, int timeOut, string Input)
        {
            return RunFile(fileName, null, timeOut, Input);
        }
    }
}
