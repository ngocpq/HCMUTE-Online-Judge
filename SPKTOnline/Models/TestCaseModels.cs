using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SPKTOnline.Models
{
    public class TestCaseModels
    {
        [Required]
        [DisplayName("Mã câu hỏi")]
        public int MaDB { get; set; }

        [Required]
        [DisplayName("Input")]
        public string Input { get; set; }

        [Required]
        [DisplayName("Output")]
        public string Output { get; set; }

        [Required]
        [DisplayName("Điểm")]
        public int Diem { get; set; }

        [Required]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [Required]
        [DisplayName("Problem")]
        public Problem problem { get; set; }
    }
}