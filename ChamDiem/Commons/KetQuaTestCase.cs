using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
    public class KetQuaTestCase
    {
        public enum LoaiKetQua
        {
            Dung = 0,
            Sai = 1,
            QuaGio = 2,
            ViPham = 3,
            Loi=4
        }
        public ITestCase TestCase { get; set; }
        public KetQuaTestCase.LoaiKetQua KetQua { get; set; }
        public string ThongDiep { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
        public double ThoiGianChay{ get; set; }
    }
}
