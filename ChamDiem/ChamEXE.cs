using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ChamDiem
{
    public class ChamEXE : IChamDiem
    {
        public KetQuaCham Cham(string FileExe, List<ITestCase> lstTestcase, IFileComparer FileCham)
        {
            KetQuaCham kq = new KetQuaCham();
            foreach (ITestCase test in lstTestcase)
            {
                //String outTS;
                //string errTS;
                //String exception;
                //RunResult rs = RunFile(FileExe, test.TimeOut, test.Input, out outTS, out  errTS, out exception);
                RunResult rs = RunUtility.RunFile(FileExe, test.TimeOut, test.Input);
                KetQuaTestCase kqTest = new KetQuaTestCase();
                kqTest.TestCase = test;
                kqTest.Output = rs.Output;
                kqTest.Error = rs.Error;
                switch (rs.Result)
                {
                    case RunResult.ResultState.Error:
                        kqTest.KetQua = KetQuaTestCase.LoaiKetQua.ViPham;
                        kqTest.ThongDiep = "Bị lỗi: " + rs.Error;
                        break;
                    case RunResult.ResultState.Timeout:
                        kqTest.KetQua = KetQuaTestCase.LoaiKetQua.QuaGio;
                        kqTest.ThongDiep = "Quá giờ";                        
                        break;
                    case RunResult.ResultState.Success:
                        try
                        {
                            string message;
                            //if (SoSanh(kqTest.Output, test.Output, FileCham,out message))
                            if(FileCham.SoSanh(kqTest.Output,test,out message))
                            {
                                kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Dung;
                                kqTest.ThongDiep = "Đúng";
                            }
                            else
                            {
                                kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Sai;
                                kqTest.ThongDiep = "Sai kết quả";
                            }
                        }
                        catch (TimeoutException ex)
                        {
                            kqTest.KetQua = KetQuaTestCase.LoaiKetQua.QuaGio;
                            kqTest.ThongDiep = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Loi;
                            kqTest.ThongDiep = ex.Message;
                        }
                        break;
                }
                kq.KetQuaTestCases.Add(kqTest);
            }
            return kq;
        }
    }
}
