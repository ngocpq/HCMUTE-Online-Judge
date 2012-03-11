using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPKTOnline.Management.MultiLanguage
{
    public interface ILanguage
    {
        LanguageEnum Lang { get; set; }
        Dictionary<string, string> Language { get; set; }
        void UpdateValue();
       
    }
}