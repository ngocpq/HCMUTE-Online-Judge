using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace SPKTOnline.Management.Menu
{
    public class MenuManager
    {
        public static Menu ReadMenu(LanguageEnum langtype)
        {
            XDocument xdoc;
            IEnumerable<XElement> xmlquery;
            OpenXml(out xdoc, out xmlquery);

            Menu _menu = new Menu();
            _menu.Children = new List<Menu>();
            foreach (var x in xmlquery)
            {
                Menu tem = new Menu();
                if (langtype == LanguageEnum.VietNamese)
                    tem.Title = x.Element("Title").Value;
                else
                    tem.Title = x.Element("TitleEng").Value;
                tem.Link = x.Element("Link").Value.Trim();
                tem.IsActive = true;

                var att = x.Element("Roles").Elements("Role");
                tem.Roles = new List<string>();
                if (att != null)
                {
                    foreach (var y in att)
                    {
                        tem.Roles.Add(y.Value);
                    }
                }
                var menu2 = x.Elements("Menu2");
                tem.Children = new List<Menu>();
                if (menu2 != null)
                {
                    foreach (var z in menu2)
                    {
                        Menu tem2 = new Menu();
                        if (langtype == LanguageEnum.VietNamese)
                            tem2.Title = z.Element("Title").Value;
                        else
                            tem2.Title = z.Element("TitleEng").Value;
                        tem2.Link = z.Element("Link").Value;
                        tem.Children.Add(tem2);
                    }                   
                }
                _menu.Children.Add(tem);
            }
            return _menu;
        }

        private static void OpenXml(out XDocument xdoc, out IEnumerable<XElement> xmlquery)
        {
            XmlReaderSettings xmlsetting = new XmlReaderSettings();
            xmlsetting.DtdProcessing = DtdProcessing.Parse;
            XmlReader xmlread = XmlReader.Create(MvcApplication.Menupath, xmlsetting);
            xdoc = XDocument.Load(xmlread);
            xmlquery = from x in xdoc.Element("Menus").Elements("Menu1")
                       select x;
            xmlread.Close();
        }


    }
}
