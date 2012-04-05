using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Models
{
    public class Student_SubmitModels
    {
    }
    public partial class Student_Submit
    {
        public TrangThaiCham TrangThai
        {
            get { return (Models.TrangThaiCham)TrangThaiCham; }
            set { TrangThaiCham = (int)value; }
        }
        public int? TongDiem
        {
            get
            {
                int? tong = 0;
                foreach (var tc in this.TestCaseResults)
                    tong += tc.Score;
                return tong;
            }
        }
        public string KetQuaBienDich
        {
            get
            {
                if (this.TrangThai == Models.TrangThaiCham.ChuaCham)
                    return null;
                if (this.TrangThaiBienDich == (int)Models.TrangThaiBienDich.ThanhCong)
                    return "Thành công";
                return "Lỗi biên dịch";
            }
        }
    }
}