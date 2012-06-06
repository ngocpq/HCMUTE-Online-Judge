using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.BussinessLayer
{
    public interface IClassBL:IBusinessBase
    { 
    
    }
    public class ClassBL: IClassBL
    {

        public void BeginChange()
        {
            throw new NotImplementedException();
        }

        public void CommitChange()
        {
            throw new NotImplementedException();
        }

        public void RollbackChange()
        {
            throw new NotImplementedException();
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }
    }
}