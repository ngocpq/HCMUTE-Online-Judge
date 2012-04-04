using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SPKTOnline.Models
{
    public  class ClassModels
    {
        [Required]
        [DisplayName("SubjectsID")]
        public string SubjectsID { get; set; }
        [Required]
        [DisplayName("Group")]
        public string Group { get; set; }
        [Required]
        [DisplayName("Term")]
        public string Term { get; set; }
        [Required]
        [DisplayName("SchoolYear")]
        public string SchoolYear { get; set; }
    }
    
}