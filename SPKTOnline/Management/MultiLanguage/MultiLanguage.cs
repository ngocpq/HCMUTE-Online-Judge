using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Data.OleDb;

namespace SPKTOnline.Management.MultiLanguage
{
    public class MultiLanguage
    {
        public static DataTable GetDatatableExcel(string filename)
        {
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filename);
            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
            var ds = new DataSet();
            adapter.Fill(ds, "0");
            DataTable data = ds.Tables["0"];
            return data;
        }

        public static void CreateLanguageKey(ILanguage language, LanguageEnum languagetype)
        {
            string path;
            if (languagetype == LanguageEnum.English)
                path = MvcApplication.Englishpath;
            else
                path = MvcApplication.VietNamesepath;

            XmlReaderSettings xmlsetting = new XmlReaderSettings();
            xmlsetting.DtdProcessing = DtdProcessing.Parse;
            XmlReader xmlread = XmlReader.Create(path, xmlsetting);
            XDocument xdoc = XDocument.Load(xmlread);
            var AllKey = xdoc.Root.Elements("Keys");

            language.Language = new Dictionary<string, string>();
            language.Lang = languagetype;
            foreach (var x in AllKey)
            {
                language.Language.Add(x.Element("KeyWord").Value, x.Element("KeyValue").Value);
            }

        }

        public static void CreateXML(DataTable source, string Path)
        {
            XmlReaderSettings xmlsetting = new XmlReaderSettings();
            xmlsetting.DtdProcessing = DtdProcessing.Parse;
            XmlReader xmlread = XmlReader.Create(Path, xmlsetting);
            XDocument xdoc = XDocument.Load(xmlread);
            XElement root = xdoc.Root;

            xmlread.Close();

            int col1 = 0, col3 = 1;

            for (int i = 1; i < source.Rows.Count; i++)
            {
                XElement tem = new XElement("Keys", new XElement("KeyWord", source.Rows[i][col1].ToString()), new XElement("KeyValue", source.Rows[i][col3].ToString()));
                root.Add(tem);
            }
            xdoc.Save(Path);

        }
    }
}