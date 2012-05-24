using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SPKTOnline.Models
{
    public partial class Problem
    {
        [Required]
        [DisplayName("Lớp học")]
        public String[] ClassID { get; set; }
        
        public int Submits { get { return this.Student_Submit.Count(); } }

        public String ContentSubString
        {
            get
            {
                if (Content == null)
                    return "";
                return Content.Substring(0, 200);
            }
        }
        public double ThongKe
        {
            get {
                    return 1.0;
            }
        }
    }

    public class AddProblemModels
    {
        [Required]
        [DisplayName("ID")]
        public int ID { get; set; }
        [Required]
        [DisplayName("Tên câu hỏi")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Nội dung câu hỏi")]
        public string Content { get; set; }

        [Required]
        [DisplayName("Được công bố")]
        public bool IsHiden { get; set; }

        [Required]
        [DisplayName("Môn học")]
        public string SubjectID { get; set; }

        [Required]
        [DisplayName("Lớp học")]
        public String[] ClassID { get; set; }

        [Required]
        [DisplayName("Độ khó")]
        public int DifficultyID { get; set; }

        [Required]
        [DisplayName("File so sánh")]
        public int ComparerID { get; set; }

        [Required]
        [DisplayName("Điểm")]
        public int Score { get; set; }

        [Required]
        [DisplayName("Giới hạn bộ nhớ")]
        public int MemoryLimit { get; set; }

        [Required]
        [DisplayName("Giới hạn thời gian")]
        public int TimeLimit { get; set; }
        public int ExamID { get; set; }
    }

}