using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ChamDiem.SoSanh
{
    public class SoSanhExternal:IFileComparer
    {
        public String FilePath { get; set; }

        #region IFileComparer Members

        public bool SoSanh(string output, ITestCase testcase, out string message)
        {            
            message = null;
            string inputString = output + "|" + testcase.Output+ "\n";
            Process p = new Process();
            ProcessStartInfo pInfo = new ProcessStartInfo(FilePath);
            pInfo.CreateNoWindow = true;
            pInfo.RedirectStandardOutput = true;
            pInfo.RedirectStandardInput = true;
            pInfo.RedirectStandardError = true;
            pInfo.UseShellExecute = false;
            p.StartInfo = pInfo;
            p.Start();
            p.StandardInput.Write(inputString);
            p.StandardInput.Close();
            if (p.WaitForExit(testcase.TimeOut))
            {
                if (p.StandardOutput != null)
                {
                    string kq = p.StandardOutput.ReadLine();
                    message = p.StandardOutput.ReadLine();
                    kq = kq.Trim(' ', '\n', '\t');
                    int result;
                    if (int.TryParse(kq, out result))
                    {
                        return result == 1;
                    }
                    else
                        throw new SoSanhException("Kết quả file không đúng định dạng");
                }
                throw new SoSanhException("Không đọc được StandarOutput");
            }
            else
            {
                if (!p.HasExited)
                    p.Kill();
                throw new TimeoutException("Thời gian chạy file quá lâu");
            }
        }
        public void Init(string parameter)
        {
            FilePath = parameter;
        }

        #endregion
    }
}
