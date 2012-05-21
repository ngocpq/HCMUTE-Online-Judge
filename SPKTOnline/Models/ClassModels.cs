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
        OnlineSPKTEntities db=new OnlineSPKTEntities();
        [Required]
        [DisplayName("SubjectID")]
        public string SubjectID { get; set; }
        [Required]
        [DisplayName("Group")]
        public string Group { get; set; }
        [Required]
        [DisplayName("Term")]
        public string Term { get; set; }
        [Required]
        [DisplayName("SchoolYear")]
        public string SchoolYear { get; set; }
        [Required]
        [DisplayName("LecturerID")]
        public string LecturerID { get; set; }

        
    }
    public partial class Class
    {
        public int TotalStudent
        {
            get { return this.Users.Count(); }
        }

    }
    
}