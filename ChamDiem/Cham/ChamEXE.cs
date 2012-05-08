using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ChamDiem.Exceptions;

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
                kqTest.ThoiGianChay = rs.ExecuteTime;                                
                switch (rs.Result)
                {
                    case RunResult.ResultState.Error:
                        kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Loi;
                        kqTest.ThongDiep = "Bị lỗi: " + rs.Error;
                        break;
                    case RunResult.ResultState.Timeout:
                        kqTest.KetQua = KetQuaTestCase.LoaiKetQua.QuaGio;
                        kqTest.ThongDiep = "Quá giờ";                                
                        break;
                    case RunResult.ResultState.Success:
                        if (kqTest.ThoiGianChay> test.TimeOut)
                        {
                            kqTest.KetQua = KetQuaTestCase.LoaiKetQua.QuaGio;
                            kqTest.ThongDiep = "Quá giờ";
                        }
                        else
                        {
                            try
                            {
                                string message;
                                if (FileCham.SoSanh(kqTest.Output, test, out message))
                                {
                                    kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Dung;
                                    kqTest.ThongDiep = "Đúng";                                    
                                }
                                else
                                {
                                    kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Sai;
                                    kqTest.ThongDiep = "Sai";
                                }
                            }                            
                            catch (Exception ex)
                            {
                                kqTest.KetQua = KetQuaTestCase.LoaiKetQua.Loi;                                
                                kqTest.ThongDiep = "Chương trình chấm bị lỗi";                                                                
                            }
                        }
                        break;
                }
                kq.KetQuaTestCases.Add(kqTest);
            }
            return kq;
        }
    }
}
