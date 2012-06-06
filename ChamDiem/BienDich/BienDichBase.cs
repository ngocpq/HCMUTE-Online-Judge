using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChamDiem.BienDich
{
    public abstract class BienDichBase : IBienDich
    {
        /// <summary>
        /// Xử lý để đưa ra chuỗi thông tin lỗi biên dịch
        /// </summary>
        /// <param name="originMessage">nội dung lỗi trình biên dịch đưa ra</param>
        /// <returns>lỗi biên dịch đưa ra cho user</returns>
        protected virtual string GetCompileErrorMessage(string originMessage)
        {
            return originMessage;
        }
        public abstract string CompilerFile { get; }
        public abstract string GetCompileOption(string sourceFile, string exeFile);

        public virtual KetQuaBienDich BienDich(string sourceFile, string exeFile)
        {
            String originDir = Directory.GetCurrentDirectory();
            KetQuaBienDich ketQua = new KetQuaBienDich();
            RunResult rs = RunUtility.RunFile(CompilerFile, GetCompileOption(sourceFile, exeFile), CompileTimeOut);
            if (rs.Result == RunResult.ResultState.Success && rs.ExitCode != 0)
            {
                ketQua.BienDichThanhCong = false;
                ketQua.Message = GetCompileErrorMessage(rs.Output);

            }
            else
            {
                switch (rs.Result)
                {
                    case RunResult.ResultState.Success:
                        ketQua.Message = "Biên dịch thành công";// rs.Output;
                        break;
                    case RunResult.ResultState.Timeout:
                        ketQua.Message = "Biên dịch quá thời gian " + CompileTimeOut + "ms";
                        break;
                    case RunResult.ResultState.Error:
                        ketQua.Message = String.Format("Chương trình biên dịch bị lỗi: {0}", rs.Error);
                        break;
                }
                if (File.Exists(exeFile))
                {
                    ketQua.BienDichThanhCong = true;
                    ketQua.FilePath = exeFile;
                }
            }
            return ketQua;
        }

        int _MaxCompileTime = 10000;
        public int CompileTimeOut
        {
            get { return _MaxCompileTime; }
            set { _MaxCompileTime = value; }
        }

    }
}
