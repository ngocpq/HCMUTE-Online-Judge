using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.BussinessLayer
{
    public interface IStudentSubmitBL
    {        
    }
    public class StudentSubmitBL :BusinessBase, IStudentSubmitBL
    {
        public StudentSubmitBL(OnlineSPKTEntities entities)
            : base(entities)
        { }
    }
}