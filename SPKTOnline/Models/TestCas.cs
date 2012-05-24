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
        public double Score
        {
            get {  return Diem; }
            set { Diem = value; }
        }
    }
}