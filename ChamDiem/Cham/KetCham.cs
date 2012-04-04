using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
  public class KetQuaCham
  {
      public int TongDiem
      {
          get
          {
              int tong = 0;
              foreach (KetQuaTestCase test in KetQuaTestCases)
              {
                  if (test.KetQua == KetQuaTestCase.LoaiKetQua.Dung)
                      tong++;
              }
              return tong;
          }
      }
    List<KetQuaTestCase> _KetQuaTestCases=new List<KetQuaTestCase>();

    public List<KetQuaTestCase> KetQuaTestCases
    {
      get { return _KetQuaTestCases; }
      set { _KetQuaTestCases = value; }
    }
  }
}
