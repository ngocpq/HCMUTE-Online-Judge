using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChamDiem;
using System.Runtime.Serialization;

namespace SPKTOnline.Models
{    
    public partial class TestCas:ITestCase
    {
        public int TimeOut { get; set; }

        //public TestCas(SerializationInfo info, StreamingContext context)
        //{ 
            
        //}
        //#region ISerializable Members

        //public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        //{
        //    //info.GetValue(
        //}

        //#endregion
    }
}