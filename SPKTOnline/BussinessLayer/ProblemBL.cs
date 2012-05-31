using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPKTOnline.Models;

namespace SPKTOnline.BussinessLayer
{
    public interface IProblemBL:IBusinessBase
    {
        Problem LayTheoMa(int ma);
    }
    public class ProblemBL:BusinessBase,IProblemBL
    {
        public ProblemBL(Models.OnlineSPKTEntities entities)
            : base(entities)
        { }

        public Problem LayTheoMa(int ma)
        {
            return db.Problems.FirstOrDefault(m => m.ID == ma);
        }
    }
}