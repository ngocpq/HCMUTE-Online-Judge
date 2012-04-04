using System;
using System.Collections.Generic;
using System.Text;

namespace ChamDiem
{
  
  public interface IChamDiem
  {
      KetQuaCham Cham(String FileBaiLamExe, List<ITestCase> lstTestcase, IFileComparer FileComparer);    
  }
}
