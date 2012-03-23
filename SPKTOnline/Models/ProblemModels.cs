using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SPKTOnline.Models
{
    public class AddProblemModels
    {

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
        [DisplayName("DoKho")]
        public int DifficultyID { get; set; }




    }
}