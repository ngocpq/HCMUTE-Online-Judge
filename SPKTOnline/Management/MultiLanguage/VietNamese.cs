using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Management.MultiLanguage
{
    public class VietNamese: ILanguage
    {
        public LanguageEnum Lang { get; set; }
        public Dictionary<string, string> Language { get; set; }
        public void UpdateValue() { }
    }
}